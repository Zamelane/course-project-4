<?php

namespace App\Models;

use App\Models\News\NewsImage;
use App\Models\News\News;
use Exception;
use Validator;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Notifications\Notifiable;
use Laravel\Sanctum\HasApiTokens;

class Image extends Model
{
    use HasApiTokens, HasFactory, Notifiable;

    protected $fillable = [
        'path',
        'upload_date'
    ];

    protected $hidden = [];

    /**
     * @param News $news
     * @return array[]
     */
    public static function savePhotos(News $news, $parameter = 'pictures'): array
    {
        $request = request();
        $pictures = $request[$parameter] ?? [];
        $pathToSave = "images/news/$news->id";

        // Успешные и не успешные загрузки
        $errored = [];
        $successful = [];

        foreach ($pictures as $key => $pictureInRequest) {
            $file = $request->file("$parameter.$key");
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
                $extension = $file->getClientOriginalExtension();
                $time = microtime(true);
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
        return [
            'errored' => $errored,
            'successful' => $successful
        ];
    }

    /**
     * @param News $news
     * @param array $ids
     * @return void
     */
    public static function deletePictures(News $news, array $ids): void
    {
        NewsImage::where('news_id', '=', $news->id)->whereIn('image_id', $ids)->delete();
    }

    /**
     * @param $parameter
     * @param $pathToSave
     * @return array|\Illuminate\Database\Eloquent\TModel
     */
    public static function savePhoto($parameter, $pathToSave, $customName = null)
    {
        $file = request()->file("$parameter");
        $filename = $file->getClientOriginalName();
        try {
            // Проверяем, разрешён ли загружаемый тип файла (картинки)
            $validator = Validator::make(request()->all(), [
                $parameter => 'mimes:' . implode(',', config('settings.allowed_upload_mimes'))
            ]);
            if ($validator->fails()) {
                return [
                    'name' => $filename,
                    'message' => 'Validation failed',
                    'errors' => $validator->errors()->toArray()['file']
                ];
            }

            // Проверяем, не слишком ли большое изображение
            $filesize = $file->getSize();
            if (config('settings.max_file_size') < $filesize) {
                return [
                    'name' => $filename,
                    'message' => 'File is too large'
                ];
            }

            // Сохраняем изображение
            $extension = $file->getClientOriginalExtension();
            $time = microtime(true);
            $fileNameSave = ($customName ?: $time) . ".$extension";

            $file->storeAs($pathToSave, $fileNameSave, ['disk' => 'public']);

            return Image::updateOrCreate(
                [
                    'path' => "$pathToSave/$fileNameSave"
                ],
                [
                    'path' => "$pathToSave/$fileNameSave",
                    'upload_date' => now()
                ]);
        } catch (Exception) {
            return [
                'name' => $filename,
                'message' => "Something went wrong"
            ];
        }
    }
}
