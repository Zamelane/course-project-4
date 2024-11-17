<?php

namespace App\Http\Controllers\Routes;

use App\Http\Controllers\Controller;
use App\Http\Requests\Tag\TagCreateRequest;
use App\Models\Tag;
use App\Models\User;
use App\Http\Controllers\Utils\MethodPolicyType;
use Illuminate\Http\JsonResponse;

class TagController extends Controller
{
    public function __construct()
    {
        $this->regModels(Tag::class);
        $this->regAbility('index', MethodPolicyType::Without)
             ->regAbility('create');
    }
    public function index(): JsonResponse
    {
        return response()->json(Tag::all());
    }

    public function create(TagCreateRequest $request): JsonResponse
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

    public function delete(Tag $tag): JsonResponse
    {
        $tag->delete();
        return response()->json(null, 204);
    }
}
