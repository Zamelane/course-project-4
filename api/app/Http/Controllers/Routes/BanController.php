<?php

namespace App\Http\Controllers\Routes;

use App\Http\Controllers\Controller;
use App\Models\Ban;

class BanController extends Controller
{
    protected string|array|null $modelsToReg = Ban::class;
}
