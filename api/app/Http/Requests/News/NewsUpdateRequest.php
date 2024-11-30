<?php

namespace App\Http\Requests\News;

use App\Http\Requests\ApiRequest;

class NewsUpdateRequest extends ApiRequest
{
    public function rules(): array
    {
        return [
            'title'     => 'required|string|min:15|max:125',
            'content'   => 'required|string|min:50',
            'tags'      => 'array',
            'tags.*'    => 'integer|exists:tags,id',
            'city'      => 'integer|exists:cities,id',
            'newPictures'  => 'array|min:1',
            'newPictures.*'=> 'required|file',
            'deletedPictures' => 'array|min:1',
            'deletedPictures.*'=> 'required|integer',
        ];
    }
}
