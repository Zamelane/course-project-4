# Инициализация проекта Laravel (api)
Инструкция по установке бралась с сайта [laravel.su](https://laravel.su/docs/11.x/installation).

## Установка PHP и установщика Laravel
Если у Вас не установлен PHP и Composer, то вы можете их установить командой ниже:
```sh
Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://php.new/install/windows'))
```
Затем, для установки установщика Laravel через Composer:
```sh
composer global require laravel/installer
```

## Создание приложения
Теперь инициализируем проект, выполнив команду установщика laravel в корне репозитория:
```sh
laravel new api
```
Во время установки, во всех вопросах жмём *Enter*, кроме предпоследнего (выбор базы данных, где я выбрал mysql) и последнего (вопрос: хотим ли мы запустить миграции). В нём пишем **No**.

## Решение проблемы биндинга порта
Если при запуске сервера `php artisan serve` у Вас не биндится порт, то выполните следующие действия:
1. Найдите, где расположен файл конфигурации, командой `php --ini`
2. Отредактируйте переменную `variables_order`, задав значение `GPCS`
3. Перезапустите команду `php artisan serve` или, если не сработало, попробуйте перезагрузить компьютер

## Запуск сервера
Чтобы запустить сервер, чтобы он был доступен в браузере, выполните команду `php artisan serve`