<?php

use App\Http\Controllers\Routes\AuthController;
use App\Http\Controllers\Routes\BanController;
use App\Http\Controllers\Routes\CityController;
use App\Http\Controllers\Routes\CommentController;
use App\Http\Controllers\Routes\ComplaintController;
use App\Http\Controllers\Routes\ImageController;
use App\Http\Controllers\Routes\NewsController;
use App\Http\Controllers\Routes\ReactionController;
use App\Http\Controllers\Routes\TagController;
use App\Http\Controllers\Routes\UserController;
use App\Http\Middleware\CustomChecker;
use App\Models\Ban;
use App\Models\City;
use App\Models\Comment;
use App\Models\Complaint;
use App\Models\Image;
use App\Models\News;
use App\Models\Reaction;
use App\Models\Tag;
use App\Models\User;
use Illuminate\Support\Facades\Route;

Route
::controller(AuthController::class)
->group(function () {
    Route::post('login', 'login');
    Route::post('register', 'register');
    Route::prefix('logout')
    ->middleware(CustomChecker::class)
    ->group(function () {
        Route::get('', 'logout')->can('logout', User::class);
        Route::get('all', 'logoutAll')->can('logout', User::class);
    });
});


Route
::controller(UserController::class)
->prefix('users')
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
->group(function () {
    Route::get('', 'index');
    Route::post('', 'create');
    Route::group(['prefix' => '{tag}'], function () {
        Route::get('', 'show')->can('show', 'tag');
        Route::delete('', 'delete')->can('delete', 'tag');
    });
});

Route
::controller(CityController::class)
->prefix('cities')
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
->group(function () {
    Route::get('', 'index')->can('showAll', News::class);
    Route::post('', 'create')->can('create', News::class);
    Route::group(['prefix' => '{news}'], function () {
        Route::get('', 'show')->can('show', 'news');
        Route::delete('', 'delete')->can('delete', 'news');
        Route::put('', 'update')->can('update', 'news');
    });
});

Route
::prefix('news')
->group(function () {
    Route::group(['prefix' => '{news}/comments', 'controller' => CommentController::class], function () {
        Route::get('', 'index')->can('showAll', Comment::class);
        Route::post('', 'create')->can('create', Comment::class);
        Route::put('{comment}', 'update')->can('update', 'comment');
        Route::delete('{comment}', 'delete')->can('delete', 'comment');
    });
});

Route
::prefix('news')
->controller(ComplaintController::class)
->group(function () {
    Route::group(['prefix' => '{news}/comments/{comment}/complaints'], function () {
        Route::get('', 'index')->can('showAll', Complaint::class);
        Route::post('', 'create')->can('create', Complaint::class);
        Route::put('', 'updateStatus')->can('updateStatus', Complaint::class);
        Route::get('{complaint}', 'show')->can('show', 'complaint');
    });
});

Route
::prefix('users/{user}/bans')
->controller(BanController::class)
->group(function () {
    Route::get('', 'index')->can('showAll', Ban::class);
    Route::post('', 'create')->can('create', Ban::class);
    Route::group(['prefix' => '{ban}'], function () {
        Route::get('', 'show')->can('show', 'ban');
        Route::put('', 'update')->can('update', 'ban');
        Route::delete('', 'delete')->can('delete', 'ban');
    });
});

Route
::prefix('news/{news}/images')
->controller(ImageController::class)
->group(function () {
    Route::get('', 'showAll')->can('showAll', Image::class);
    Route::get('{image}', 'show')->can('show', 'image');
});
