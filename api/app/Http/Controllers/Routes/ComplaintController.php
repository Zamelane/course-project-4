<?php

namespace App\Http\Controllers\Routes;

use App\Exceptions\ApiException;
use App\Http\Controllers\Controller;
use App\Http\Requests\Complaint\ComplaintRequest;
use App\Http\Requests\Complaint\ComplaintUpdateStatusRequest;
use App\Models\Comment;
use App\Models\Complaint;
use App\Models\News;

class ComplaintController extends Controller
{
    protected string|array|null $modelsToReg = Complaint::class;
    protected array $customAbilityMap = [
        'updateStatus' => 'updateStatus'
    ];

    protected string $class = __CLASS__;

    protected string|null $parameter = Complaint::class . ',comment';

    protected bool $clearWithoutModels = true;

    public function index(News $news, Comment $comment)
    {
        // TODO: не забыть сделать ресурсы !!!
        return response()->json($comment->complaints);
    }

    public function store(ComplaintRequest $request, News $news, Comment $comment)
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

    public function updateStatus(ComplaintUpdateStatusRequest $request, News $news, Comment $comment, Complaint $complaint)
    {
        $complaint->update([
            $request->validated()
        ]);

        return response()->json($complaint);
    }

    public function show(News $news, Comment $comment, Complaint $complaint)
    {
        return response()->json($complaint);
    }
}
