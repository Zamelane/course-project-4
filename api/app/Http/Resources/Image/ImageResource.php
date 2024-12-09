<?php

namespace App\Http\Resources\Image;

use App\Models\Image;
use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class ImageResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        return [
            'id' => $this->id,
            'url' => Image::getPathUrl($this->resource),
            'upload_date' => $this->upload_date
        ];
    }
}
