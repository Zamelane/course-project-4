<?php

namespace App\Http\Resources\HistoryView;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class HistoryViewResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        $news = $this->news;
        return [
            'news' => [
                'id' => $news->id,
                'title' => $news->title
            ],
            'read_date' => $this->read_date,
            'read_time' => $this->read_time
        ];
    }
}
