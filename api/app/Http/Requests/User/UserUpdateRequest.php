<?php

namespace App\Http\Requests\User;

use App\Http\Requests\ApiRequest;

class UserUpdateRequest extends ApiRequest
{
    public function rules(): array
    {
        return [
            'firstName' => 'string|min:2|max:45',
            'lastName'  => 'string|min:2|max:45',
            'password'  => 'string|min:6|max:255'
        ];
    }
}
