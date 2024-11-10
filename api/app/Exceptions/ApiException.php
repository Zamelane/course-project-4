<?php

namespace App\Exceptions;

use Illuminate\Http\Exceptions\HttpResponseException;

class ApiException extends HttpResponseException
{
    public function __construct(string $message = '', int $code = 0, $errors = [])
    {
        $body = [
            'code' => $code,
            'message' => $message
        ];

        if (count($errors))
            $body['errors'] = $errors;

        parent::__construct(response($body, $code));
    }
}
