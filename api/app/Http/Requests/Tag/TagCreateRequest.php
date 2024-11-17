<?php

namespace App\Http\Requests\Tag;

use App\Http\Requests\ApiRequest;

class TagCreateRequest extends ApiRequest
{
    public function rules(): array
    {
        return [
            'name' => 'required|string|min:2|max:25|unique:tags',
            'description' => 'string|min:5|max:50',
        ];
    }
}
