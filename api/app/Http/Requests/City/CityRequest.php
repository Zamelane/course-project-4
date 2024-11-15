<?php

namespace App\Http\Requests\City;

use App\Http\Requests\ApiRequest;

class CityRequest extends ApiRequest
{
    public function rules(): array
    {
        return [
            'name' => 'required|string|min:2|max:25|unique:cities',
        ];
    }
}
