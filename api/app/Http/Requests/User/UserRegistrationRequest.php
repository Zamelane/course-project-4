<?php

namespace App\Http\Requests\User;

use App\Http\Requests\ApiRequest;

class UserRegistrationRequest extends ApiRequest
{
    public function rules(): array
    {
        return [
            'firstName' => 'required|string|min:2|max:75',
            'lastName'  => 'required|string|min:2|max:75',
            'login'     => 'required|string|min:2|regex:/^[a-zA-Z0-9_-]+$/|max:75|unique:users',
            'password'  => 'required|string|min:8|max:75',
            'birthDay'  => 'required|date',
            'email'     => 'required|string|min:2|max:75|unique:users'
        ];
    }
}
