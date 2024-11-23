<?php

namespace App\Models\News;

use App\Models\City;
use App\Models\Comment\Comment;
use App\Models\Image;
use App\Models\Reaction;
use App\Models\Tag;
use App\Models\User;
use App\Models\User\UserReaction;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;
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
