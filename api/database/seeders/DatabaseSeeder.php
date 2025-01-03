<?php

namespace Database\Seeders;

use App\Models\Category;
use App\Models\City;
use App\Models\Comment\Comment;
use App\Models\News\News;
use App\Models\Reaction;
use App\Models\Reason;
use App\Models\Tag;
use App\Models\User;
use Illuminate\Database\Seeder;

// use Illuminate\Database\Console\Seeds\WithoutModelEvents;

class DatabaseSeeder extends Seeder
{
    private $cities;
    private $tags;
    private $reasons;
    private $reactions;
    private $users;

    public function run(): void
    {
        // User::factory(10)->create();

        User::factory()->create([
            'login' => 'test',
            'password' => 'test',
        ]);

        $reader = User::factory()->create([
            'login' => 'reporter',
            'password' => 'reporter',
            'role' => 'reporter'
        ]);

        User::factory()->create([
            'login' => 'admin',
            'password' => 'admin',
            'role' => 'admin'
        ]);

        $this->users = User::factory()->createMany(10);

        $this->cities = City::factory()->createMany(10);
        $this->tags = Tag::factory()->createMany(10);
        $this->reasons = Reason::factory()->createMany(10);
        $this->reactions = Reaction::factory()->createMany(10);

        $this->make_categories();

        for($i = 0; $i < 10; $i++)
            $this->make_news($reader);
    }

    private function make_news(User $reader)
    {
        $news = News::factory()->create([
            'title' => 'Заголовок важной новости',
            'content' => 'Это какая-то очень важная новость',
            'city_id' => $this->cities->random()->id,
            'user_id' => $reader->id
        ]);

        $comments = Comment::factory(10)->create([
            'news_id' => $news->id
        ]);
    }

    private function make_categories()
    {
        $categories = [
            "Business",
            "Food & Culture",
            "Office Productivity",
            "Finance & Accounting",
            "IT & Software",
            "Office Productivity",
            "Personal Development",
            "Design",
            "Lifestyle",
            "Photography & Video",
            "Health & Fitness",
            "Development"
        ];

        foreach($categories as $category)
            Category::create([
                'name' => $category,
                'accent_color' => '000000',
                'background_color' => 'ffffff'
            ]);
    }
}
