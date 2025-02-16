<?php

namespace App\Models;

use App\Models\News\News;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Notifications\Notifiable;
use Laravel\Sanctum\HasApiTokens;

class HistoryView extends Model
{
    public $timestamps = false;

    protected $fillable = [
        'user_id',
        'news_id',
        'read_date',
        'read_time'
    ];

    protected $hidden = [];

    public function news()
    {
        return $this->belongsTo(News::class);
    }
}
