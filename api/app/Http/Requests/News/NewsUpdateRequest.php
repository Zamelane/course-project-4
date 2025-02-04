<?php

namespace App\Http\Requests\News;

use App\Http\Requests\ApiRequest;

class NewsUpdateRequest extends ApiRequest
{
    public function rules(): array
    {
        return [
            'title'     => 'string|min:15|max:125',
            'content'   => 'string|min:50',
            'tags'      => 'array',
            'tags.*'    => 'integer|exists:tags,id',
            //'city'      => 'integer|exists:cities,id',
            //'pictures'  => 'array|min:1',
            //'pictures.*'=> 'required|string|exists:images,hash',
            'cover'     => 'string|exists:images,hash'
        ];
    }
}
