<?php

namespace App\Http\Requests\Comment;

use App\Http\Requests\ApiRequest;

class CommentCreateRequest extends ApiRequest
{
    public function rules(): array
    {
        return [
            'content' => 'required|string|min:1|max:1000',
        ];
    }
}
