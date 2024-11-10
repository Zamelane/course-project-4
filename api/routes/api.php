<?php
use Illuminate\Support\Facades\Route;
use \App\Http\Controllers\AuthController;

Route::controller(AuthController::class)
    ->group(function ($auth) {
        $auth->post('login', 'login');
    });
