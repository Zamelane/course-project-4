<?php

namespace App\Http\Controllers\Routes;

use App\Exceptions\ApiException;
use App\Http\Controllers\Controller;
use App\Http\Requests\User\UserRegistrationRequest;
use App\Http\Requests\User\UserUpdateRequest;
use App\Http\Resources\User\FullUserResource;
use App\Http\Resources\User\MinUserResource;
use App\Models\Image;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Hash;

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
        $dataToUpdate = [];

        if (isset($data['avatar']))
        {
            $image = Image::where(['hash' => $data['avatar']])->first();
            $dataToUpdate['image_id'] = $image->id;
        }

        if (isset($data['firstName']))
            $dataToUpdate['firstName'] = $data['firstName'];

        if (isset($data['lastName']))
            $dataToUpdate['lastName'] = $data['lastName'];

        if (isset($data['email']))
            $dataToUpdate['email'] = $data['email'];

        if (isset($data['password']))
            $dataToUpdate['password'] = $data['password'];

        $user->update($dataToUpdate);

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
