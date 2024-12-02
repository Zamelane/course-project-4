<?php

namespace App\Http\Controllers\Routes;

use App\Exceptions\ApiException;
use App\Http\Controllers\Controller;
use App\Http\Requests\User\UserRegistrationRequest;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use DateTime;

class AuthController extends Controller
{
    public function login(Request $request)
    {
        if (!Auth::attempt($request->only('login', 'password')))
            throw new ApiException('Invalid credentials', 401);

        $user = Auth::user();

        $currentDate = (new DateTime())->format('Y-m-d');

        $ban = User\Ban::join('complaints', 'complaints.id', '=', 'bans.complaint_id')
            ->join('comments', 'comments.id', '=', 'complaints.comment_id')
            ->where('end_date', '>', $currentDate)
            ->where('user_id', '=', $user->id)
            ->first();

        if ($ban)
            throw new ApiException('Your account is blocked', 403);

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
