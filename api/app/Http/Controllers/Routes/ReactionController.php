<?php

namespace App\Http\Controllers\Routes;

use App\Http\Controllers\Controller;
use App\Http\Requests\Reaction\ReactionCreateRequest;
use App\Http\Requests\Reaction\ReactionUpdateRequest;
use App\Http\Resources\Reaction\ReactionResource;
use App\Models\News\News;
use App\Models\Reaction;
use Illuminate\Http\JsonResponse;

class ReactionController extends Controller
{
    public function index(): JsonResponse
    {
        return response()->json(Reaction::all());
    }

    public function show(Reaction $reaction): JsonResponse
    {
        return response()->json($reaction);
    }

    public function store(ReactionCreateRequest $request): JsonResponse
    {
        $reaction = Reaction::create($request->validated());
        return response()->json([
            'code' => 201,
            'data' => $reaction
        ], 201);
    }

    public function update(ReactionUpdateRequest $request, Reaction $reaction): JsonResponse
    {
        $reaction->update($request->validated());
        return response()->json([
            'code' => 200,
            'data' => $reaction
        ]);
    }

    public function destroy(Reaction $reaction): JsonResponse
    {
        $reaction->delete();
        return response()->json(null, 204);
    }

    public function userReactionStore(News $news, Reaction $reaction)
    {
        $reaction->setReaction(auth()->user(), $news);
        return response()->json(ReactionResource::make($reaction), 201);
    }

    public function userReactionDestroy(News $news)
    {
        Reaction::deleteReaction(auth()->user(), $news);
        return response()->json(null, 204);
    }
}
