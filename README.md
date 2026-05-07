## Отчеты

### https://az-anastasia.github.io/DemoQA.Playwright/

<details>
   <summary>
      <b>
         Создание проекта
      </b>
   </summary>

   Создать папку для решения
   ```
   mkdir Playwright
   cd Playwright
   ```

   Создание пустого проекта:
   ```
   dotnet new sln -n DemoQA.Playwright
   ```

   Создание проекта для тестов:
   ```
   dotnet new nunit -n DemoQA.Tests
   ```

   Создание проекта для страниц и элементов:
   ```
   dotnet new classlib -n DemoQA.PagesAndControls
   ```

   Добавление ссылки в проект:
   ```
   dotnet add DemoQA.Tests reference DemoQA.PagesAndControls
   ```

   Настройки для Playwright в PlaywrightSetup.cs

</details>

<details>
   <summary>
      <b>
         Как настраивать GitHub Actions:
      </b>
   </summary>

- [GitHub Actions and .NET](https://learn.microsoft.com/ru-ru/dotnet/devops/github-actions-overview)
- [Quickstart: Create a test validation GitHub workflow](https://learn.microsoft.com/ru-ru/dotnet/devops/dotnet-test-github-action)
</details>

<details>
   <summary>
      <b>
         Про настройку .yaml/.yml:
      </b>
   </summary>

- [GitHub: setup-dotnet](https://github.com/actions/setup-dotnet)

</details>

<details>
   <summary>
      <b>
         Про настройку Allure:
      </b>
   </summary>

- [GitHub: allure-report-action](https://github.com/simple-elf/allure-report-action)

</details>

---
---
---

## Задачи для автотеста на demoqa.com

### TestID 001

#### Описание
Проверка количества карточек элементов на странице «Elements»

#### Шаги для теста:
1. Открыть сайт `https://demoqa.com/`
2. Проверить, количество карточек элементов на странице равно `6`

---

### TestID 002

#### Описание
Подтверждение возможности выбора вариантов в Radio Button через форму на странице «Elements» → «Radio Button»

#### Шаги для теста:
1. Открыть сайт `https://demoqa.com/`
2. Перейти в раздел `Elements` → `Radio Button`
3. Проверить, развернута ли вкладка `Elements`
   4. Если вкладка закрыта, нажать на нее
5. Проверить, что текст заголовка совпадает с текстом выбранного элемента - `Radio Button`
6. Нажать на радио кнопку с текстом `Impressive`
7. Проверить, что эта кнопка выбрана
8. Проверить, что текст ниже содержит текст кнопки - `Impressive`

---

### TestID 003

#### Описание
Добавление информации о новом пользователе через форму на странице «Elements» → «Text Box»

#### Шаги для теста:
1. Открыть сайт `https://demoqa.com/`
2. Перейти в раздел `Elements` → `Text Box`
3. Проверить, развернута ли вкладка `Elements`
   4. Если вкладка закрыта, нажать на нее
5. Проверить, что текст заголовка совпадает с текстом выбранного элемента -  `Text Box`
6. Заполнить поля формы:
- `Full Name` (например, Иван Иванов)
- `Email` (например, ivan.ivanov@example.com)
- `Current Address` (например, г. Москва, ул. Примерная, д. 1)
- `Permanent Address` (например, г. Москва, ул. Примерная, д. 1)
7. Нажать кнопку `Submit`
8. Проверить, что введённые данные корректно отображаются в блоке ниже формы

---

### TestID 004

#### Описание
Открытие окна с подтверждением и выбором опции на странице «Alerts, Frame & Windows» → «Alerts»

#### Шаги для теста:
1. Открыть сайт `https://demoqa.com/`
2. Перейти в раздел `Alerts, Frame & Windows` → `Alerts`
3. Проверить, развернута ли вкладка `Alerts, Frame & Windows`
   4. Если вкладка закрыта, нажать на нее
5. Проверить, что текст заголовка совпадает с текстом выбранного элемента - `Alerts`
6. Нажать на кнопку напротив текста `On button click, confirm box will appear`
7. Нажать `OK` в появившемся окне
8. Проверить, что на странице рядом с текстом напротив кнопки появился еще текст с выбранным вариантом `Ok`

---

### TestID 005

#### Описание
Открытие новой вкладки на странице «Alerts, Frame & Windows» → «Browser Windows»

#### Шаги для теста:
1. Открыть сайт `https://demoqa.com/`
2. Перейти в раздел `Alerts, Frame & Windows` → `Browser Windows`
3. Проверить, развернута ли вкладка `Alerts, Frame & Windows`
   4. Если вкладка закрыта, нажать на нее
5. Проверить, что текст заголовка совпадает с текстом выбранного элемента - `Browser Windows`
6. Нажать на кнопку `New Tab`
7. Проверить, что URL открытой страницы содержит на конце `sample`

---

### TestID 006

#### Описание
Поиск, изменение и удаление данных на странице «Alerts, Frame & Windows» → «Web Tables»

#### Шаги для теста:
1. Открыть сайт `https://demoqa.com/`
2. Перейти в раздел `Alerts, Frame & Windows` → `Web Tables`
3. Проверить, развернута ли вкладка `Alerts, Frame & Windows`
   4. Если вкладка закрыта, нажать на нее
5. Проверить, что текст заголовка совпадает с текстом выбранного элемента - `Web Tables`
6. Убедиться, что в отобразившейся на странице таблице `7` заголовкой (названий столбцов) и `3` строки с данными
7. Ввести в поле для поиска по таблице имя `Alden`
8. Проверить, что после ввода имени в таблице отображается только строка с данными по введенному имени
9. Нажать кнопку `Edit` в строке с именем `Alden`
10. Проверить, что появилось модальное окно
11. В открывшемся модальном окне изменить фамилию на `Smith`
12. Нажать кнопку `Submit`
13. Проверить, что фамилия в таблице изменилась на `Smith`
14. Нажать кнопку `Delete` в строке с именем `Alden Smith`
15. Проверить, что в таблице не осталось строк по поиску с введенным именем
16. Очистить поле поиска
17. Проверить, что в таблице снова отображаются оставшиеся строки с данными
