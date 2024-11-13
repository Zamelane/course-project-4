<?php
use Illuminate\Support\Facades\Route;
use App\Models\User;
use App\Http\Controllers\AuthController;
use App\Http\Controllers\UserController;

Route
::controller(AuthController::class)
->group(function () {
    Route::post('login', 'login');
    Route::post('register', 'register');
    Route::prefix('logout')
    ->group(function () {
        Route::get('', 'logout');
        Route::get('all', 'logoutAll');
    });
});


Route
::controller(UserController::class)
->prefix('users')
->middleware('auth:sanctum')
->group(function() {
    Route::get('{showedUser}', 'index')
        ->can('show', 'showedUser');
    Route::post('', 'create')
        ->can('create', User::class);
    Route::delete('{user}', 'delete')
        ->can('delete', 'user');
});
