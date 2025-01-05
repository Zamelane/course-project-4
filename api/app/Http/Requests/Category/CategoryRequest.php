<?php

namespace App\Http\Requests\Category;

use App\Http\Requests\ApiRequest;

class CategoryRequest extends ApiRequest
{
    public function rules(): array
    {
        return [
            'name' => 'required|string|min:5|max:30|unique:categories,name',
            'accent_color' => 'required|string|min:6|max:6',
            'background_color' => 'required|string|min:6|max:6',
            'image' => 'nullable|image|mimes:svg',
        ];
    }
}
