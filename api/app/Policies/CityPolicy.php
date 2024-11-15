<?php

namespace App\Policies;

use App\Models\City;
use App\Models\User;

class CityPolicy
{
    public function showAll(?User $user): bool
    {
        return true;
    }

    public function create(?User $user): bool
    {
        return $user?->isAdministrator() ?: false;
    }

    public function show(?User $user, City $city): bool
    {
        return true;
    }

    public function delete(?User $user, City $city): bool
    {
        return $user?->isAdministrator() ?: false;
    }

    public function update(?User $user, City $city): bool
    {
        return $user?->isAdministrator() ?: false;
    }
}
