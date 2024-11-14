<?php

namespace App\Exceptions;

use Exception;

class UnauthorizedException extends Exception
{
    public function __construct()
    {
        parent::__construct(throw new ApiException('Unauthorized', 401));
    }
}
