<?php

namespace Database\Seeders;

use App\Models\Category;
use App\Models\Image;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\Log;

class CategoriesSeeder extends Seeder
{
    public function run(): void
    {
        $categories = [
            [ "name" => "Бизнес", "background_color" => "E4F9E0", "image" => "Handshake_green.svg" ],
            [ "name" => "Еда & Культура", "background_color" => "E6F0FD", "image" => "ForkKnife.svg" ],
            [ "name" => "Офис & Работа", "background_color" => "FFE5E5", "image" => "Coffee.svg" ],
            [ "name" => "Экономика", "background_color" => "FFF8E0", "image" => "CreditCard.svg" ],
            [ "name" => "Работа", "background_color" => "E6F0FD", "image" => "Buildings.svg" ],
            [ "name" => "IT & Технологии", "background_color" => "F7E6FD", "image" => "BugDroid.svg" ],
            [ "name" => "Разработки", "background_color" => "F5F7FA", "image" => "Handshake_gray.svg" ],
            [ "name" => "Дизайн", "background_color" => "E4F9E0", "image" => "PenNib.svg" ],
            [ "name" => "Образ жизни", "background_color" => "FFF8E0", "image" => "Notepad.svg" ],
            [ "name" => "Фото & Видео", "background_color" => "E6F0FD", "image" => "Camera.svg" ],
            [ "name" => "Здоровье & Фитнес", "background_color" => "E4F9E0", "image" => "Heartbeat.svg" ],
            [ "name" => "Строительство", "background_color" => "FFE5E5", "image" => "Bug.svg" ],
        ];

        // Сохраняем картинки в storage и базе
        foreach ($categories as &$category) {
            $image = Image::saveLocalPhoto("database/seeders/images/" . $category["image"], "images/categories");
            $category["image_id"] = $image->id;
            unset($category["image"]);
        }

        // Сохраняем категории в базе
        foreach ($categories as &$category)
            Category::create($category);
    }
}
