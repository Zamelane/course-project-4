<?php

namespace App\Http\Controllers\Routes;

use App\Http\Controllers\Controller;
use App\Http\Requests\Comment\CommentCreateRequest;
use App\Http\Resources\Comment\CommentResource;
use App\Models\Comment\Comment;
use App\Models\News\News;
use Illuminate\Http\JsonResponse;

class CommentController extends Controller
{
    public function index(News $news): JsonResponse
    {
        $paginateResponse = $news->comments()->simplePaginate();
        return response()->json(CommentResource::collection($paginateResponse));
    }

    public function store(CommentCreateRequest $request, News $news): JsonResponse
    {
        $comment = Comment::create([
            ...$request->validated(),
            'user_id' => auth()->id(),
            'news_id' => $news->id
        ]);
        return response()->json(CommentResource::make($comment), 201);
    }

    public function update(CommentCreateRequest $request, News $news, Comment $comment): JsonResponse
    {
        $comment->update($request->validated());
        return response()->json([
            'code' => 200,
            'data' => $comment
        ]);
    }

    public function destroy(News $news, Comment $comment): JsonResponse
    {
        $comment->delete();
        return response()->json(null, 204);
    }
}
