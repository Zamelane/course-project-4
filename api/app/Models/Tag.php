<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Notifications\Notifiable;
use Laravel\Sanctum\HasApiTokens;

class Tag extends Model
{
    protected $fillable = [
        'name',
        'description'
    ];

    protected $hidden = [
        'created_at',
        'updated_at'
    ];
}
