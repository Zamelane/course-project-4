<?php

namespace App\Http\Controllers\Utils;

use App\Models\User;
use Illuminate\Foundation\Auth\Access\AuthorizesRequests;
use App\Http\Controllers\Utils\MethodPolicyType;
use phpDocumentor\Reflection\Types\ClassString;

trait PolicyMapRegister
{
    use AuthorizesRequests;
    protected array $abilityMap = [
        'show' => 'view',
        'create' => 'create',
        'store' => 'create',
        'edit' => 'update',
        'update' => 'update',
        'destroy' => 'delete',
    ];
    protected array $methodsWithoutModels = ['index', 'create', 'store'];
    //protected bool $isDefaultPolicyMap = true;

    protected function regModels(string|array $models): void
    {
        if (is_array($models))
            foreach ($models as $model)
                $this->authorizeResource($model, lcfirst($model));
        else
            $this->authorizeResource($models, lcfirst($models));
    }
    // Сопоставление методов политики с методами запросов
    protected function resourceAbilityMap(): array
    {
        return $this->abilityMap;
    }

    // Перечисление методов запросов, не принимающих модели
    protected function resourceMethodsWithoutModels(): array
    {
        return $this->methodsWithoutModels;
    }

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

        return $this;
    }
}
