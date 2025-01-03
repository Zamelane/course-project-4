<?php

namespace App\Http\Resources\Category;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class CategoryResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        $data = [
            'id' => $this->id,
            'name' => $this->name,
            'accent_color' => $this->accent_color,
            'background_color' => $this->background_color
        ];

        if ($this->image)
            $data['image'] = [
                'id' => $this->image->id,
                'path' => $this->image->path
            ];

        return $data;
    }
}
