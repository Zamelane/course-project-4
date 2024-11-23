<?php

namespace App\Http\Controllers\Routes;

use App\Exceptions\ApiException;
use App\Http\Controllers\Controller;
use App\Http\Requests\User\UserRegistrationRequest;
use App\Http\Requests\User\UserUpdateRequest;
use App\Http\Resources\User\FullUserResource;
use App\Http\Resources\User\MinUserResource;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;

class UserController extends Controller
{
    public function me()
    {
        return response()->json(FullUserResource::make(auth()->user()));
    }
    public function show(Request $request, User $user)
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

    public function destroy(User $user)
    {
        $user->delete();
        return response(null, 204);
    }
}
