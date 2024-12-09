<?php

namespace App\Http\Resources\User;

use DateTime;
use App\Models\Image;
use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class MinUserResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        $avatar = $this->avatar;
        $urlToAvatar = $this->avatar ? Image::getPathUrl($avatar) : null;
        return [
            'id'        => $this->id,
            'firstName' => $this->firstName,
            'lastName'  => $this->lastName,
            'role'      => $this->role,
            'avatar'    => $urlToAvatar
        ];
    }
}
