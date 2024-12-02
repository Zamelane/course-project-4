<?php

namespace App\Http\Controllers\Routes;

use App\Http\Controllers\Controller;
use App\Http\Requests\History\HistoryRequest;
use App\Http\Resources\HistoryView\HistoryViewResource;
use App\Models\HistoryView;
use DateTime;

class HistoryViewController extends Controller
{
    public function index(HistoryRequest $request)
    {
        $user = auth()->user();
        $limit = intval($request->query('limit', 15));
        $readDate = $request->get('date') ?? (new DateTime())->format('Y-m-d');

        $historyQuery = HistoryView
            ::where([
                'read_date' => $readDate,
                'user_id' => $user->id
            ])
            ->orderBy('read_time', 'desc');

        $historyViewPage = $historyQuery->paginate($limit);

        return response()->json(
            formatPaginate($historyViewPage, new HistoryViewResource(null))
        );
    }
}
