<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class FavouriteResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        $news = $this->news;
        return [
            'news' => [
                'id' => $news->id,
                'title' => $news->title
            ],
            'added_date' => $this->added_date
        ];
    }
}
