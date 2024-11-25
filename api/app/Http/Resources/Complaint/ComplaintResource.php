<?php

namespace App\Http\Resources\Complaint;

use App\Http\Resources\Comment\CommentResource;
use App\Http\Resources\Reason\ReasonResource;
use App\Http\Resources\User\FullUserResource;
use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class ComplaintResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        return [
            'id'            => $this->id,
            'reason'        => ReasonResource::make($this->reason),
            'author'        => FullUserResource::make($this->author),
            'comment'       => CommentResource::make($this->comment),
            'description'   => $this->description,
            'status'        => $this->status
        ];
    }
}
