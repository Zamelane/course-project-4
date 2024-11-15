<?php

namespace App\Http\Requests\Reaction;

use Illuminate\Foundation\Http\FormRequest;

class ReactionUpdateRequest extends FormRequest
{
    public function rules(): array
    {
        return [
            'symbol' => 'string|min:1|max:1|unique:reactions',
            'description' => 'string|min:1|max:255|unique:reactions'
        ];
    }
}
