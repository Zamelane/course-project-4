<?php

namespace App\Http\Controllers\Routes;

use App\Http\Controllers\Controller;
use App\Http\Resources\Image\ImageResource;
use App\Models\Image;
use App\Models\News\News;

class ImageController extends Controller
{
    public function index(News $news)
    {
        $images = $news->images;
        return response()->json(ImageResource::collection($images));
    }
}
