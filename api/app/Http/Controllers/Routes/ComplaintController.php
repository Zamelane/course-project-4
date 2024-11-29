<?php

namespace App\Http\Controllers\Routes;

use App\Exceptions\ApiException;
use App\Http\Controllers\Controller;
use App\Http\Requests\Complaint\ComplaintRequest;
use App\Http\Requests\Complaint\ComplaintUpdateStatusRequest;
use App\Http\Resources\Complaint\ComplaintResource;
use App\Models\Comment\Comment;
use App\Models\Comment\Complaint;
use App\Models\News\News;
use App\Models\User\Ban;
use Illuminate\Http\JsonResponse;
use Illuminate\Support\Facades\Validator;

class ComplaintController extends Controller
{
    public function index(News $news, Comment $comment): JsonResponse
    {
        // TODO: не забыть сделать ресурсы !!!
        return response()->json($comment->complaints);
    }

    public function store(ComplaintRequest $request, News $news, Comment $comment): JsonResponse
    {
        if ($comment->checkComplaintExists(auth()->user()))
            throw new ApiException('Complaint exists', 400);

        $complaint = Complaint::create([
            ...$request->validated(),
            'author_user_id' => auth()->user()->id,
            'comment_id' => $comment->id
        ]);

        return response()->json($complaint);
    }

    public function updateStatus(ComplaintUpdateStatusRequest $request, News $news, Comment $comment, Complaint $complaint): JsonResponse
    {
        if ($request->get('status') === 'accepted')
        {
            $res = Validator::make(
                request()->all(),
                ['ban_end_date' => 'required|date|date_format:Y-m-d|after:today']
            );
            if ($res->fails())
                throw new ApiException('Request validation error',422, $res->errors());
            Ban::updateOrCreate(
                ['complaint_id' => $complaint->id],
                ['end_date' => $request->get('ban_end_date')]
            );
        }
        else
            Ban::where('complaint_id', '=', $complaint->id)->delete();

        $complaint->update($request->validated());

        return response()->json(ComplaintResource::make($complaint));
    }

    public function show(News $news, Comment $comment, Complaint $complaint): JsonResponse
    {
        return response()->json(ComplaintResource::make($complaint));
    }
}
