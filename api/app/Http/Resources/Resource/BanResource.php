<?php

namespace App\Http\Resources\Resource;

use App\Http\Resources\Complaint\ComplaintResource;
use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class BanResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        return [
            'complaint' => ComplaintResource::make($this->complaint),
            'ban_end_date' => $this->end_date
        ];
    }
}
