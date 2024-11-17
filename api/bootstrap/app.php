<?php

use Illuminate\Foundation\Application;
use Illuminate\Foundation\Configuration\Exceptions;
use Illuminate\Foundation\Configuration\Middleware;
use App\Exceptions\UnauthorizedException;
use App\Http\Middleware\CustomChecker;
use Symfony\Component\HttpKernel\Exception\NotFoundHttpException;
use Illuminate\Http\Request;

return Application::configure(basePath: dirname(__DIR__))
    ->withRouting(
        web: __DIR__ . '/../routes/web.php',
        commands: __DIR__ . '/../routes/console.php',
        api: __DIR__ . '/../routes/api.php',
        health: '/up',
    )
    ->withMiddleware(function (Middleware $middleware) {
        $middleware->redirectGuestsTo(fn() => throw new UnauthorizedException());
        $middleware->append(CustomChecker::class);
    })
    ->withExceptions(function (Exceptions $exceptions) {
        $exceptions->shouldRenderJsonWhen(
            fn(Request $request) => $request->is('api/*') && !config('app.debug') // <-- Если надо всё в json
        );

        $exceptions->render(function (NotFoundHttpException $e) {
            return response()->json([
                'code' => 404,
                'message' => 'Not found',
            ], 404);
        });
        /*->render(function (AccessDeniedHttpException $e) {
            return response()->json([
                'code' => 403,
                'message' => 'Access Denied',
            ], 403);
        })
        ->render(function (NotFoundHttpException $e) {
            return response()->json([
                'code' => 404,
                'message' => 'Not Found',
            ], 404);
        });*/

    })->create();
