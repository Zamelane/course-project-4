<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('history_views', function (Blueprint $table) {
            $table->foreignId('user_id')->constrained()->cascadeOnUpdate()->cascadeOnDelete();
            $table->foreignId('news_id')->constrained()->cascadeOnUpdate()->cascadeOnDelete();
            $table->date('read_date');
            $table->time('read_time');
            $table->primary(['user_id', 'news_id', 'read_date']);
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('history_views');
    }
};
