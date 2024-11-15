<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Notifications\Notifiable;
use Laravel\Sanctum\HasApiTokens;

class UserReaction extends Model
{
    use HasApiTokens, HasFactory, Notifiable;

    protected $fillable = [
        'news_id',
        'user_id',
        'reaction_id',
    ];

    protected $hidden = [];
}
