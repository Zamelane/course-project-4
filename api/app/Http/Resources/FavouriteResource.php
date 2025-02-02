<?php

namespace App\Http\Resources;

use App\Http\Resources\News\NewsFullResource;
use App\Http\Resources\News\NewsMinResource;
use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class FavouriteResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        $news = $this->news;
        return [
            'news' => NewsFullResource::make($news),
            'added_date' => $this->added_date
        ];
    }
}
