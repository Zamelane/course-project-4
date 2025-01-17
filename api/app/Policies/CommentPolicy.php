<?php

namespace App\Policies;

use App\Models\Comment\Comment;
use App\Models\User;

class CommentPolicy
{
    public function viewAll(?User $user): bool
    {
        return true;
    }

    public function create(User $user): bool
    {
        return true;
    }

    public function update(User $user, Comment $comment): bool
    {
        return $user->id === $comment->user_id;
    }

    public function delete(User $user, Comment $comment): bool
    {
        return $user->isAdministrator() || $user->id === $comment->user_id;
    }
}
