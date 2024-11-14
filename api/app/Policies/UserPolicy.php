<?php

namespace App\Policies;

use App\Models\User;

class UserPolicy
{
    public function show(?User $user, User $showedUser): bool
    {
        return $user?->isAdministrator() || !$showedUser->isAdministrator();
    }

    public function update(User $user, User $updatedUser): bool
    {
        return $user->isAdministrator() || $user->id === $updatedUser->id;
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
