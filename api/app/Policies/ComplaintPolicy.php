<?php

namespace App\Policies;

use App\Models\Comment;
use App\Models\News;
use App\Models\User;

class ComplaintPolicy
{
    public function viewAll(User $user): bool
    {
        return $user->isAdministrator();
    }

    public function create(User $user, Comment $comment)
    {
        return $comment->user->id !== $user->id;
    }

    public function updateStatus(User $user, Comment $comment): bool
    {
        return $user->isAdministrator();
    }
}
