<?php

namespace App\Http\Requests\Reaction;

use App\Http\Requests\ApiRequest;

class ReactionUpdateRequest extends ApiRequest
{
    public function rules(): array
    {
        return [
            'emoji' => 'string|min:1|max:1|unique:reactions',
            'description' => 'string|min:1|max:255'
        ];
    }
}
