<?php

namespace App\Http\Controllers\Utils;

use Illuminate\Foundation\Auth\Access\AuthorizesRequests;
use PHPUnit\Framework\Exception;
use RecursiveIteratorIterator;
use ReflectionClass;
use Symfony\Component\Finder\Iterator\RecursiveDirectoryIterator;

trait PolicyMapRegister
{
    use AuthorizesRequests;

    protected string|null $modelToReg = null;
    protected array $abilityMap = [
        'index' => 'viewAll',
        'store' => 'create',
        'show' => 'view',
        'update' => 'update',
        'destroy' => 'delete'
    ];
    protected array $methodsWithoutModels = [];
    // К какой модели относится контроллер
    protected string|null $target = null;

    /**
     * Возвращает имя модели из строки-класса модели
     * @param string $model
     * @return string
     */
    protected function getModelName(string $model): string
    {
        $separatedPath = explode('\\', $model);
        return $separatedPath[array_key_last($separatedPath)];
    }

    /**
     * Связывает контроллер с политикой указанной модели, если она была задана
     */
    public function __construct()
    {
        $this->target ??= static::class;
        $targetClass = $this->target;
        $generatedAbilityMap = [];      // Итоговые правила мэппинга
        $generatedWithoutModels = [];   // Перечисление моделей без параметров
        $middleware = [];               // Для отладки

        // Извлекаем имя для общего именования через контроллер
        $name = str_replace('Controller', '', $this->getModelName($targetClass));

        $this->modelToReg ??= $this->searchModelByName($name);
        $policyClass = "\App\Policies\\{$name}Policy";

        // Если политики не существует, то не привязываем ability
        if (!class_exists($policyClass))
            return;

        if (!class_exists($this->modelToReg))
            throw new Exception('Model not found');

        $policyInfo = $this->getMapForClassMethods($policyClass);
        $controllerInfo = $this->getMapForClassMethods($targetClass);

        // Перебираем ability политики
        foreach ($policyInfo as $method) {
            $abilityName = $method['name'];
            $modelName = $this->modelToReg;

            // Имя текущего метода контроллера, связанного со способностью
            $controllerMethodName = $this->getControllerMethodNameByAbilityName($abilityName);

            // Если способность и метод политики содержит модель с тем же именованием, как у контроллера
            $isContains = $this->checkPolicyContains($method, $controllerMethodName, $controllerInfo, $name);

            $generatedAbilityMap[$controllerMethodName] = $abilityName;

            if (!$isContains)
                $generatedWithoutModels[] = $controllerMethodName;
            else
                $modelName = lcfirst($name);

            $method = null;
            foreach ($controllerInfo as $controllerMethod)
                if ($controllerMethod['name'] === $controllerMethodName)
                    $method = $controllerMethod;

            if (!$method)
                throw new Exception("The controller ({$controllerMethodName}) method is not set for the corresponding policy method");

            $models = $this->getModelsNamesForMethodParams($method['params'], $name);
            if (count($models))
                $modelName = join(',', [$modelName, ...$models]);

            $middleware["can:{$abilityName},{$modelName}"][] = $controllerMethodName; // Для отладки
            $this->middleware("can:{$abilityName},{$modelName}", [])->only($controllerMethodName);
        }

        $this->methodsWithoutModels = $generatedWithoutModels;
        $this->abilityMap = $generatedAbilityMap;
    }

    public function searchModelByName($name)
    {
        $directories = new RecursiveDirectoryIterator(app_path('Models'), \FilesystemIterator::FOLLOW_SYMLINKS);
        $iterator = new RecursiveIteratorIterator($directories);
        foreach ($iterator as $dir)
            if (!$dir->isDir() && substr($dir->getFileName(), 0, -(strlen($dir->getExtension()) + 1)) === $name) {
                $path = $dir->getPath();
                return 'App\\' . explode('app\\', $path)[1] . "\\{$name}";
            }
        return null;
    }

    /**
     * Получает имена моделей используемых в параметрах метода контроллера
     * @param $params
     * @param $nameToContinue
     * @return array
     */
    public function getModelsNamesForMethodParams($params, $nameToContinue): array
    {
        foreach ($params as $param)
            if (str_contains($param['typeHint'], '\Models\\')
                && ($modelName = $this->getModelName($param['typeHint'])) !== $nameToContinue)
                $names[] = lcfirst($modelName);

        return $names ?? [];
    }

    /**
     * Получает имя связанного метода в контроллере со способностью
     * @param $controllerMethodName
     * @return false|int|mixed|string
     */
    public function getControllerMethodNameByAbilityName($controllerMethodName): mixed
    {
        return array_search($controllerMethodName, $this->abilityMap) ?: $controllerMethodName;
    }

    /**
     * Проверяет, принимает ли политика и метод контроллера модель с именем контроллера
     * @param $policyMethod
     * @param $controllerMethodName
     * @param $controllerMethods
     * @param $checkContainsName
     * @return bool
     */
    public function checkPolicyContains($policyMethod, $controllerMethodName, $controllerMethods, $checkContainsName): bool
    {
        foreach ($policyMethod['params'] as $param)
            if (str_contains($param['typeHint'], $checkContainsName))
                // Если содержит, то перебираем методы контроллера
                return $this->checkControllerContains($controllerMethods, $controllerMethodName, $checkContainsName);
        return false;
    }

    /**
     * Проверяет, принимает ли метод контроллера модель с именем контроллера
     * @param $controllerMethods
     * @param $searchMethodName
     * @param $checkContainsName
     * @return bool
     */
    public function checkControllerContains($controllerMethods, $searchMethodName, $checkContainsName): bool
    {
        // Если содержит, то перебираем методы контроллера
        foreach ($controllerMethods as $controllerMethod) {
            if ($controllerMethod['name'] != $searchMethodName)
                continue;
            // Если метод контроллера содержится в карте способностей, то перебираем параметры этого метода
            foreach ($controllerMethod['params'] as $controllerParameter)
                // Если метод и вправду использует этот класс, то помечаем
                if (!str_contains($controllerParameter['typeHint'], 'Request')
                    && str_contains($controllerParameter['typeHint'], $checkContainsName)
                    && ($controllerParameter['position'] != 0 || count($controllerMethod['params']) == 1))
                    return true;
        }
        return false;
    }

    /**
     * Составляет карту методов у класса и их параметры
     * @param $class
     * @return array
     * @throws \ReflectionException
     */
    public function getMapForClassMethods($class)
    {
        $map = [];  // Карта методов класса
        $reflection = new ReflectionClass($class);

        foreach ($reflection->getMethods() as $method) {
            // Перебираем все параметры методов
            foreach ($method->getParameters() as $parameter)
                $params[] = [
                    'name' => $parameter->getName(),
                    'typeHint' => $parameter->getType()?->getName(),
                    'position' => count($params ?? [])
                ];
            // Сохраняем в карту методов класса
            $map[] = [
                'name' => $method->getName(),
                'params' => $params ?? []
            ];
        }
        return $map;
    }

    /**
     * Сопоставление методов политики с методами запросов
     * @return array
     */
    protected function resourceAbilityMap(): array
    {
        return $this->abilityMap;
    }

    /**
     * Перечисление методов запросов, не принимающих модели
     * @return array
     */
    protected function resourceMethodsWithoutModels(): array
    {
        return $this->methodsWithoutModels;
    }
}
