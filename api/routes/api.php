<?php
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\AuthController;
use App\Http\Controllers\UserController;

Route
::controller(AuthController::class)
->group(function (Route $auth) {
    $auth->post('login', 'login');
    $auth->post('register', 'register');
    $auth->prefix('logout')
    ->group(function ($logout) {
        $logout->get('', 'logout');
        $logout->get('all', 'logoutAll');
    });
});


Route
::controller(UserController::class)
->prefix('users')
->group(function($users) {
    $users->post('create', 'create')
        ->can('create', 'user');
});
