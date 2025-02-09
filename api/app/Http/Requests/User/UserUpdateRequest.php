<?php

namespace App\Http\Requests\User;

use App\Http\Requests\ApiRequest;

class UserUpdateRequest extends ApiRequest
{
    public function rules(): array
    {
        return [
            'firstName' => 'nullable|string|min:2|max:45',
            'lastName'  => 'nullable|string|min:2|max:45',
            'password'  => 'nullable|string|min:6|max:255',
            'avatar'    => 'nullable|string|exists:images,hash',
        ];
    }
}
