<?php

namespace App\Http\Requests\Complaint;

use App\Http\Requests\ApiRequest;

class ComplaintRequest extends ApiRequest
{
    public function rules(): array
    {
        return [
            'reason_id'      => 'required|integer|exists:reasons,id',
            'description' => 'required|string|min:1|max:255',
        ];
    }
}
