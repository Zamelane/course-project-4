<?php

namespace App\Http\Controllers;

use App\Exceptions\ApiException;
use App\Http\Requests\User\UserRegistrationRequest;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;

class AuthController extends Controller
{
    public function login(Request $request)
    {
        if (!Auth::attempt($request->only('login', 'password')))
            throw new ApiException('Invalid credentials', 401);

        $user = Auth::user();
        $token = $user->createToken('token')->plainTextToken;

        return response([
            'success' => true,
            'token' => $token,
            'user' => $user
        ]);
    }

    public function register(UserRegistrationRequest $request)
    {
        $user = User::create($request->validated());
        $token = $user->createToken('token')->plainTextToken;

        return response([
            'success' => true,
            'token' => $token,
            'user' => $user
        ], 201);
    }

    public function logout()
    {
        auth()->user()->currentAccessToken()->delete();
        return response(null, 204);
    }

    public function logoutAll()
    {
        foreach (auth()->user()->tokens as $token)
            $token->delete();
        return response(null, 204);
    }
}
