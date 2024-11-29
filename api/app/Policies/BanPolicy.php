<?php

namespace App\Policies;

use App\Models\User;
use App\Models\User\Ban;

class BanPolicy
{
    public function viewAll(User $user): bool
    {
        return $user->isAdministrator();
    }

    public function view(User $user, Ban $ban): bool
    {
        return $user->isAdministrator();
    }
}
