<?php

namespace App\Http\Resources\User;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class FullUserResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        $data               = MinUserResource::make($this)->toArray($request);
        $data['login']      = $this->login;
        $data['email']      = $this->email;
        $data['birthDay']   = $this->birthDay;

        return  $data;
    }
}
