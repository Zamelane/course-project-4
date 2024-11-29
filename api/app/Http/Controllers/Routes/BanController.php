<?php

namespace App\Http\Controllers\Routes;

use App\Http\Controllers\Controller;
use App\Models\User;
use App\Models\User\Ban;
use Illuminate\Http\JsonResponse;

class BanController extends Controller
{
    public function index(User $user): JsonResponse
    {
        // TODO: ресурсы !!!
        return response()->json($user->bans);
    }

    public function show(User $user, Ban $ban): JsonResponse
    {
        return response()->json($ban);
    }
}
