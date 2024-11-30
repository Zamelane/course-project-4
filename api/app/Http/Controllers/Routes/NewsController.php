<?php

namespace App\Http\Controllers\Routes;

use App\Models\Image;
use App\Models\News\NewsImage;
use Exception;
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
        // TODO: отображать всего количество страниц
        return response()->json(NewsMinResource::collection(News::simplePaginate()));
    }

    public function show(News $news)
    {
        return response()->json(NewsFullResource::make($news));
    }

    public function store(NewsCreateRequest $request)
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

        $pictures = $request->pictures ?? [];
        $pathToSave = "images/news/$news->id";

        // Успешные и не успешные загрузки
        $errored = [];
        $successful = [];

        foreach ($pictures as $key => $pictureInRequest) {
            $file = $request->file("pictures.$key");
            $filename = $file->getClientOriginalName();
            try {
                // Проверяем, разрешён ли загружаемый тип файла (картинки)
                $validator = Validator::make($pictures, [
                    $key => 'mimes:' . implode(',', config('settings.allowed_upload_mimes'))
                ]);
                if ($validator->fails()) {
                    $errored[] = [
                        'name' => $filename,
                        'message' => 'Validation failed',
                        'errors' => $validator->errors()->toArray()['file']
                    ];
                    continue;
                }

                // Проверяем, не слишком ли большое изображение
                $filesize = $file->getSize();
                if (config('settings.max_file_size') < $filesize) {
                    $errored[] = [
                        'name' => $filename,
                        'message' => 'File is too large'
                    ];
                    continue;
                }

                // Сохраняем изображение
                $extension    = $file->getClientOriginalExtension();
                $time         = microtime(true);
                $fileNameSave = "$time.$extension";

                $file->storeAs($pathToSave, $fileNameSave, ['disk' => 'public']);

                $result = Image::create([
                    'path' => "$pathToSave/$fileNameSave",
                    'upload_date' => now()
                ]);

                // TODO: убрать дублирование картинок
                NewsImage::create([
                    'news_id' => $news->id,
                    'image_id' => $result->id
                ]);

                $successful[] = [
                    'url' => asset($result->path),
                    'upload_date' => $result->upload_date
                ];
            } catch (Exception) {
                $errored[] = [
                    'name' => $filename,
                    'message' => "Something went wrong"
                ];
            }
        }

        return response()->json([
            'code' => 201,
            'data' => NewsMinResource::make($news),
            'uploadStatus' => [
                'errored' => $errored,
                'successful' => $successful
            ]
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
        if ($tags) {
            foreach ($tags as $tag) {
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

    public function destroy(News $news)
    {
        $news->delete();
        return response()->json(null, 204);
    }
}
