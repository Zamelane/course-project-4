<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Category extends Model
{
    protected $fillable = [
        'name',
        'background_color',
        'image_id'
    ];

    protected $hidden = [
        'created_at',
        'updated_at',
        'image_id'
    ];

    public function image()
    {
        return $this->belongsTo(Image::class);
    }
}
