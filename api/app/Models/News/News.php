<?php

namespace App\Models\News;

use App\Models\Category;
use App\Models\City;
use App\Models\Comment\Comment;
use App\Models\Favourite;
use App\Models\HistoryView;
use App\Models\Image;
use App\Models\Reaction;
use App\Models\Tag;
use App\Models\User;
use App\Models\User\UserReaction;
use Database\Factories\NewsFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Support\Facades\Auth;

class News extends Model
{
    protected $fillable = [
        'title',
        'user_id',
        'city_id',
        'image_id',
        'content',
        'category_id',
        'create_date',
        'update_date'
    ];

    protected $hidden = [];


    /**
     * Подсказывает, где лежит фабрика
     * @return NewsFactory
     */
    protected static function newFactory(): NewsFactory
    {
        return NewsFactory::new();
    }

    public function city(): BelongsTo
    {
        return $this->belongsTo(City::class);
    }
    public function image()
    {
        return $this->belongsTo(Image::class, 'image_id');
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

    public function category()
    {
        return $this->belongsTo(Category::class);
    }

    public function isBookmarked()
    {
        $user = Auth::user();

        if (!$user)
            return false;

        return Favourite::where([
            ["news_id", "=", $this->id],
            ["user_id", "=", $user->id]
        ])->first() != null;
    }

    public function commentsCount()
    {
        return Comment::where("news_id", $this->id)->count();
    }

    public function viewsCount()
    {
        return HistoryView::where("news_id", $this->id)->count();
    }
}
