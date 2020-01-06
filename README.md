# Dokumentacja Project-PixBlocks-Addition

## 1. Stawianie bazy danych (wymagane jest posiadanie zainstalowanej bazy SQL)
- Przed uruchomieniem projektu w Visual Studio przechodzimy do zakładki Tools -> NugetPackageManager -> PackageManagerConsole.
- Następnie pojawi nam się konsola w którą wpisujuemy "Add-Migration PixBlockMigration -Context PixBlocksContext".
- (Wersja linux: "dotnet ef migrations add PixBlockMigration --context PixBlocksContext")
- Następnie wpisujemy "Add-Migration PixBlockMigration -Context RefreshTokenContext".
- (Wersja linux: "dotnet ef migrations add PixBlockMigration --context RefreshTokenContext")
- Następnie wpisujemy "Update-Database -Context PixBlocksContext" oraz klikamy enter.
- (Wersja linux: "dotnet ef database update --context PixBlocksContext")
- Następnie wpisujemy "Update-Database -Context RefreshTokenContext" oraz klikamy enter.
- (Wersja linux: "dotnet ef database update --context RefreshTokenContext")

## 2. Swagger
Konfiguracja swaggera znajduje się w pliku `Startup.cs`
- SwaggerUI znajduje się pod adresem: "http://localhost:port/swagger".
- SwaggerJson znajduje się pod adresem: "http://localhost:port/swagger/v1/swagger.json".
## 3. Autoryzacja i uwierzytelnianie
Autoryzacja została zaimplementowana przy użyciu tokenów JWT. Token do autoryzacji jest ważny 5 minut oraz posiada ClockSkew 5 minut. 
Posiadając token należy umieścić go w nagłówku w następujący sposób: 
<pre><b>Authorization</b>: Bearer <i>token</i></pre>
### 3.1 Rejestracja i logowanie
Klient rejestruje się przy pomocy metody POST endpointa  `api/identity/register`. Używając podanych wcześniej danych loguje się endpointem `api/identity/login`. Serwer zwraca:
- **AccessToken** – token do autoryzacji
- **RefreshToken** – token do odświeżenia tokenu
- **Expires** – datę wygaśnięcia tokenu w formacie Unix
- **RoleId** – Id roli użytkownika
- **Email** – email użytkownika
### 3.2 Refresh tokeny
Posiadając *RefreshToken* można uzyskać nowy token do autoryzacji. Wystarczy wykonać zapytanie POST dla `api/identity/refresh` podając jako parametr *RefreshToken*. *RefreshToken*`y są przechowywane w bazie danych oraz są skorelowane z użytkownikiem. Raz wygenerowany *RefreshToken* jest aktywny do czasu jego unieważnienia(revoke). 
### 3.2.1 Unieważnianie RefreshToken
Aby unieważnić *RefreshToken* trzeba być zalogowanym, a następnie należy odpytać endpoint `api/identity/revoke` używając metody POST oraz przekazując jako parametr *RefreshToken*.
*RefreshToken*, który został unieważniony nadal jest przechowywany w bazie danych, jednak zostaje mu ustawiona flaga Revoked, przez co nie można go już używać.
### 3.3 Wylogowanie, unieważnianie tokenów dostępu
Aby unieważnić token dostępu(AccessToken) należy odpytać endpoint `api/identity/cancel` metodą POST, jako zalogowany użytkownik(podając swój token dostępu w nagłówku autoryzacji).
Gdy użytkownik unieważni swój token dostępu, jego token jest dodawany do cache’u w pamięci na 5 minut. Każdy request przechodzi przez *CancellationTokenMiddleware*, który sprawdza, czy dany token dostępu nie został unieważniony.
## 4. Zarządzanie kursami
Kurs posiada następujące pola:
- **Id** – Guid, unikalny identyfikator kursu
- **Index** – wartość int, wykorzystywana przy zwracaniu kursów do ich posortowania
- **MediaId** – nie jest wykorzystywane
- **Category** - kategoria
- **Premium** – flaga wskazująca czy użytkownik posiada konto premium
- **Title** – unikalny tytuł(unikalny tylko w obrębie danego języka), zawiera od 3 do 250 znaków
- **Description** – opis kursu, zawiera od 3 do 10000 znaków
- **Picture** – adres url obrazka kursu. Do bazy danych zapisywane są tylko id zdjęć
- **Resources** – kolekcja adresów url do plików pdf lub zdjęć. Do bazy zapisywane są tylko id
- **Duration** – nie jest wykorzystywane
- **PublishDate** – data opublikowania kursu
- **Language** – język kursu
- **QuizId** - id do quizu, w przypadku braku przypisanego quizu ma wartość null
- **Tags** – kolekcja tagów
- **CourseVideos** – kolekcja wideo, które zostały dodane do kursu

Pole **Index** jest przechowywane w bazie danych, lecz nie jest zwracane wraz z kursem.
### 4.1 Tworzenie nowego kursu
Dane do utworzenia nowego kursu przesyłane są poprzez **x-www-form-urlencoded**. Aby utworzyć nowy kurs należy być zalogowanym jako Administrator i przesłać *MediaResource* metodą POST do endpointa `api/course/create`. Wymagane pola to **Premium**, **Title**, **Description**, **Language**. 

Nie mogą istnieć kursy o takim samym tytule, jeżeli posiadają ten sam język.
Jeżeli podamy jakieś zdjęcie, bądź pliki do tworzonego kursu, zostaną one zapisane do bazy danych.
### 4.2 Pobieranie kursów
Pobierając dane należy pamiętać, że wszystkie zwracane dane są kategoryzowane ze względu na język(wyjątkiem jest pobieranie kursu po id). Język dla jakiego pobrane zostaną dane jest ustalany na podstawie nagłówka Accept-Language. Domyślnym językiem jest język angielski. Pobieranie kursów następuje poprzez odpytanie metodą GET jednego z następujących endpointów:
- `api/course/all` z parametrami (int page, int count = 10) – zwraca wszystkie kursy
- `api/course/tags` z parametrem (string [] tags) – zwraca wszystkie kursy o danych tagach
- `api/course/title` z parametrem (string title) – zwraca kursy zawierające w tytule frazę title
- `api/course` z parametrem (Guid id) – zwraca kurs o danym id

## 5. Quizy
Quiz składa się z następujących składowych:
- **Id** - Guid, unikalny identyfikator quizu
- **MediaId** - Guid, identyfikator media(wideo lub kurs), którego dotyczy dany quiz
- **Question** - string, pytanie
- **Answers** - kolekcja zawierająca obiekty typu **QuizAnswer**, które opisują odpowiedzi

Każdy quiz jest skorelowany z dokładnie jednym media. Każde media może przechowywać maksymalnie 1 quiz.
Odpowiedzi do quizów mogą zawierać dowolną ilość poprawnych odpowiedzi.
Usunięcie quizu powoduje ustawienie na null pola **QuizId** powiązanego media.
Usunięcie media z quizem powoduje również usunięcie quizu.
Aktualizacja quizu powoduje usunięcie kolekcji **Answers** i stworzenie nowej.
