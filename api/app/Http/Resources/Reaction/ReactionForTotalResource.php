<?php

namespace App\Http\Resources\Reaction;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class ReactionForTotalResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        return [
            'total' => $this->total,
            'emoji' => $this->emoji
        ];
    }
}
