<?php

namespace App\Http\Controllers;

use App\Http\Controllers\Utils\PolicyMapRegister;

abstract class Controller extends \Illuminate\Routing\Controller
{
    use PolicyMapRegister;
}
