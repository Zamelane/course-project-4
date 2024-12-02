<?php

namespace App\Models;

use App\Models\News\NewsImage;
use App\Models\News\News;
use Exception;
use Validator;
use DateTime;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Notifications\Notifiable;
use Laravel\Sanctum\HasApiTokens;

class Image extends Model
{
    use HasApiTokens, HasFactory, Notifiable;

    public $timestamps = false;

    protected $fillable = [
        'hash',
        'extension',
        'upload_date'
    ];

    /**
     * @param News $news
     * @param array $ids
     * @return void
     */
    public static function deletePictures(News $news, array $ids): void
    {
        NewsImage::where('news_id', '=', $news->id)->whereNotIn('image_id', $ids)->delete();
    }

    /**
     * @param DateTime $date
     * @return string
     */
    public static function getPathByDate(DateTime $date = new DateTime()): string
    {
        // Извлекаем конкретные числа
        $day   = $date->format('d');
        $month = $date->format('m');
        $year  = $date->format('Y');

        return "images/$year/$month/$day";
    }

    public static function getPathUrl(string|Image $value): string
    {
        $image = $value instanceof Image
            ? $value
            : Image::where(['hash' => $value])->first();

        return 'storage/' . Image::getPathByDate(new DateTime($image->upload_date)) . "/$image->hash.$image->extension";
    }

    /**
     * @param $parameter - Имя поля в запросе, хранящего изображение
     * @param $pathToSave - Куда сохраняем картинку
     * @return array|\Illuminate\Database\Eloquent\TModel
     */
    public static function savePhoto($parameter, $customName = null)
    {
        $pathToSave = Image::getPathByDate();

        // Обрабатываем загрузку файла
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

            // Подготавливаем уникальность изображения
            $tmpPath = $file->getRealPath();
            $extension = $file->getClientOriginalExtension();
            $hash = hash_file('xxh3', $tmpPath);

            // Сохраняем изображение
            $fileNameSave = "$hash.$extension";

            $file->storeAs($pathToSave, $fileNameSave, ['disk' => 'public']);

            $imageExists = Image::where('hash', '=', $hash)->first();
            if (!$imageExists)
                $imageExists = Image::create([
                    'hash' => $hash,
                    'extension' => $extension,
                    'upload_date' => date('Y-m-d')
                ]);

            return $imageExists;
        } catch (Exception) {
            return [
                'name' => $filename,
                'message' => "Something went wrong"
            ];
        }
    }
}
