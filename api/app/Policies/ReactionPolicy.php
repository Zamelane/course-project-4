<?php

namespace App\Policies;

use App\Models\Reaction;
use App\Models\User;

class ReactionPolicy
{
    public function viewAll(?User $user): bool
    {
        return true;
    }

    public function view(?User $user, Reaction $reaction): bool
    {
        return true;
    }

    public function create(?User $user): bool
    {
        return true;
    }

    public function delete(?User $user, Reaction $reaction): bool
    {
        return $user?->isAdministrator() ?: false;
    }

    public function update(?User $user, Reaction $reaction): bool
    {
        return $user?->isAdministrator() ?: false;
    }
}
