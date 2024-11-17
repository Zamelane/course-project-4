<?php

namespace App\Http\Controllers;

use App\Http\Controllers\Utils\PolicyMapRegister;
use App\Models\User;

abstract class Controller extends \Illuminate\Routing\Controller
{
    use PolicyMapRegister;
}
