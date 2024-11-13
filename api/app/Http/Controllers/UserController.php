<?php

namespace App\Http\Controllers;

use App\Exceptions\ApiException;
use App\Models\User;
use Illuminate\Http\Request;

class UserController extends Controller
{
    public function index(User $showedUser)
    {
        return response()->json($showedUser);
    }
    public function delete(User $user)
    {
        $user->delete();
        return response(null, 204);
    }
}
