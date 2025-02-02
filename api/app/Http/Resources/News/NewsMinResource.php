<?php

namespace App\Http\Resources\News;

use App\Http\Resources\Image\ImageResource;
use App\Http\Resources\Reaction\ReactionForTotalResource;
use App\Http\Resources\Tag\TagResource;
use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Str;

class NewsMinResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        return [
            'id' => $this->id,
            'title' => $this->title,
            'content' => Str::limit($this->content, 120), //TODO: продумать, как отрывки показывать
            'city' => $this->city,
            'tags' => TagResource::collection($this->tags),
            'reactions' => ReactionForTotalResource::collection(
                $this->reactions()->groupBy('reactions.id')
                    ->select('reactions.id', DB::raw('count(*) as total, emoji'))
                    ->orderByDesc('total')
                    ->get()
            ),
            'cover' => ImageResource::make($this->image),
            'create_date' => $this->create_date,
            'update_date' => $this->update_date
        ];
    }
}
