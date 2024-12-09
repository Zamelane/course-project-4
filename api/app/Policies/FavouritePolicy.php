<?php

namespace App\Policies;

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
}
