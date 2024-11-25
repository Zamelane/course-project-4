<?php

namespace Database\Factories;

use App\Models\News\News;
use Illuminate\Database\Eloquent\Factories\Factory;

/**
 * @extends \Illuminate\Database\Eloquent\Factories\Factory<\App\Models\News\News>
 */
class NewsFactory extends Factory
{
    protected $model = News::class;
    /**
     * Define the model's default state.
     *
     * @return array<string, mixed>
     */
    public function definition(): array
    {
        return [
            'create_date' => $this->faker->dateTimeBetween('-1 years', 'now'),
            'update_date' => null
        ];
    }
}
