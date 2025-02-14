<?php

namespace App\Models\User;

use App\Models\Comment\Complaint;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Notifications\Notifiable;
use Laravel\Sanctum\HasApiTokens;

class Ban extends Model
{
    protected $fillable = [
        'complaint_id',
        'end_date'
    ];

    protected $hidden = [];

    public function complaint(): BelongsTo
    {
        return $this->belongsTo(Complaint::class);
    }
}
