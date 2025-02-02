<?php

namespace App\Http\Controllers\Routes;

use App\Http\Controllers\Controller;
use App\Http\Requests\FavouriteRequest;
use App\Http\Resources\FavouriteResource;
use App\Models\Favourite;
use App\Models\News\News;
use App\Models\User;

class FavouriteController extends Controller
{
    public function index()
    {
        $favourites = Favourite::where([
            'user_id' => auth()->id()
        ])->orderBy('added_date', 'desc')
        ->paginate(15);

        return response()->json(FavouriteResource::collection($favourites));
    }

    public function store(FavouriteRequest $request)
    {
        $favourite = Favourite::firstOrCreate([
            ...$request->validated(),
            'user_id' => auth()->id()
        ], [
            'added_date' => now()
        ]);

        return response()->json(null, 201);
    }

    public function destroy(News $news)
    {
        Favourite::where([
            ["news_id", "=", $news->id],
            ["user_id", "=", auth()->id()]
        ])->delete();

        return response()->json(null, 204);
    }
}
