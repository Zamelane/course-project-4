<?php

namespace App\Http\Controllers\Routes;

use App\Http\Controllers\Controller;
use App\Models\Complaint;

class ComplaintController extends Controller
{
    protected string|array|null $modelsToReg = Complaint::class;
}
