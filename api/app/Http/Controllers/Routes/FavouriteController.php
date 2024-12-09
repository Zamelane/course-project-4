<?php

namespace App\Http\Controllers\Routes;

use App\Http\Controllers\Controller;
use App\Http\Requests\FavouriteRequest;
use App\Http\Resources\FavouriteResource;
use App\Models\Favourite;

class FavouriteController extends Controller
{
    public function index()
    {
        $favourites = Favourite::where([
            'user_id' => auth()->id()
        ])->orderBy('added_date', 'desc')
        ->paginate(15);

        return response()->json(formatPaginate($favourites, new FavouriteResource(null)));
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
}
