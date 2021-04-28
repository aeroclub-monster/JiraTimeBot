# JiraTimeBot [![Build status](https://ci.appveyor.com/api/projects/status/anmhfvuj4oq84j1f/branch/master?svg=true)](https://ci.appveyor.com/project/monster1025/jiratimebot/branch/master)
Приложение для автоматического добавления Jira Worklog'ов исходя из количества коммитов в ветку. Возможна работа по JQL-запросу JIRA.

# Ограничения
 - Предполагается что все Ваши репозитории находятся в одной папке (к которой вы указываете путь в настройках).
 - Предполагается что ветки в JIRA имеют название эквивалентное имени бранча в Mercurial.

# Настройки
 - Режим работы:
   - Mercurial - по коммитам в Mercurial, можем определять множественные коммиты в одну задачу (ветку) и пропорционально списывать время.
   - Git - по коммитам в Git.
   - Jira - выполняем JQL и равномерно распределяем время.
 - Путь до репо (Режим Mercurial, Git) - путь до папки, в которую клонируются исходники.
 - Jira URL - адрес Jira-сервреа.
 - Логин Jira - Логин в Jira (от этого пользователя будем выполнять запросы и вносить время).
 - Пароль Jira - шифруется через DPAPI (может быть JSessionID - если длинна = 32 (!)).
 - Mercurial Name (Режим Mercurial) - обязателен к указанию для режима Mercurial - имя пользователя Mercurial, от которого проходят коммиты (именно UserName, не E-Mail).
 - Округление времени - Для красоты распределения времени тут устанавливается округление. По умолчанию - 15 минут, получим время 1:15, 0:30 итд. Остаток нераспределенного времени будет внесен в самую долгую задачу или же если это невозможно - количество рабочего времени будет увеличено или уменьшено.
 - JQL (Режим Jira) - можно задать свой JQL для поиска задач. Возможные замены: %USER% - Логин Jira, %DATE% дата на которую формируем список. Значнеие по умолчанию: status changed by '%USER%' during ("%DATE%","%DATE%")
 - Задача "учёта времени" - задача, куда будет списываться по 30 минут в день на "затраты на учет времени". Должна содержать знак '-'.
 - Время запуска - время автоматического запуска бота. Если была установлена галочка "тестовый прогон" - она будет снята.
 - Минут в рабочем дне - количество распределяемых минут (оыбчно 8 * 60).
 - Рандомных минут - если > 0, то будет прибавлено рандомное количество минут к рабочему времени (но кратное значению округления).
 - Добавлять описание (Режим Mercurial) - добавлять ли в описание CommitMessage, если не установлено - описание не заполняются.
 - Автозагрузка - загружать ли программу при старте ОС.
 - Делать pull (Режим Mercurial) - делать ли hg pull перед началом обработки (полезно, если изменения делаются Вами не только на текущем ПК).
 
# Скачать
Скачать актуальную версию можно тут: https://github.com/monster1025/JiraTimeBot/releases/latest
