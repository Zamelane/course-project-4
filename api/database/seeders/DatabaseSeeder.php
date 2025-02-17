<?php

namespace Database\Seeders;

use App\Models\Category;
use App\Models\City;
use App\Models\Comment\Comment;
use App\Models\Image;
use App\Models\News\News;
use App\Models\Reaction;
use App\Models\Reason;
use App\Models\Tag;
use App\Models\User;
use Illuminate\Database\Seeder;

// use Illuminate\Database\Console\Seeds\WithoutModelEvents;

class DatabaseSeeder extends Seeder
{
    private $cities;
    private $tags;
    private $reasons;
    private $reactions;
    private $users;

    public function run(): void
    {
        $this->call(CategoriesSeeder::class);
        // User::factory(10)->create();

        User::factory()->create([
            'login' => 'test',
            'password' => 'test',
        ]);

        $reader = User::factory()->create([
            'login' => 'reporter',
            'password' => 'reporter',
            'role' => 'reporter'
        ]);

        User::factory()->create([
            'login' => 'admin',
            'password' => 'admin',
            'role' => 'admin'
        ]);

        $this->users = User::factory()->createMany(10);

        $this->cities = City::factory()->createMany(10);
        $this->tags = Tag::factory()->createMany(10);
        $this->reasons = Reason::factory()->createMany(10);
        $this->reactions = Reaction::factory()->createMany(10);

        //for($i = 0; $i < 10; $i++)
        $this->make_news($reader);
    }

