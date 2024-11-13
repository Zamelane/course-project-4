<?php

namespace App\Policies;

use App\Exceptions\ApiException;
use App\Models\User;

class UserPolicy
{
    public function show(User $user): bool
    {
        dd($user);
        throw new ApiException('fsdf', 400);
        return true;
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
