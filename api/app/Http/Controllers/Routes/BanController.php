<?php

namespace App\Http\Controllers\Routes;

use App\Http\Controllers\Controller;
use App\Models\Ban;
use App\Models\User;
use Illuminate\Http\JsonResponse;

class BanController extends Controller
{
    protected string|array|null $modelsToReg = Ban::class;
    protected array $customWithoutModels = [];

    public function index(User $user): JsonResponse
    {
        // TODO: ресурсы !!!
        return response()->json($user->bans);
    }


}
