<?php

namespace App\Http\Controllers\Routes;

use App\Models\HistoryView;
use App\Models\Image;
use DateTime;
use Validator;
use App\Http\Controllers\Controller;
use App\Http\Requests\News\NewsCreateRequest;
use App\Http\Requests\News\NewsUpdateRequest;
use App\Http\Resources\News\NewsFullResource;
use App\Http\Resources\News\NewsMinResource;
use App\Models\News\News;
use App\Models\News\NewsTag;

class NewsController extends Controller
{
    public function index()
    {
        $query = News::query();

        if (isset(request()->sort))
        {
            switch (request()->sort) {
                case 'FirstOld':
                    $query->orderBy('id', 'asc');
                    break;
                case 'Random':
                    $query->inRandomOrder();
                    break;
                case 'FirstMoreViews':
                    $query->leftJoin('history_views', 'history_views.news_id', '=', 'news.id')
                        ->select('news.*', \DB::raw('COUNT(history_views.user_id) as views_count'))
                        ->groupBy('news.id')
                        ->orderBy('views_count', 'desc');
                    break;
                case 'FirstNew':
                    $query->orderBy('id', 'desc');
            }
        }

        if (isset(request()->search))
            $query->where('title', 'like', '%' . request()->search . '%')
                ->orWhere('content', 'like', '%' . request()->search . '%');

        if (isset(request()->categories))
            $query->whereIn('category_id', explode(',', request()->categories));

        if (request()->has("me")) {
            $user = auth()->user();
            if (isset($user))
                $query->where('news.user_id', '=', $user->id);
        }

        $paginate = $query->paginate(isset(request()->limit) ? request()->limit : 15);

        // TODO: отображать всего количество страниц
        return response()->json([
            "news" => NewsMinResource::collection($paginate),
            "total" => $paginate->total()
        ]);
    }

    public function mostRead()
    {
        // TODO: самое читаемое
    }

    public function topNews()
    {
        // TODO: самая популярная новость
    }

    public function show(News $news)
    {
        $user = auth()->user();
        if ($user) {
            $historyData = [
                'news_id' => $news->id,
                'user_id' => $user->id,
                'read_date' => (new DateTime())->format('Y-m-d'),
            ];
            HistoryView::firstOrCreate($historyData,
                [
                    'read_time' => now()->format('H:i:s')
                ]);
        }

        return response()->json(NewsFullResource::make($news));
    }

    public function store(NewsCreateRequest $request)
    {
        $validatedData = $request->validated();

        if (isset($validatedData['cover']))
        {
            $image = Image::where("hash", $validatedData['cover'])->first();
            if ($image != null)
                $cover_id = $image->id;
        }
        else
            $cover_id = null;

        // Создаём новость
        $news = News::create([
            ...$request->validated(),
            'user_id' => auth()->id(),
            'image_id' => $cover_id
        ]);

        // Привязываем теги
        if ($tags = $request->validated('tags'))
            foreach ($tags as $tag)
                NewsTag::create([
                    'news_id' => $news->id,
                    'tag_id' => $tag
                ]);

        // Привязываем картинки
        /*$picturesHashes = $request->get('pictures') ?? [];

        foreach ($picturesHashes as $hash) {
            $image = Image::where(['hash' => $hash])->first();
            NewsImage::updateOrCreate([
                'news_id' => $news->id,
                'image_id' => $image->id
            ]);
        }*/

        return response()->json([
            'code' => 201,
            'data' => NewsMinResource::make($news)
        ], 201);
    }

    public function update(NewsUpdateRequest $request, News $news)
    {
        $updateData = [];
        $validatedData = $request->validated();

        if (isset($validatedData['title']))
            $updateData['title'] = $validatedData['title'];

        if (isset($validatedData['content']))
            $updateData['content'] = $validatedData['content'];

        if (isset($validatedData['cover']))
        {
            $image = Image::where("hash", $validatedData['cover'])->first();
            if ($image != null)
                $updateData['image_id'] = $image->id;
        }
        else
            $updateData['image_id'] = null;

        if (isset($validatedData['category_id']))
            $updateData['category_id'] = $validatedData['category_id'];

        $news->update($updateData);

        $tags = $request->validated('tags');
        NewsTag::whereNotIn('tag_id', $tags ?: [])->delete();
        if ($tags) {
            foreach ($tags as $tag) {
                NewsTag::updateOrCreate([
                    'news_id' => $news->id,
                    'tag_id' => $tag
                ]);
            }
        }

        // Привязываем картинки
        /*$picturesHashes = $request->get('pictures') ?? [];
        $picturesIds = [];

        foreach ($picturesHashes as $hash) {
            $image = Image::where(['hash' => $hash])->first();
            $picturesIds[] = $image->id;
            NewsImage::updateOrCreate([
                'news_id' => $news->id,
                'image_id' => $image->id
            ]);
        }

        // Удаляем старые картинки
        Image::deletePictures($news, $picturesIds);*/

        return response()->json([
            'code' => 200,
            'data' => NewsFullResource::make($news)
        ]);
    }

    public function destroy(News $news)
    {
        $news->delete();
        return response()->json(null, 204);
    }
}
