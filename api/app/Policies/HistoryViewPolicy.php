<?php

namespace App\Policies;

use App\Models\User;

class HistoryViewPolicy
{
    public function viewAll(User $user)
    {
        return true;
    }
}
