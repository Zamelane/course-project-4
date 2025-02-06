<?php

namespace App\Http\Requests\News;

use App\Http\Requests\ApiRequest;

class NewsCreateRequest extends ApiRequest
{
    public function rules(): array
    {
        return [
            'title'     => 'required|string|min:15|max:125',
            'content'   => 'required|string|min:50',
            'tags'      => 'array',
            'tags.*'    => 'integer|exists:tags,id',
            //'city'      => 'integer|exists:cities,id',
            //'pictures'  => 'array|min:1',
            //'pictures.*'=> 'required|string|exists:images,hash',
            'category_id' => 'required|integer|exists:categories,id',
            'cover'     => 'string|exists:images,hash'
        ];
    }
}
