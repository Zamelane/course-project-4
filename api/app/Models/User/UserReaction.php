<?php

namespace App\Models\User;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Notifications\Notifiable;
use Laravel\Sanctum\HasApiTokens;

class UserReaction extends Model
{
    protected $fillable = [
        'news_id',
        'user_id',
        'reaction_id',
    ];

    protected $hidden = [];
}
