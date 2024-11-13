<?php

namespace App\Policies;

use App\Exceptions\ApiException;
use App\Models\User;

class UserPolicy
{
    public function show(User $user, User $showedUser): bool
    {
        return $user->isAdministrator() || !$showedUser->isAdministrator();
    }

    public function create(User $user): bool
    {
        return $user->isAdministrator();
    }

    public function delete(User $user): bool
    {
        return $user->isAdministrator();
    }
}
