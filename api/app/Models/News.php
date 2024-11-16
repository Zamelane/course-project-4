<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasManyThrough;
use Illuminate\Foundation\Auth\User as Authenticatable;
use Illuminate\Notifications\Notifiable;
use Laravel\Sanctum\HasApiTokens;

class News extends Model
{
    use HasApiTokens, HasFactory, Notifiable;

    protected $fillable = [
        'title',
        'user_id',
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

    public function images(): BelongsToMany
    {
        return $this->belongsToMany(Image::class, NewsImage::class);
    }

    public function user_reaction(User $user): Reaction | null
    {
        return $this->hasOneThrough(Reaction::class, 'user_reactions')
            ->where('user_id', '=', $user->id)->first();
    }

    public function author(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }

    public function tags(): BelongsToMany
    {
        return $this->belongsToMany(Tag::class, NewsTag::class);
    }

    public function reactions(): BelongsToMany
    {
        return $this->belongsToMany(Reaction::class, UserReaction::class);
    }

    public function comments(): HasMany
    {
        return $this->hasMany(Comment::class);
    }
}
