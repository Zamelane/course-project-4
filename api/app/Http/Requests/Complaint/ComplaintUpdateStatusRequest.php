<?php

namespace App\Http\Requests\Complaint;

use App\Http\Requests\ApiRequest;

class ComplaintUpdateStatusRequest extends ApiRequest
{
    public function rules(): array
    {
        return [
            'status' => 'required|string|in:pending,accepted,rejected'
        ];
    }
}
