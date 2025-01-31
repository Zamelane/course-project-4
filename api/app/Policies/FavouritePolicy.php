<?php

namespace App\Policies;

use App\Models\News\News;
use App\Models\User;

class FavouritePolicy
{
    public function viewAll(User $user)
    {
        return true;
    }

    public function create(User $user)
    {
        return true;
    }

    public function delete(User $user, News $news): bool
    {
        return true;
    }
}
