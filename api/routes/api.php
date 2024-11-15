<?php

use App\Http\Controllers\NewsController;
use App\Http\Controllers\ReactionController;
use App\Models\City;
use App\Models\News;
use App\Models\Reaction;
use Illuminate\Support\Facades\Route;
use App\Models\User;
use App\Http\Controllers\AuthController;
use App\Http\Controllers\UserController;
use App\Http\Controllers\TagController;
use App\Http\Controllers\CityController;
use App\Models\Tag;
use App\Http\Middleware\CustomChecker;

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
->middleware(CustomChecker::class)
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

Route
::controller(TagController::class)
->prefix('tags')
->middleware(CustomChecker::class)
->group(function () {
    Route::get('', 'index')->can('showAll', Tag::class);
    Route::post('', 'create')->can('create', Tag::class);
    Route::group(['prefix' => '{tag}'], function () {
        Route::get('', 'show')->can('show', 'tag');
        Route::delete('', 'delete')->can('delete', 'tag');
    });
});

Route
::controller(CityController::class)
->prefix('cities')
->middleware(CustomChecker::class)
->group(function () {
    Route::get('', 'index')->can('showAll', City::class);
    Route::post('', 'create')->can('create', City::class);
    Route::group(['prefix' => '{city}'], function () {
        Route::get('', 'show')->can('show', 'city');
        Route::delete('', 'delete')->can('delete', 'city');
        Route::put('', 'update')->can('update', 'city');
    });
});

Route
::controller(ReactionController::class)
->prefix('reactions')
->middleware(CustomChecker::class)
->group(function () {
    Route::get('', 'index')->can('showAll', Reaction::class);
    Route::post('', 'create')->can('create', Reaction::class);
    Route::group(['prefix' => '{reaction}'], function () {
        Route::get('', 'show')->can('show', 'reaction');
        Route::delete('', 'delete')->can('delete', 'reaction');
        Route::put('', 'update')->can('update', 'reaction');
    });
});

Route
::controller(NewsController::class)
->prefix('news')
->middleware(CustomChecker::class)
->group(function () {
    Route::get('', 'index')->can('showAll', News::class);
    Route::post('', 'create')->can('create', News::class);
    Route::group(['prefix' => '{news}'], function () {
        Route::get('', 'show')->can('show', 'news');
        Route::delete('', 'delete')->can('delete', 'news');
        Route::put('', 'update')->can('update', 'news');
    });
});
