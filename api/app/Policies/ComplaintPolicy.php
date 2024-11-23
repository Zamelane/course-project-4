<?php

namespace App\Policies;

use App\Models\Comment\Comment;
use App\Models\News\News;
use App\Models\User;

class ComplaintPolicy
{
    public function viewAll(User $user): bool
    {
        return $user->isAdministrator();
    }

    public function create(User $user, News $news, Comment $comment)
    {
        return $comment->user->id !== $user->id;
    }

    public function updateStatus(User $user, Comment $comment): bool
    {
        return $user->isAdministrator();
    }
}
