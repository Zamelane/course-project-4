<?php

namespace App\Policies;

use App\Models\Tag;
use App\Models\User;
use Illuminate\Auth\Access\Response;

class TagPolicy
{
    public function showAll(?User $user): bool
    {
        return true;
    }

    public function show(?User $user, Tag $tag): bool
    {
        return true;
    }

    public function delete(?User $user, Tag $tag): bool
    {
        return $user?->isAdministrator() ?: false;
    }

    public function create(?User $user): bool
    {
        return $user?->isAdministrator() ?: false;
    }
}
