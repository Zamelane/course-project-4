<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Foundation\Auth\User as Authenticatable;
use Illuminate\Notifications\Notifiable;
use Laravel\Sanctum\HasApiTokens;
use Ramsey\Collection\Collection;

class User extends Authenticatable
{
    use HasApiTokens, HasFactory, Notifiable;

    protected $fillable = [
        'firstName',
        'lastName',
        'login',
        'password',
        'email',
        'birthDay',
        'role',
        'image_id'
    ];

    protected $hidden = [
        'password',
    ];

    protected function casts(): array
    {
        return [
            'password' => 'hashed',
        ];
    }

    public function isAdministrator(): bool
    {
        return $this->role == 'admin';
    }

    public function avatar(): BelongsTo
    {
        return $this->belongsTo(Image::class);
    }

    public function history_views(int $count = 20, int $offset = 0): Collection
    {
        return News::whereHas('reactions', function ($query) {
            $query->where('user_id', '=', $this->id)->get();
        });
//        return $this->hasManyThrough(News::class, 'history_views')
//            ->where('user_id', '=', $this->id)->offset($offset)->limit($count)->get();
    }
}
