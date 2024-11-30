<?php

namespace App\Models\Comment;

use App\Models\Reason;
use App\Models\User;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Notifications\Notifiable;
use Laravel\Sanctum\HasApiTokens;

class Complaint extends Model
{
    use HasApiTokens, HasFactory, Notifiable;

    protected $fillable = [
        'reason_id',
        'author_user_id',
        'comment_id',
        'description',
        'status',
        'created_at',
        'updated_at'
    ];

    protected $hidden = [];

    public function reason(): BelongsTo
    {
        return $this->belongsTo(Reason::class);
    }

    public function author(): BelongsTo
    {
        return $this->belongsTo(User::class, 'author_user_id');
    }

    public function comment(): BelongsTo
    {
        return $this->belongsTo(Comment::class);
    }

    public function bans(): HasMany
    {
        return $this->hasMany(User\Ban::class);
    }
}
