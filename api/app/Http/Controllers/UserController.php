<?php

namespace App\Http\Controllers;

use App\Http\Requests\User\UserUpdateRequest;
use App\Http\Resources\User\FullUserResource;
use App\Http\Resources\User\MinUserResource;
use App\Models\User;
use Illuminate\Http\Request;

class UserController extends Controller
{
    public function index(Request $request, User $user)
    {
        $collection = $request->user()?->isAdministrator()
            ? FullUserResource::make($user)
            : MinUserResource::make($user);
        return response()->json($collection);
    }

    public function update(UserUpdateRequest $request, User $user)
    {
        $data = $request->validated();
        $user->update($data);
        return response()->json(
            $request->user()->isAdministrator()
                ? new FullUserResource($user)
                : new MinUserResource($user)
        );
    }

    public function delete(User $user)
    {
        $user->delete();
        return response(null, 204);
    }
}
