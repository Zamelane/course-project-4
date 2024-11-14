<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasManyThrough;
use Illuminate\Foundation\Auth\User as Authenticatable;
use Illuminate\Notifications\Notifiable;
use Laravel\Sanctum\HasApiTokens;

class News extends Authenticatable
{
    use HasApiTokens, HasFactory, Notifiable;

    protected $fillable = [
        'title',
        'city_id',
        'content',
        'create_date',
        'update_date'
    ];

    protected $hidden = [];

    public function city(): BelongsTo
    {
        return $this->belongsTo(City::class);
    }

    public function images(): HasManyThrough
    {
        return $this->hasManyThrough(Image::class, 'news_images');
    }

    public function user_reaction(User $user): Reaction | null
    {
        return $this->hasOneThrough(Reaction::class, 'user_reactions')
            ->where('user_id', '=', $user->id)->first();
    }

    public function tags(): HasManyThrough
    {
        return $this->hasManyThrough(Tag::class, 'news_tags');
    }
}