    private function make_news(User $reporter)
    {
        /*$news = News::factory()->create([
            'title' => 'Заголовок важной новости',
            'content' => 'Это какая-то очень важная новость',
            'city_id' => $this->cities->random()->id,
            'user_id' => $reader->id,
            'category_id' => Category::inRandomOrder()->first()->id
        ]);

        $comments = Comment::factory(10)->create([
            'news_id' => $news->id
        ]);*/

        $image2_1 = $this->UploadImage("news2_1.jpg")["path"];
        $image4_1 = $this->UploadImage("news4_1.jpeg")["path"];
        $image5_1 = $this->UploadImage("news5_1.jpg")["path"];
        $image5_2 = $this->UploadImage("news5_2.jpg")["path"];
        $image5_3 = $this->UploadImage("news5_3.jpg")["path"];
        $image5_4 = $this->UploadImage("news5_4.jpg")["path"];
        $image5_5 = $this->UploadImage("news5_5.jpg")["path"];
        $image7_1 = $this->UploadImage("news7_1.jpg")["path"];

        $news = [
            [
                'title' => 'Все те же деньги: томский эксперт развеяла мифы о цифровом рубле',
                'content' => "Центробанк (Банк России, ЦБ) зафиксировал массовую рассылку \"пугалок\", касающихся цифрового рубля. Например, в социальные сети и мессенджеры жителям Томской области стали приходить сообщения о якобы принудительном переходе на цифровые рубли, которые люди не смогут тратить. Мифы об использовании цифрового рубля – в материале РИА Томск.\n# Что такое цифровой рубль?\n\nРанее сообщалось, что цифровой рубль – это новая форма обычных рублей, только в электронном виде. По планам ЦБ, физические лица смогут пользоваться цифровыми рублями с помощью привычных приложений любого банка, но не сразу. В широкое обращение такой рубль поступит только в 2025-2027 годах.\n\nВ настоящее время, по данным регулятора, к пилотному проекту по внедрению цифрового рубля присоединились 15 банков России.\n\n# МИФ 1. Цифровой рубль заменит наличные и безналичные. Все расчеты скоро станут только цифровыми, а наличные деньги исчезнут\n\nПо словам Петроченко, цифровой рубль не заменит, а дополнит наличные и безналичные. Внедрение цифровых денег – это лишь еще один способ расчетов. Никто не заставит людей отказываться от наличных, если они удобны. Никто и не собирается принудительно переводить людей на цифровые рубли.\n\n\"Выбор, чем пользоваться в той или иной ситуации – наличными, безналичными или цифровыми рублями, останется за человеком, в зависимости от его желания и потребностей... Использование цифровых рублей будет абсолютно добровольным. В итоге человек сам будет выбирать, каким инструментом ему пользоваться\", – подчеркнула она.\n\n# МИФ 2. Цифровой рубль – это криптовалюта, его курс будет постоянно меняться\n\nЦифровой рубль – это официальная национальная валюта, подконтрольная ЦБ. Его стоимость всегда будет равна одному обычному рублю. В отличие от криптовалют цифровой рубль стабилен и защищен от резких скачков курса. Он не требует майнинга и не зависит от спроса на рынках.Кроме того, у цифрового рубля нет срока годности. Его курс по отношению к иностранной валюте формируется вне зависимости от его формы: цифровой, наличной и безналичной.\n\n\nВ связи с этим никаких специальных заявлений об отказе пользоваться цифровыми рублями заполнять и относить в МФЦ или какие-то другие инстанции не нужно. МФЦ не имеет никакого отношения к открытию счетов цифрового рубля.",
                'user_id' => $reporter->id,
                'category_id' => Category::inRandomOrder()->first()->id,
                'image' => 'news_1.png'
            ],
            [
                'title' => 'Ученый ТГУ: полностью избавиться от микропластика не получится',
                'content' => "Человечество вряд ли когда-нибудь полностью избавится от пластика, но минимизировать попадание его частиц в природу можно – с помощью улучшения систем очистки коммунальных стоков или с применением индивидуальных технологий очистки, считает директор Центра исследования микропластика Томского госуниверситета (ТГУ) Юлия Франк.\n\nРанее сообщалось, что ТГУ несколько лет занимается изучением микропластика и его переноса в окружающей среде, прежде всего в водоемах. Юлия Франк первой в стране защитила докторскую диссертацию по микропластику. В ее работе собраны количественные данные о загрязнении им поверхностных вод Оби, Енисея и их притоков, отложении на дне рек и взаимодействии с живыми организмами.\n\nПо ее словам, одно из решений – сокращение использования пластиковых отходов за счет новых подходов к обращению с ними: это их повторное использование, переработка, сбор и хранение. Что касается производных от пластика, то есть попадающих в окружающую среду микро- и наночастиц, то здесь ученый видит решение только в улучшении систем коммунальных стоков, которые блокировали бы выход микропластика в поверхностные воды.\n\n\"Интересно, и я всегда привожу этот пример на лекциях. Несколько европейских компаний предложили решение: индивидуальные фильтры для стиральных машин, они устанавливаются на конкретное устройство и собирают микроволокна. Это хороший пример технологий, которые можно внедрить повсеместно\", – сказала Франк.\n\n![Ученые ТГУ обнаружили микропластик в снеге во всей Западной Сибири](:api:$image2_1)\n\n# Проблема, которая еще только изучается\n\nПо словам Франк, несмотря на то, что сегодня в мире очень много исследований, посвященных микропластику, и очевиден факт его переизбытка в окружающей среде, сказать однозначно, что он вреден для живых существ во всех отношениях нельзя. Например, на выживаемость комаров микропластик не влияет, а вот на скорость метаморфоза влияет.",
                'user_id' => $reporter->id,
                'category_id' => Category::inRandomOrder()->first()->id,
                'image' => 'news_2.jpg'
            ],
            [
                'title' => '"Магия больших зарплат" у курьеров больше не действует на томичей',
                'content' => "Высокая зарплата перестала быть главным мотивирующим фактором для соискателей на должность курьеров – чтобы ее получать, нужно много работать, а к этому не все готовы; при этом спрос на курьеров в регионе в 2024 году вырос на 32%, сообщила РИА Томск представитель пресс-службы HeadHunter.\n\nРанее сообщалось, что, по данным аналитиков HeadHunter, в январе 2025 года работодатели Томской области предложили населению свыше 1 тысячи вакансий с заработной платой 200 тысяч рублей и выше. При этом чаще всего высокую зарплату предлагают курьерам – это 39% от всех высокооплачиваемых вакансий.\n\nОна уточнила, что спрос на курьеров в Томской области по итогам 2024 года вырос на 32% относительно 2023 года. На начало февраля в регионе открыто 312 вакансий для курьеров. Чаще всего их нанимают компании из отраслей \"Перевозка, логистика, ВЭД\" и \"Розничная торговля\". За этот же период было открыто или обновлено 811 резюме местных соискателей, которые рассматривали работу курьерами.\n\n\"На одну курьерскую вакансию в области приходится в среднем 2,6 резюме. Норма для рынка труда – от четырех резюме на вакансию. Такие показатели говорят о том, что  работодатели продолжают испытывать сложности с закрытием вакансий курьеров, кандидатов недостаточно\", – сказала представитель HeadHunter.\n\nОна подчеркнула, что требования к курьерам сегодня минимальны, 99% вакансий доступны кандидатам без опыта работы. Томским курьерам предлагают гибкий график, отсутствие штрафов за ошибки, справедливую очередь заказов, моментальное зачисление чаевых на карту, еженедельные выплаты и доставку заказов в удобном районе. Но даже это все не очень привлекает томскую молодежь.\n\n\"У молодежи курьерская работа вообще не в приоритете сейчас. Данные hh.ru говорят о том, что кандидаты в возрасте до 20 лет ищут преимущественно работу в ресторанно-гостиничной сфере, в качестве менеджеров по продажам, упаковщиками или комплектовщиками, а также в топе их интересов работа программистами и разработчиками\", – добравила собеседница агентства.",
                'user_id' => $reporter->id,
                'category_id' => Category::inRandomOrder()->first()->id,
                'image' => 'news_3.jpg'
            ],
            [
                'title' => 'Больше всего томичей ищут работу менеджеров, продавцов и водителей',
                'content' => "Томичи открыли и обновили более 200 тысяч резюме в 2024 году; в топ-5 по соискательскому спросу вошли такие профессии, как кассир, продавец-консультант и водитель, сообщила РИА Томск сотрудница пресс-службы РИА Томск руководитель пресс-службы HeadHunter Сибирь Лилия Эсауленко.\n\nРанее сообщалось, что с 2022 года в регионе на фоне демографической ситуации в РФ и проведения СВО фиксируется дефицит кадров, в особенности на вакансии линейного персонала. В 2024 года острую нехватку рук испытывала сфера розничной торговли и пассажироперевозок.\n\nОна добавила, что замыкают пятерку по соискательскому спросу профессии продавец-консультант и продавец-кассир (в сумме 5,3 тысячи резюме) и сфера IT – программисты и разработчики (5,2 тысячи). В топ-20 также вошли: официант, бармен, бариста (4 тысячи резюме в сумме), курьер (3,8 тысячи), а также дизайнер и художник, которыми хотели стать 2,6 тысячи томичей.\n\n![Спрос на клининговые услуги вырос в Томске вдвое в январе](:api:$image4_1)\n\nПо ее словам, меньше всего томских соискателей интересовала работа андеррайтерами, то есть специалистами в сфере страхования и финансов, и бортпроводниками: за весь год было открыто менее ста таких резюме.\n\nРанее сообщалось, что в 2024 году томичи стали активнее искать работу. В регионе было открыто и обновлено на 18% больше резюме. При этом уровень безработицы осенью того же года не превышал 1%. Рост активности соискателей эксперты объясняли их готовностью сменить профессию на ту, где лучше условия и больше платят.",
                'user_id' => $reporter->id,
                'category_id' => Category::inRandomOrder()->first()->id,
                'image' => 'news_4.jpeg'
            ],
            [
                'title' => 'От купцов к инвесторам: 21 "дом за рубль" в Томске выставят на торги',
                'content' => "Мэрия планирует найти инвесторов для восстановления 21 \"дома за рубль\" в Томске в 2025 году. Первые четыре строения хотят выставить на торги уже 19 февраля. Какие здания предлагают восстановить инвесторам - далее.\n\n![](:api:$image5_1)\n\nДвухэтажный деревянный дом на улице Шишкова, 27 – выявленный объект культурного наследия. Выставить на торги его планируют уже 19 февраля. Начальная стоимость – 78 677 рублей.![](:api:$image5_2)\n\nПереулок Аптекарский, 11/1. Двухэтажный деревянный дом с резным декором. Построен в 1880 году. Входит в предмет охраны приказа Минкульта об утверждении границ исторического Томска. Фасад здания выходит на бывшую Бочановскую (нынче Алтайскую) улицу, где в XIX веке располагались дома терпимости.\n\n![](:api:$image5_3)\n\nДеревянный двухэтажный дом на улице Загорной, 59, как и предыдущий, находится в границах исторического Томска. Построен в 1903 году.\n\n![](:api:$image5_4)\n\nДеревянная двухэтажка на Советской, 10б также входит в предмет охраны. Здание построили в конце XIX века как богадельню при женском монастыре. В начале ХХ века там разместился военный лазарет, в доме также была освященная домовая церковь, купол которой до сих пор сохранился.\n\n![](:api:$image5_5)\n\nДом на Советской, 8в построен в 1892 году. Находится в границах исторического Томска.",
                'user_id' => $reporter->id,
                'category_id' => Category::inRandomOrder()->first()->id,
                'image' => 'news_5.jpg'
            ],
            [
                'title' => 'Более 4,7 тыс томичей заявились на "Лыжню России" через Госуслуги',
                'content' => "Свыше 4,7 тысячи жителей Томской области зарегистрировались на участие в \"Лыжне России\" через портал госуслуг, но реальное количество участников забегов во всех 20 муниципалитетах может оказаться гораздо больше, сообщила РИА Томск представительница пресс-службы облдепартамента по спорту Юлия Бирюк.\n\nВсероссийская акция \"Лыжня России\" проводится в Томской области с 2007 года. Ранее сообщалось, что в 2024 году заявки на участие подавали более 2 тысяч томичей. Массовая спортивная акция в 2025-м запланирована на 8 февраля. Мероприятие приурочено к 80-летию победы в Великой Отечественной войне. Соревнования начнутся в регионе одновременно – в 11.00.\n\nПо данным облдепартамента, в 2025 году старты \"Лыжни России\" пройдут во всех 20 муниципальных образованиях региона. В Томске традиционно будет организовано четыре площадки, в Томском районе – девять.\n\nТакже сообщалось, что в Томске старты пройдут одновременно на стадионе \"Буревестник\" (улица 19-й Гвардейской Дивизии, 20) – с учетом времени, а также на лыжных базах \"Метелица\" (Королева, 13), \"Сосновый бор\" (Кутузова, 1Б) и \"Кедр\" (Высоцкого, 7, строение 2) – без учета времени.",
                'user_id' => $reporter->id,
                'category_id' => Category::inRandomOrder()->first()->id,
                'image' => 'news_6.jpg'
            ],
            [
                'title' => 'ТГУ закупил оборудование на 10 млн руб для изучения стресса растений',
                'content' => "Томский госуниверситет (ТГУ) приобрел спектрофотометры, планшетный ридер, флюоресцентный анализатор фотосинтеза и газоанализатор для изучения устойчивости растений к стрессу; оборудование обошлось вузу в 10 миллионов рублей, сообщила в пятницу журналистам доцент ПИШ \"Агробиотек\" ТГУ Лилия Коломейчук.\n\nПо ее словам, среди нового оборудования: спектрофотометры, планшетный ридер, флюоресцентный анализатор фотосинтеза и газоанализатор. Оборудование обошлось университету в 10 миллионов рублей.\n\n\"Растения подвергаются стрессу в течение всей своей жизни. Так как в интересах человечества сохранять и приумножать продуктивность ценных для нас сортов, важно создавать новые технологии, которые позволят нам повышать устойчивость и продуктивность растений\", – пояснила доцент.\n\nПо данным пресс-службы, оборудование было закуплено для передовой инженерной школы \"Агробиотек\". Оно позволит эффективнее исследовать физиологию растений и готовить биологов, биотехнологов, агрономов, используя новейшие инструменты. В планах – разработать биопрепараты, которые повысят устойчивость сельскохозяйственных растений к стрессам, недостатку света, вредителям.\n\n![ТГУ начнет готовить технологов для сельхозпредприятий с 2025г](:api:$image7_1)\n\n\"Научная группа ПИШ ТГУ и БИ (Биологического института) ТГУ уже готовит заявку на грант Российского научного фонда. В исследовании будут рассматриваться механизмы регуляции устойчивости сельхозкультур к стрессам. Это поможет разработать технологии, которые повысят продуктивность. В планах группы — создание прототипа биопрепарата, который повысит устойчивость к засухе\", – сказано в сообщении.\n\nРанее сообщалось, что ПИШ \"Агробиотек\" ТГУ создана в 2022 году на грант Минобрнауки для подготовки биоинженеров, клеточных технологов, биоинформатиков, молекулярно-клеточных ветеринаров и других специалистов для сельского хозяйства. Кроме того, ПИШ призвана довести подготовку аграриев до современного уровня с учетом всех имеющихся рисков – глобальных и региональных.",
                'user_id' => $reporter->id,
                'category_id' => Category::inRandomOrder()->first()->id,
                'image' => 'news_7.jpg'
            ]
        ];

        // Сохраняем картинки в storage и базе
        foreach ($news as &$item) {
            $image = $this->UploadImage($item["image"])["image"];
            $item["image_id"] = $image->id;
            unset($item["image"]);
        }

        // Сохраняем категории в базе
        foreach ($news as &$item) {
            $createdNews = News::create($item);
            $comments = Comment::factory(10)->create([
                'news_id' => $createdNews->id
            ]);
        }
    }

    public function UploadImage(string $filename)
    {
        // Текущая дата
        $date = new \DateTime();

        // Извлекаем конкретные числа
        $day   = $date->format('d');
        $month = $date->format('m');
        $year  = $date->format('Y');

        $image = Image::saveLocalPhoto("database/seeders/images/" . $filename, "images/$year/$month/$day");
       return [
           "image" => $image,
           "path" => "storage/images/$year/$month/$day/" . $image->hash . "." . $image->extension
       ];
    }
}
