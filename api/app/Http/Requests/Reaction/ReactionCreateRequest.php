<?php

namespace App\Http\Requests\Reaction;

use App\Http\Requests\ApiRequest;

class ReactionCreateRequest extends ApiRequest
{
    public function rules(): array
    {
        return [
            'symbol' => 'required|string|min:1|max:1|unique:reactions',
            'description' => 'required|string|min:1|max:255|unique:reactions'
        ];
    }
}
