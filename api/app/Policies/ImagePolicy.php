<?php

namespace App\Policies;

use App\Models\User;

class ImagePolicy
{
    public function upload(User $user)
    {
        return true;
    }
}
