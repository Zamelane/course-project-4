<?php

namespace App\Http\Middleware;

use App\Exceptions\UnauthorizedException;
use App\Models\User;
use Closure;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;

class CustomChecker
{
    public function handle(Request $request, Closure $next, ...$role)
    {
        if (request()->bearerToken()) {
            if ($user = Auth::guard('sanctum')->user())
                Auth::setUser($user);
            else throw new UnauthorizedException();
        }
        return $next($request);
    }
}
