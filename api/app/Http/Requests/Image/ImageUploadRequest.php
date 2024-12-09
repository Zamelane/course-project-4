<?php

namespace App\Http\Requests\Image;

use App\Http\Requests\ApiRequest;

class ImageUploadRequest extends ApiRequest
{
    public function rules(): array
    {
        return [
            'image' => 'required|image',
        ];
    }
}
