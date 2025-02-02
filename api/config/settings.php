<?php
return [
    'allowed_upload_mimes' => explode(',', env('ALLOWED_UPLOAD_MIMES', 'jpeg,jpg,png,gif,svg')),
    'max_file_size' => env('MAX_FILE_SIZE', 1024 * 1024 * 5) // 5 MiB
];
