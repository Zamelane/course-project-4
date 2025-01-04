<?php

namespace App\Http\Resources\Category;

use App\Models\Image;
use App\Models\ImgType;
use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class CategoryResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        $data = [
            'id' => $this->id,
            'name' => $this->name,
            'background_color' => $this->background_color
        ];

        if ($this->image)
            $data['image'] = [
                'id' => $this->image->id,
                'path' => Image::getPathUrl($this->image, ImgType::LocalCategory)
            ];

        return $data;
    }
}
