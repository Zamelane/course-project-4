<?php

namespace App\Http\Controllers\Routes;

use App\Http\Controllers\Controller;
use App\Http\Requests\Category\CategoryRequest;
use App\Http\Resources\Category\CategoryResource;
use App\Models\Category;
use App\Models\Image;
use Illuminate\Http\JsonResponse;
use function Psy\debug;

class CategoryController extends Controller
{
    public function index(): JsonResponse
    {
        return response()->json(CategoryResource::collection(Category::all()));
    }

    public function store(CategoryRequest $request): JsonResponse
    {
        $data = $request->validated();
        if (isset($data['image']))
        {
            $image = Image::savePhoto('image', "images/categories");
            if (!isset($image['message']))
                $data['image_id'] = $image->id;
        }

        $category = Category::create($data);

        return response()->json($category, 201);
    }

    public function destroy(Category $category): JsonResponse
    {
        $category->delete();
        return response()->json(null, 204);
    }
}
