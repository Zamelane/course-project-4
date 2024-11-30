<?php
return [
    'settings.allowed_upload_mimes' => explode(',', getenv('ALLOWED_UPLOAD_MIMES', 'jpeg,jpg,png,gif')),
    'settings.max_file_size' => getenv('MAX_FILE_SIZE', 1024 * 1024 * 7) // 7 MiB
];
