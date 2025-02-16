<?php

namespace App\Http\Controllers\Routes;

use App\Http\Controllers\Controller;
use App\Http\Requests\Image\ImageUploadRequest;
use App\Http\Resources\Image\ImageResource;
use App\Models\Image;
use DateTime;

class ImageController extends Controller
{
    public function upload(ImageUploadRequest $request)
    {
        // Текущая дата
        $date = new DateTime();

        // Извлекаем конкретные числа
        $day   = $date->format('d');
        $month = $date->format('m');
        $year  = $date->format('Y');

        // Сохраняем картинку по своему пути
        $result = Image::savePhoto('image', "images/$year/$month/$day");

        return response()->json(ImageResource::make($result));
    }
}
