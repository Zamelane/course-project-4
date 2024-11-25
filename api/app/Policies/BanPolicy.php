<?php

namespace App\Policies;

use App\Models\User;

class BanPolicy
{
    public function viewAll(User $user)
    {
        return $user->isAdministrator();
    }
}
