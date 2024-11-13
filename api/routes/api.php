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
    // Создание пользователя
    Route::post('', 'create')->can('create', User::class);
    // Работа с конкретным пользователем
    Route::group(['prefix' => '{user}'], function () {
        Route::get   ('', 'index' )->can('show'  , 'user');
        Route::put   ('', 'update')->can('update', 'user');
        Route::delete('', 'delete')->can('delete', 'user');
    });
});
