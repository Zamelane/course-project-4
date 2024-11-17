<?php

namespace App\Http\Controllers\Routes;

use App\Http\Middleware\CustomChecker;
use Illuminate\Support\Facades\Route;

Route
::controller(AuthController::class)
->group(function () {
    Route::post('login', 'login');
    Route::post('register', 'register');
    Route::prefix('logout')
    ->middleware(CustomChecker::class)
    ->group(function () {
        Route::get('', 'logout');
        Route::get('all', 'logoutAll');
    });
});


Route
::controller(UserController::class)
->prefix('users')
->group(function() {
    // Создание пользователя
    Route::post('', 'store');
    // Работа с конкретным пользователем
    Route::group(['prefix' => '{user}'], function () {
        Route::get   ('', 'show' );
        Route::put   ('', 'update');
        Route::delete('', 'destroy');
    });
});

Route
::controller(TagController::class)
->prefix('tags')
->group(function () {
    Route::get('', 'index');
    Route::post('', 'store');
    Route::group(['prefix' => '{tag}'], function () {
        Route::get('', 'show');
        Route::delete('', 'destroy');
    });
});

Route
::controller(CityController::class)
->prefix('cities')
->group(function () {
    Route::get('', 'index');
    Route::post('', 'store');
    Route::group(['prefix' => '{city}'], function () {
        Route::get('', 'show');
        Route::delete('', 'destroy');
        Route::put('', 'update');
    });
});

Route
::controller(ReactionController::class)
->prefix('reactions')
->group(function () {
    Route::get('', 'index');
    Route::post('', 'store');
    Route::group(['prefix' => '{reaction}'], function () {
        Route::get('', 'show');
        Route::delete('', 'destroy');
        Route::put('', 'update');
    });
});

Route
::controller(NewsController::class)
->prefix('news')
->group(function () {
    Route::get('', 'index');
    Route::post('', 'store');
    Route::group(['prefix' => '{news}'], function () {
        Route::get('', 'show');
        Route::delete('', 'destroy');
        Route::put('', 'update');
    });
});

Route
::prefix('news')
->group(function () {
    Route::group(['prefix' => '{news}/comments', 'controller' => CommentController::class], function () {
        Route::get('', 'index');
        Route::post('', 'store');
        Route::put('{comment}', 'update');
        Route::delete('{comment}', 'destroy');
    });
});

Route
::prefix('news')
->controller(ComplaintController::class)
->group(function () {
    Route::group(['prefix' => '{news}/comments/{comment}/complaints'], function () {
        Route::get('', 'index');
        Route::post('', 'create');
        Route::put('', 'updateStatus');
        Route::get('{complaint}', 'show');
    });
});

Route
::prefix('users/{user}/bans')
->controller(BanController::class)
->group(function () {
    Route::get('', 'index');
    Route::post('', 'create');
    Route::group(['prefix' => '{ban}'], function () {
        Route::get('', 'show');
        Route::put('', 'update');
        Route::delete('', 'delete');
    });
});

Route
::prefix('news/{news}/images')
->controller(ImageController::class)
->group(function () {
    Route::get('', 'showAll');
    Route::get('{image}', 'show');
});
