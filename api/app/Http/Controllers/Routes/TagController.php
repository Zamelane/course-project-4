<?php

namespace App\Http\Controllers\Routes;

use App\Http\Controllers\Controller;
use App\Http\Requests\Tag\TagCreateRequest;
use App\Models\Tag;
use Illuminate\Http\JsonResponse;

class TagController extends Controller
{
    public function index(): JsonResponse
    {
        return response()->json(Tag::all());
    }

    public function store(TagCreateRequest $request): JsonResponse
    {
        $tag = Tag::create($request->validated());
        return response()->json([
            'code' => 201,
            'data' => $tag
        ]);
    }

    public function show(Tag $tag): JsonResponse
    {
        return response()->json($tag);
    }

    public function destroy(Tag $tag): JsonResponse
    {
        $tag->delete();
        return response()->json(null, 204);
    }
}
