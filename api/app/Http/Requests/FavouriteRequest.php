<?php

namespace App\Http\Requests;

class FavouriteRequest extends ApiRequest
{
    public function rules(): array
    {
        return [
            'news_id' => 'required|integer|exists:news,id'
        ];
    }
}
