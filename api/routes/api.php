<?php

namespace App\Http\Controllers\Routes;

use App\Http\Middleware\OptionalAuth;
use Illuminate\Support\Facades\Route;


/*
 * [Авторизация]
 * /login      | post
 * /register   | post
 * /logout     | get
 * /logout/all | get
 */
Route
::controller(AuthController::class)
->group(function () {
    Route::post('login',    'login'   );
    Route::post('register', 'register');
    Route::prefix('logout')
    ->middleware(OptionalAuth::class)
    ->group(function () {
        Route::get('',    'logout'   );
        Route::get('all', 'logoutAll');
    });
});

/*
 * [Пользователи]
 * /users        | post
 * /users/{user} | get, put, delete
 */
Route
::controller(UserController::class)
->prefix('users')
->group(function() {
    // Создание пользователя
    Route::post('', 'store');
    // Работа с собой
    Route::get('me', 'me');
    // Работа с конкретным пользователем
    Route::group(['prefix' => '{user}'], function () {
        Route::get   ('', 'show'   );
        Route::put   ('', 'update' );
        Route::delete('', 'destroy');
    });
});

/*
 * [Тэги]
 * /tags       | get, post
 * /tags/{tag} | get, delete
 */
Route
::controller(TagController::class)
->prefix('tags')
->group(function () {
    Route::get ('', 'index');
    Route::post('', 'store');
    Route::group(['prefix' => '{tag}'], function () {
        Route::get   ('', 'show'   );
        Route::delete('', 'destroy');
    });
});

/*
 * [Города]
 * /cities        | get, post
 * /cities/{city} | get, put, delete
 */
Route
::controller(CityController::class)
->prefix('cities')
->group(function () {
    Route::get ('', 'index');
    Route::post('', 'store');
    Route::group(['prefix' => '{city}'], function () {
        Route::get   ('', 'show'   );
        Route::put   ('', 'update' );
        Route::delete('', 'destroy');
    });
});

/*
 * [Реакции]
 * /reactions            | get, post
 * /reactions/{reaction} | get, put, delete
 */
Route
::controller(ReactionController::class)
->group(function () {
    Route::group(['prefix' => 'reactions'], function () {
        Route::get ('', 'index');
        Route::post('', 'store');
        Route::group(['prefix' => '{reaction}'], function () {
            Route::get   ('', 'show'   );
            Route::put   ('', 'update' );
            Route::delete('', 'destroy');
        });
    });
    Route::group([
        'prefix' => '/news/{news}/reactions',
        'controller' => ReactionController::class
    ], function () {
        Route::post  ('{reaction}', 'userReactionStore'  );
        Route::delete('',           'userReactionDestroy');
    });
});

/*
 * [Новости]
 * /news        | get, post
 * /news/{news} | get, put, delete
 */
Route
::controller(NewsController::class)
->prefix('news')
->group(function () {
    Route::get ('', 'index');
    Route::post('', 'store');
    Route::group(['prefix' => '{news}'], function () {
        Route::get   ('', 'show'   );
        Route::put   ('', 'update' );
        Route::delete('', 'destroy');
    });
});

/*
 * [Комментарии]
 * /news/{news}/comments           | get, post
 * /news/{news}/comments/{comment} | put, delete
 */
Route
::prefix('news')
->group(function () {
    Route::group([
        'prefix' => '{news}/comments',
        'controller' => CommentController::class
    ], function () {
        Route::get   ('', 'index');
        Route::post  ('', 'store');
        Route::group(['prefix' => '{comment}'], function () {
            Route::put   ('', 'update' );
            Route::delete('', 'destroy');
        });
    });
});

/*
 * [Жалобы]
 * /news/{news}/comments/{comment}/complaints             | get, post, put
 * /news/{news}/comments/{comment}/complaints/{complaint} | get
 */
Route
::prefix('news')
->controller(ComplaintController::class)
->group(function () {
    Route::group(['prefix' => '{news}/comments/{comment}/complaints'], function () {
        Route::get ('',            'index'       );
        Route::post('',            'store'       );
        Route::put ('{complaint}', 'updateStatus');
        Route::get ('{complaint}', 'show'        );
    });
});

/*
 * [Баны]
 * /users/{user}/bans       | get, post
 * /users/{user}/bans/{ban} | get, put, delete
 */
Route
::prefix('users/{user}/bans')
->controller(BanController::class)
->group(function () {
    Route::get ('', 'index' );
    Route::post('', 'create');
    Route::group(['prefix' => '{ban}'], function () {
        Route::get   ('', 'show'  );
        Route::put   ('', 'update');
        Route::delete('', 'delete');
    });
});

/*
 * [Изображения]
 * /news/{news}/images         | get
 * /news/{news}/images/{image} | get
 */
Route
::prefix('news/{news}/images')
->controller(ImageController::class)
->group(function () {
    Route::get('',        'showAll');
    Route::get('{image}', 'show'   );
});
