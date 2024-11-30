<?php

namespace App\Http\Resources\News;

use App\Http\Resources\Image\ImageResource;
use App\Http\Resources\Reaction\ReactionForTotalResource;
use App\Http\Resources\Tag\TagResource;
use App\Http\Resources\User\MinUserResource;
use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Str;

class NewsFullResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        return [
            'id' => $this->id,
            'author' => MinUserResource::make($this->author),
            'title' => $this->title,
            'content' => $this->content,
            'images' => ImageResource::collection($this->images), // TODO: сделать коллекцию для нормального вывода
            'city' => $this->city,
            'tags' => TagResource::collection($this->tags),
            'reactions' => ReactionForTotalResource::collection(
                $this->reactions()->groupBy('reactions.id')
                    ->select('reactions.id', DB::raw('count(*) as total, emoji'))
                    ->orderByDesc('total')
                    ->get()
            ),
            'create_date' => $this->create_date,
            'update_date' => $this->update_date
        ];
    }
}
