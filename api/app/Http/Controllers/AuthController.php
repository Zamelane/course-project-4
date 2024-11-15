<?php

namespace App\Http\Controllers;

use App\Exceptions\ApiException;
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
}
