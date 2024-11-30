<?php
return [
    'allowed_upload_mimes' => explode(',', env('ALLOWED_UPLOAD_MIMES', 'jpeg,jpg,png,gif')),
    'max_file_size' => env('MAX_FILE_SIZE', 1024 * 1024 * 7) // 7 MiB
];
