<?php
use Illuminate\Pagination\LengthAwarePaginator;
use Illuminate\Http\Resources\Json\JsonResource;
function formatPaginate(LengthAwarePaginator $paginator, JsonResource $resourceFormatter): array
{
    return [
        'page' => $paginator->currentPage(),
        'limit' => $paginator->perPage(),
        'total' => $paginator->total(),
        'data' => $resourceFormatter->collection($paginator->items())
    ];
}
