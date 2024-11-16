<?php

namespace App\Http\Controllers;

use App\Http\Resources\Comment\CommentResource;
use App\Models\News;
use Illuminate\Support\Collection;

class CommentController extends Controller
{
    public function index(News $news)
    {
        $paginateResponse = $news->comments()->simplePaginate();
        return response()->json(CommentResource::collection($paginateResponse));
    }
}
