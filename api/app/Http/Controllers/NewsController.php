<?php

namespace App\Http\Controllers;

use App\Http\Requests\News\NewsCreateRequest;
use App\Http\Requests\News\NewsUpdateRequest;
use App\Http\Resources\News\NewsFullResource;
use App\Http\Resources\News\NewsMinResource;
use App\Models\News;
use App\Models\NewsTag;
use App\Models\User;
use Illuminate\Http\Request;

class NewsController extends Controller
{
    public function index()
    {
        // TODO: отображать всего количество страниц
        return response()->json(NewsMinResource::collection(News::simplePaginate()));
    }

    public function show(News $news)
    {
        return response()->json(NewsFullResource::make($news));
    }

    public function create(NewsCreateRequest $request)
    {
        $news = News::create([
            ...$request->validated(),
            'user_id' => auth()->id()
        ]);

        if ($tags = $request->validated('tags'))
            foreach ($tags as $tag)
                NewsTag::create([
                    'news_id' => $news->id,
                    'tag_id' => $tag
                ]);

        return response()->json([
            'code' => 201,
            'data' => NewsMinResource::make($news)
        ], 201);
    }

    public function update(NewsUpdateRequest $request, News $news)
    {
        $news->update([
            'title' => $request->validated('title'),
            'description' => $request->validated('description')
        ]);

        $tags = $request->validated('tags');
        NewsTag::whereNotIn('tag_id', $tags ?: [])->delete();
        if ($tags)
        {
            foreach ($tags as $tag)
            {
                NewsTag::updateOrCreate([
                    'news_id' => $news->id,
                    'tag_id' => $tag
                ]);
            }
        }

        return response()->json([
            'code' => 200,
            'data' => NewsMinResource::make($news)
        ]);
    }

    public function delete(News $news)
    {
        $news->delete();
        return response()->json(null, 204);
    }
}
