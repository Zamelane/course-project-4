<?php

namespace App\Http\Controllers\Routes;

use App\Http\Controllers\Controller;
use App\Http\Requests\Reaction\ReactionCreateRequest;
use App\Http\Requests\Reaction\ReactionUpdateRequest;
use App\Models\Reaction;
use Illuminate\Http\JsonResponse;

class ReactionController extends Controller
{
    public function index(): JsonResponse
    {
        return response()->json(Reaction::all());
    }

    public function show(?Reaction $reaction): JsonResponse
    {
        return response()->json($reaction);
    }

    public function create(ReactionCreateRequest $request): JsonResponse
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

    public function delete(Reaction $reaction): JsonResponse
    {
        $reaction->delete();
        return response()->json(null, 204);
    }
}
