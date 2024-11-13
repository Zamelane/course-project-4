<?php

namespace App\Http\Resources\User;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class MinUserResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        return [
            'id'        => $this->id,
            'firstName' => $this->firstName,
            'lastName'  => $this->lastName,
            'role'      => $this->role,
            'avatar'    => 'TODO: не забыть сделать'
        ];
    }
}
