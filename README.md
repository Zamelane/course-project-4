# coursework-2024

Курсовая работа на 4ом курсе

## Настройка серверной части (backend)

Вы можете запустить проект (в том числе для разработки) используя 2 варианта:
1. Установить для разработки программный комплекс ***OSPanel***
2. Установить php, compose и установщик laravel локально в систему

Ниже вы можете выбрать подходящий вам вариант и следовать инструкциям.

### 1 Вариант - установка программного комплекса для разработки (OSPanel)

Необходимо:
* Установить [git](https://git-scm.com/download/win)
* В настойках OSPanel во вкладке "Модули" установить PHP 8.1 или выше и MySQL 5.7 или выше
* В настойках OSPanel во вкладке "Сервер" установить "Свой Path + Win Path" для видимости команды `git`

Откройте консоль OSPanel и введите следующие команды (по очереди):
```bat
cd domains
git clone https://git.zmln.ru/zamelane/course-project-4 menews.ru
cd menews.ru/api
composer update & composer i
copy .env.example .env
php artisan key:generate
php artisan storage:link
php artisan migrate:fresh --seed
```
После можно перезапускать OSPanel для видимости нового домена.

Не забудьте отредактировать `.env`, а именно указать
подключение к базе данных (предварительно создав базу данных с кодировкой `utf8mb4_general_ci`.

Для доступа к веб-сайту просто через домен в корне проекта необходимо создать файл с названием `.htaccess` и заполнить следующим содержимым:
```apacheconf
RewriteEngine on
RewriteRule (.*)? /public/$1
```

### 2 вариант - локальная установка

Для локальной установки можете следовать [инструкциям](./docs/documenting_steps/installation.md).

Затем выберите папку, где будет располагаться репозиторий и откройте его, а затем выполните команды:
```bat
git clone https://git.zmln.ru/zamelane/course-project-4
cd course-project-4/api
composer update & composer i
copy .env.example .env
php artisan key:generate
php artisan storage:link
php artisan migrate:fresh --seed
```
Для запуска сервера, можете использовать `php artisan serve`.

## Связывание символической ссылкой общедоступных файлов (если выполнили команды выше, то пропускайте)
```bat
php artisan storage:link
```
Эта команда создаст символическую ссылку на `storage/app/public` в `public/storage`.

## Обновление

```bat
git pull
composer update & composer i
php artisan migrate:fresh --seed
```

## Подсказки классов при разработке

1. Установите помощника (уже установлен, поэтому можно пропустить):

```bat
composer require --dev barryvdh/laravel-ide-helper
```

2. Сгенерируйте подсказки для моделей:

```bat
php artisan ide-helper:models
php artisan ide-helper:models --reset
```

3. Сгенерируйте подсказки кода для методов фасадов:

```bat
php artisan ide-helper:generate
```

4. Сгенерируйте подсказки по коду для классов-контейнеров:

```bat
php artisan ide-helper:meta
```
