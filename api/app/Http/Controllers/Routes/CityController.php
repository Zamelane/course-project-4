<?php

namespace App\Http\Controllers\Routes;

use App\Http\Controllers\Controller;
use App\Http\Requests\City\CityRequest;
use App\Models\City;
use Illuminate\Http\JsonResponse;

class CityController extends Controller
{
    protected string|array|null $modelsToReg = City::class;
    public function index(): JsonResponse
    {
        return response()->json(City::all());
    }

    public function store(CityRequest $request): JsonResponse
    {
        $city = City::create($request->validated());
        return response()->json([
            'code' => 201,
            'data' => $city
        ]);
    }

    public function show(City $city): JsonResponse
    {
        return response()->json($city);
    }

    public function destroy(City $city): JsonResponse
    {
        $city->delete();
        return response()->json(null, 204);
    }

    public function update(CityRequest $request, City $city): JsonResponse
    {
        $city->update($request->validated());
        return response()->json([
            'code' => 200,
            'data' => $city
        ]);
    }
}
