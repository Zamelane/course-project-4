<?php

namespace App\Models;

use App\Models\News\News;
use Illuminate\Database\Eloquent\Model;

class Favourite extends Model
{
    public $timestamps = false;

    protected $fillable = [
        'news_id',
        'user_id',
        'added_date'
    ];

    public function news()
    {
        return $this->belongsTo(News::class);
    }
}
