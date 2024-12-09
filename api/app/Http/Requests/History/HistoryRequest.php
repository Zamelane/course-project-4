<?php

namespace App\Http\Requests\History;

use App\Http\Requests\ApiRequest;

class HistoryRequest extends ApiRequest
{
    public function rules(): array
    {
        return [
            'date' => 'date'
        ];
    }
}
