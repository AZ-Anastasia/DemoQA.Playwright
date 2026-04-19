## Создание проекта
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

Как настраивать GitHub Actions: \
https://learn.microsoft.com/ru-ru/dotnet/devops/github-actions-overview

https://learn.microsoft.com/ru-ru/dotnet/devops/dotnet-test-github-action

Про настройку .yaml/.yml: \
https://github.com/actions/setup-dotnet

Про настройку Allure:\
https://github.com/simple-elf/allure-report-action

---
---
---

## Задачи для автотеста на demoqa.com

### TestID 001

**Описание** \
Автоматизировать проверку количества карточек элементов на странице «Elements».

Шаги для теста:
1. Открыть сайт `https://demoqa.com/`
2. Проверить, количество карточек элементов на странице равно `6`

### TestID 002

**Описание** \
Автоматизировать выбор и проверку выбранного Radio Button через форму на странице «Elements» → «Radio Button».

Шаги для теста:
1. Открыть сайт `https://demoqa.com/`
2. Перейти в раздел `Elements` → `Radio Button`
3. Проверить, открыт ли аккордион
   1. Если аккордион закрыт, нажать на него
4. Проверить, что текст заголовка совпадает с текстом выбранного элемента
5. Нажать на радио кнопку с текстом `Impressive`
6. Проверить, что эта кнопка выбрана
7. Проверить, что текст ниже содержит текст кнопки - `Impressive`

### TestID 003

**Описание** \
Автоматизировать добавление информации о новом пользователе через форму на странице «Elements» → «Text Box».

Шаги для теста:
1. Открыть сайт `https://demoqa.com/`
2. Перейти в раздел `Elements` → `Text Box`
3. Проверить, открыт ли аккордион
   1. Если аккордион закрыт, нажать на него
4. Проверить, что текст заголовка совпадает с текстом выбранного элемента
5. Заполнить поля формы:
- `Full Name` (например, Иван Иванов)
- `Email` (например, ivan.ivanov@example.com)
- `Current Address` (например, г. Москва, ул. Примерная, д. 1)
- `Permanent Address` (например, г. Москва, ул. Примерная, д. 1)
6. Нажать кнопку `Submit`
7. Проверить, что введённые данные корректно отображаются в блоке ниже формы