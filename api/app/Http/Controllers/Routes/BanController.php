<?php

namespace App\Http\Controllers\Routes;

use App\Http\Controllers\Controller;
use App\Http\Resources\BanMinResource;
use App\Http\Resources\Resource\BanResource;
use App\Models\User;
use App\Models\User\Ban;
use Illuminate\Http\JsonResponse;

class BanController extends Controller
{
    public function index(User $user): JsonResponse
    {
        // TODO: ресурсы !!!
        return response()->json(BanMinResource::collection($user->bans()));
    }

    public function show(User $user, Ban $ban): JsonResponse
    {
        return response()->json(BanResource::make($ban));
    }
}
