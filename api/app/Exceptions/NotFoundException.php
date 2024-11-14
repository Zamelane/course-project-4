<?php

namespace App\Exceptions;

class NotFoundException extends ApiException
{
    public function __construct($class = null)
    {
        $message = ucfirst(($class ? class_basename($class) . ' ' : '') . 'not found');
        parent::__construct(throw new ApiException($message, 404));
    }
}
