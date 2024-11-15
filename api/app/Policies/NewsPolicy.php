<?php

namespace App\Policies;

use App\Models\News;
use App\Models\User;

class NewsPolicy
{
    public function showAll(?User $user): bool
    {
        return true;
    }

    public function show(?User $user, News $news): bool
    {
        return true;
    }

    public function create(?User $user): bool
    {
        return $user?->role === 'reporter';
    }

    public function update(?User $user, News $news): bool
    {
        return $user?->isAdministrator() || $user?->id === $news->user_id;
    }

    public function delete(?User $user, News $news): bool
    {
        return $user?->isAdministrator() || $user?->id === $news->user_id;
    }
}
