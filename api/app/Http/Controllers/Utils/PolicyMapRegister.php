<?php

namespace App\Http\Controllers\Utils;

use App\Exceptions\ApiException;
use Illuminate\Foundation\Auth\Access\AuthorizesRequests;
use ReflectionClass;

trait PolicyMapRegister
{
    use AuthorizesRequests;
    protected string|array|null $modelsToReg = null;
    protected array $abilityMap = [
        'index'     => 'viewAll',
        'store'     => 'create',
        'show'      => 'view',
        'update'    => 'update',
        'destroy'   => 'delete'
    ];
    protected array $methodsWithoutModels = ['index', 'store'];
    protected array $customAbilityMap = [];
    protected array $customWithoutModels = [];
    protected bool $clearWithoutModels = false;
    protected string|null $parameter = null;
    //protected bool $isDefaultPolicyMap = true;

    /**
     * Указывает модели, политики которых будут использоваться для проверки прав
     * @param string|array $models
     * @return void
     */
    protected function regModels(string|array $models): void
    {
        $models = is_array($models) ? $models : [$models];
        $this->modelsToReg = $models;
    }

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

    /**
     * Регистрирует способность или ассоциацию метода
     * контроллера с именованием метода проверки прав в политике
     * @param string $methodName
     * @param string|MethodPolicyType|null $param1
     * @param string|MethodPolicyType|null $param2
     * @return $this
     */
    protected function regAbility(
        string $methodName,
        string|MethodPolicyType|null $param1 = null,
        string|MethodPolicyType|null $param2 = MethodPolicyType::Required
    )
    {
        //if ($this->isDefaultPolicyMap)
        //{
        //    $this->isDefaultPolicyMap = false;
        //    $this->abilityMap = [];
        //    $this->methodsWithoutModels = [];
        //}

        $policyName = is_string($param1)
            ? $param1
            : (is_string($param2) ? $param2 : $methodName);

        $methodType = $param1 instanceof MethodPolicyType
            ? $param1
            : ($param2 instanceof MethodPolicyType ? $param2 : MethodPolicyType::Required);

        $this->abilityMap[$methodName] = $policyName;

        if ($methodType === MethodPolicyType::Without)
            $this->methodsWithoutModels[] = $methodName;

        $this->middleware = [];
        //$this->middleware("can:{$policyName},{$methodType === MethodPolicyType::Required ?  : }")
        //$this->authorizeResource($methodName);

        return $this;
    }

    /**
     * Применяет методы запросов к способностям
     * @return void
     */
    public function applyRules(): void
    {
        $this->authorizeResource($this->modelsToReg[0], $this->parameter);
    }

    /**
     * Связывает контроллер с политикой указанной модели, если она была задана
     */
    public function __construct()
    {
//        // Если нужно переопределить withoutModels
//        if ($this->clearWithoutModels)
//            $this->methodsWithoutModels = [];
//
//        $this->abilityMap           = array_merge($this->abilityMap, $this->customAbilityMap);
//        $this->methodsWithoutModels = array_merge($this->methodsWithoutModels, $this->customWithoutModels);
//        if ($this->modelsToReg !== null)
//            $this->regModels($this->modelsToReg);
//        $this->applyRules();

        //$this->methodsWithoutModels = [];
        //->abilityMap = [];

        $targetClass = static::class;

        $name = str_replace('Controller', '', $this->getModelName($targetClass));

        $policyClass = "\App\Policies\\{$name}Policy";

        $policyInfo = new ReflectionClass($policyClass);
        $controllerInfo = new ReflectionClass($targetClass);

        // Перебираем ability политики
        foreach ($policyInfo->getMethods() as $method)
        {
            $isContains = false; // Смотрим, содержится ли модель от контроллера в параметрах политики
            foreach ($method->getParameters() as $parameter)
            {
                if (str_contains($parameter->getType()->getName(), $name))
                {
                    // Если содержит, то перебираем методы контроллера
                    foreach ($controllerInfo->getMethods() as $controllerMethod)
                        // Если метод контроллера содержится в карте способностей
                        if (isset($this->abilityMap[$controllerMethod->getName()])
                        && $this->abilityMap[$controllerMethod->getName()] === $method->getName())
                        {
                            // То перебираем параметры этого метода
                            foreach ($controllerMethod->getParameters() as $controllerParameter)
                            {
                                // Если метод и вправду использует этот класс, то помечаем
                                if (str_contains($controllerParameter->getType()->getName(), $name))
                                {
                                    $isContains = true;
                                    break;
                                }
                            }
                        }
                }
            }

            $controllerMethod = $this->searchMethodInController($method->getName()) ?: $method->getName();

            $this->customAbilityMap[$controllerMethod] = $method->getName();

            if (!$isContains)
            {
                $this->customWithoutModels[] = $controllerMethod;
            }
        }

        $this->methodsWithoutModels = $this->customWithoutModels;
        $this->abilityMap = $this->customAbilityMap;

        $this->modelsToReg = "\App\Models\\{$name}";

        $this->regModels($this->modelsToReg);
        $this->applyRules();
    }

    public function searchMethodInController($abilityName)
    {
        foreach ($this->abilityMap as $methodName => $policyName)
            if ($policyName === $abilityName)
                return $methodName;
        return null;
    }
}
