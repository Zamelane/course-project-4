<?php

namespace Database\Seeders;

use App\Models\Category;
use App\Models\Image;
use Illuminate\Database\Seeder;

class CategoriesSeeder extends Seeder
{
    public function run(): void
    {
        $categories = [
            [
                "name" => "Business",
                "background_color" => "E4F9E0",
                "image" => "Handshake_green.svg"
            ],
            [
                "name" => "Food & Culture",
                "background_color" => "E6F0FD",
                "image" => "ForkKnife.svg"
            ],
            [
                "name" => "Office Productivity",
                "background_color" => "FFE5E5",
                "image" => "Coffee.svg"
            ],
            [
                "name" => "Finance & Accounting",
                "background_color" => "FFF8E0",
                "image" => "CreditCard.svg"
            ],
            [
                "name" => "IT & Software",
                "background_color" => "E6F0FD",
                "image" => "Buildings.svg"
            ],
            [
                "name" => "Office Productivity",
                "background_color" => "F7E6FD",
                "image" => "BugDroid.svg"
            ],
            [
                "name" => "Personal Development",
                "background_color" => "F5F7FA",
                "image" => "Handshake_gray.svg"
            ],
            [
                "name" => "Design",
                "background_color" => "E4F9E0",
                "image" => "PenNib.svg"
            ],
            [
                "name" => "Lifestyle",
                "background_color" => "FFF8E0",
                "image" => "Notepad.svg"
            ],
            [
                "name" => "Photography & Video",
                "background_color" => "E6F0FD",
                "image" => "Camera.svg"
            ],
            [
                "name" => "Health & Fitness",
                "background_color" => "E4F9E0",
                "image" => "Heartbeat.svg"
            ],
            [
                "name" => "Development",
                "background_color" => "FFE5E5",
                "image" => "Bug.svg"
            ],
        ];

        // Сохраняем картинки в storage и базе
        foreach ($categories as &$category) {
            $image = Image::saveLocalPhoto("database/seeders/images/" . $category["image"], "images/categories");
            $category["image_id"] = $image->id;
            unset($category["image"]);
        }

        // Сохраняем категории в базе
        foreach ($categories as $category)
            Category::create($category);
    }
}
