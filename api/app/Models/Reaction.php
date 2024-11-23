<?php

namespace App\Models;

use App\Models\News\News;
use App\Models\User\UserReaction;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\TModel;
use Illuminate\Notifications\Notifiable;
use Laravel\Sanctum\HasApiTokens;

class Reaction extends Model
{
    use HasApiTokens, HasFactory, Notifiable;

    protected $fillable = [
        'emoji',
        'description'
    ];

    protected $hidden = [];

    /**
     * @param User $user
     * @param News $news
     * @return TModel
     */
    public function setReaction(User $user, News $news)
    {
        return UserReaction::updateOrCreate([
            'user_id' => $user->id,
            'news_id' => $news->id
        ], [
            'user_id' => $user->id,
            'news_id' => $news->id,
            'reaction_id' => $this->id
        ]);
    }

    /**
     * @param User $user
     * @param News $news
     * @return bool|mixed|null
     */
    public static function deleteReaction(User $user, News $news)
    {
        return UserReaction::where([
            'user_id' => $user->id,
            'news_id' => $news->id
        ])->delete();
    }
}
