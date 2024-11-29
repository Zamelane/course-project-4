<?php

namespace App\Models\News;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Notifications\Notifiable;
use Laravel\Sanctum\HasApiTokens;

class NewsTag extends Model
{
    use HasApiTokens, HasFactory, Notifiable;

    protected $fillable = [
        'news_id',
        'tag_id'
    ];

    protected $hidden = [];
}
