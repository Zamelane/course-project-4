<?php

namespace App\Models\Comment;

use App\Models\News\News;
use App\Models\User;
use Database\Factories\CommentFactory;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Notifications\Notifiable;
use Laravel\Sanctum\HasApiTokens;

class Comment extends Model
{
    use HasApiTokens, HasFactory, Notifiable;

    protected $fillable = [
        'user_id',
        'news_id',
        'content',
        'created_at',
        'updated_at'
    ];

    protected $hidden = [];

    /**
     * Подсказывает, где лежит фабрика
     * @return CommentFactory
     */
    protected static function newFactory(): CommentFactory
    {
        return CommentFactory::new();
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class);
    }

    public function news(): BelongsTo
    {
        return $this->belongsTo(News::class);
    }

    public function complaints()
    {
        return $this->hasMany(Complaint::class);
    }

    public function checkComplaintExists(User $user)
    {
        return $this->complaints()->where('author_user_id', '=', $user->id)->exists();
    }
}
