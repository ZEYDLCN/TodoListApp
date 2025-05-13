# TodoListApp - ASP.NET Core Web API

A simple yet robust Todo List application built with ASP.NET Core, demonstrating N-Tier architecture and best practices. Users can create, manage, and track their tasks through a RESTful API.

---

Basit ama sağlam bir Yapılacaklar Listesi uygulamasıdır. ASP.NET Core ile N-Katmanlı mimari ve en iyi pratikler kullanılarak geliştirilmiştir. Kullanıcılar, RESTful API aracılığıyla görevlerini oluşturabilir, yönetebilir ve takip edebilir.

## Table of Contents | İçindekiler

*   [English](#english)
    *   [Features](#features)
    *   [Tech Stack & Key Concepts](#tech-stack--key-concepts)
    *   [Project Structure](#project-structure)
    *   [Getting Started](#getting-started)
        *   [Prerequisites](#prerequisites)
        *   [Installation & Setup](#installation--setup)
        *   [Running Migrations](#running-migrations)
        *   [Running the API](#running-the-api)
    *   [API Endpoints](#api-endpoints)
    *   [Contributing](#contributing)
    *   [License](#license)
*   [Türkçe](#türkçe)
    *   [Özellikler](#özellikler)
    *   [Teknolojiler ve Temel Kavramlar](#teknolojiler-ve-temel-kavramlar)
    *   [Proje Yapısı](#proje-yapısı)
    *   [Başlarken](#başlarken-1)
        *   [Ön Gereksinimler](#ön-gereksinimler)
        *   [Kurulum](#kurulum)
        *   [Migration'ları Çalıştırma](#migrationları-çalıştırma)
        *   [API'yi Çalıştırma](#apiyi-çalıştırma)
    *   [API Uç Noktaları](#api-uç-noktaları)
    *   [Katkıda Bulunma](#katkıda-bulunma)
    *   [Lisans](#lisans-1)

---

## English

### Features

*   Create, Read, Update, and Delete (CRUD) operations for Todo items.
*   Mark tasks as completed or uncompleted.
*   N-Tier architectural pattern (Entities, Data Access, Business Logic, API).
*   RESTful API endpoints.
*   Swagger/OpenAPI documentation for easy API testing and exploration.
*   Entity Framework Core for data persistence.
*   Dependency Injection for loosely coupled components.
*   Asynchronous programming (`async`/`await`) for improved performance and scalability.

### Tech Stack & Key Concepts

*   **.NET 8** (Can be adapted for .NET 6/7)
*   **ASP.NET Core Web API**: For building RESTful services.
*   **Entity Framework Core**: ORM for database interaction.
    *   **SQLite**: As the database provider (easily swappable).
    *   **Code-First Migrations**: For database schema management.
*   **N-Tier Architecture**:
    *   `TodoListApp.Entities`: Core domain models.
    *   `TodoListApp.DataAccess`: Data access logic (Repositories, DbContext).
    *   `TodoListApp.Business`: Business rules and service layer.
    *   `TodoListApp.API`: Presentation layer (Controllers).
*   **Repository Pattern**: To abstract data access logic.
*   **Service Layer Pattern**: To encapsulate business logic.
*   **Dependency Injection (DI)**: Built-in ASP.NET Core DI container.
*   **Swagger (Swashbuckle.AspNetCore)**: For API documentation and testing UI.
*   **Data Transfer Objects (DTOs)**: For decoupling API contracts from domain models.
*   **Async/Await**: For non-blocking I/O operations.

### Project Structure
Use code with caution.
Markdown
TodoListApp.sln
├── TodoListApp.API # ASP.NET Core Web API (Startup Project)
│ ├── Controllers
│ │ └── TodoController.cs
│ ├── appsettings.json
│ └── Program.cs
├── TodoListApp.Business # Business Logic Layer
│ ├── Interfaces
│ │ └── ITodoService.cs
│ └── Services
│ └── TodoService.cs
├── TodoListApp.DataAccess # Data Access Layer
│ ├── Context
│ │ └── AppDbContext.cs
│ ├── Interfaces
│ │ └── ITodoRepository.cs
│ ├── Migrations # EF Core Migrations
│ └── Repositories
│ └── TodoRepository.cs
├── TodoListApp.Entities # Core Entities/Models
│ └── TodoItem.cs
└── README.md
### Getting Started

#### Prerequisites

*   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (or .NET 6/7 SDK if adapted)
*   [Git](https://git-scm.com/downloads)
*   An IDE like [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) (with C# Dev Kit extension).
*   EF Core CLI tools:
    ```bash
    dotnet tool install --global dotnet-ef
    # To update:
    # dotnet tool update --global dotnet-ef
    ```

#### Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/YOUR_USERNAME/TodoListApp.git
    cd TodoListApp
    ```

2.  **Restore NuGet packages:**
    Open the solution (`TodoListApp.sln`) in Visual Studio and it should restore packages automatically.
    Alternatively, from the solution's root directory in the terminal:
    ```bash
    dotnet restore
    ```

3.  **Configure Connection String (if necessary):**
    The default database is SQLite, and the database file (`todoapp.db`) will be created in the solution's root directory.
    The connection string is located in `TodoListApp.API/appsettings.json`:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Data Source=../todoapp.db"
    }
    ```
    You can change this to use a different SQLite file path or switch to another database provider (e.g., SQL Server) by updating `Program.cs` and `appsettings.json`.

#### Running Migrations

To create/update the database schema:

*   **Using Package Manager Console (Visual Studio):**
    1.  Open **Tools > NuGet Package Manager > Package Manager Console**.
    2.  Set **Default project** to `TodoListApp.DataAccess`.
    3.  Ensure `TodoListApp.API` is the **Startup Project** (Right-click on `TodoListApp.API` in Solution Explorer > Set as Startup Project).
    4.  Run:
        ```powershell
        Add-Migration YourMigrationName -OutputDir DataAccess/Migrations 
        Update-Database
        ```
        *(Note: If you encountered issues with `-OutputDir DataAccess/Migrations`, you might have used just `-OutputDir Migrations` or no `-OutputDir` for the default `Migrations` folder directly under `TodoListApp.DataAccess`)*

*   **Using .NET CLI (from the `TodoListApp.API` directory):**
    ```bash
    # Ensure you are in the TodoListApp.API project directory
    cd TodoListApp.API 
    dotnet ef migrations add YourMigrationName --project ../TodoListApp.DataAccess -o DataAccess/Migrations 
    dotnet ef database update --project ../TodoListApp.DataAccess
    cd .. 
    # Go back to solution root
    ```
    *(Adjust `-o DataAccess/Migrations` if your migrations folder structure is different, e.g., `-o Migrations`)*

#### Running the API

1.  Set `TodoListApp.API` as the startup project in Visual Studio.
2.  Press `F5` or click the "Run" button (e.g., IIS Express or `TodoListApp.API` Kestrel profile).
    Alternatively, from the `TodoListApp.API` directory in the terminal:
    ```bash
    dotnet run
    ```
3.  The API will typically start on `https://localhost:PORT` or `http://localhost:PORT`.
4.  Swagger UI will be available at `/swagger` (e.g., `https://localhost:PORT/swagger`).

### API Endpoints

Base URL: `/api/Todo`

*   **GET `/api/Todo`**: Retrieves all todo items.
*   **GET `/api/Todo/{id}`**: Retrieves a specific todo item by its ID.
*   **POST `/api/Todo`**: Creates a new todo item.
    *   Request Body (example):
        ```json
        {
          "title": "My New Task",
          "isCompleted": false
        }
        ```
*   **PUT `/api/Todo/{id}`**: Updates an existing todo item.
    *   Request Body (example):
        ```json
        {
          "title": "My Updated Task Title",
          "isCompleted": true
        }
        ```
*   **DELETE `/api/Todo/{id}`**: Deletes a todo item by its ID.

### Contributing

Contributions are welcome! If you have suggestions or want to improve the application, please feel free to:
1.  Fork the Project
2.  Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3.  Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4.  Push to the Branch (`git push origin feature/AmazingFeature`)
5.  Open a Pull Request

### License

Distributed under the MIT License. See `LICENSE` file for more information (if you add one).

---

## Türkçe

### Özellikler

*   Yapılacaklar (Todo) öğeleri için Oluşturma, Okuma, Güncelleme ve Silme (CRUD) işlemleri.
*   Görevleri tamamlandı veya tamamlanmadı olarak işaretleme.
*   N-Katmanlı mimari deseni (Varlıklar, Veri Erişimi, İş Mantığı, API).
*   RESTful API uç noktaları.
*   Kolay API testi ve keşfi için Swagger/OpenAPI dokümantasyonu.
*   Veri kalıcılığı için Entity Framework Core.
*   Gevşek bağlı bileşenler için Bağımlılık Enjeksiyonu (Dependency Injection).
*   Gelişmiş performans ve ölçeklenebilirlik için asenkron programlama (`async`/`await`).

### Teknolojiler ve Temel Kavramlar

*   **.NET 8** (.NET 6/7 için uyarlanabilir)
*   **ASP.NET Core Web API**: RESTful servisler oluşturmak için.
*   **Entity Framework Core**: Veritabanı etkileşimi için ORM.
    *   **SQLite**: Veritabanı sağlayıcısı olarak (kolayca değiştirilebilir).
    *   **Code-First Migrations**: Veritabanı şema yönetimi için.
*   **N-Katmanlı Mimari**:
    *   `TodoListApp.Entities`: Temel alan modelleri.
    *   `TodoListApp.DataAccess`: Veri erişim mantığı (Repository'ler, DbContext).
    *   `TodoListApp.Business`: İş kuralları ve servis katmanı.
    *   `TodoListApp.API`: Sunum katmanı (Controller'lar).
*   **Repository Deseni**: Veri erişim mantığını soyutlamak için.
*   **Servis Katmanı Deseni**: İş mantığını kapsüllemek için.
*   **Bağımlılık Enjeksiyonu (DI)**: Dahili ASP.NET Core DI container'ı.
*   **Swagger (Swashbuckle.AspNetCore)**: API dokümantasyonu ve test arayüzü için.
*   **Veri Transfer Nesneleri (DTO'lar)**: API sözleşmelerini alan modellerinden ayırmak için.
*   **Async/Await**: Engellemeyen G/Ç işlemleri için.

### Proje Yapısı
Use code with caution.
TodoListApp.sln
├── TodoListApp.API # ASP.NET Core Web API (Başlangıç Projesi)
│ ├── Controllers
│ │ └── TodoController.cs
│ ├── appsettings.json
│ └── Program.cs
├── TodoListApp.Business # İş Mantığı Katmanı
│ ├── Interfaces
│ │ └── ITodoService.cs
│ └── Services
│ └── TodoService.cs
├── TodoListApp.DataAccess # Veri Erişim Katmanı
│ ├── Context
│ │ └── AppDbContext.cs
│ ├── Interfaces
│ │ └── ITodoRepository.cs
│ ├── Migrations # EF Core Migration'ları
│ └── Repositories
│ └── TodoRepository.cs
├── TodoListApp.Entities # Temel Varlıklar/Modeller
│ └── TodoItem.cs
└── README.md
### Başlarken

#### Ön Gereksinimler

*   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (.NET 6/7 SDK'ya uyarlandıysa)
*   [Git](https://git-scm.com/downloads)
*   [Visual Studio 2022](https://visualstudio.microsoft.com/) veya [VS Code](https://code.visualstudio.com/) (C# Dev Kit eklentisi ile) gibi bir IDE.
*   EF Core CLI araçları:
    ```bash
    dotnet tool install --global dotnet-ef
    # Güncellemek için:
    # dotnet tool update --global dotnet-ef
    ```

#### Kurulum

1.  **Depoyu klonlayın:**
    ```bash
    git clone https://github.com/SENIN_KULLANICI_ADIN/TodoListApp.git
    cd TodoListApp
    ```

2.  **NuGet paketlerini geri yükleyin:**
    Çözümü (`TodoListApp.sln`) Visual Studio'da açtığınızda paketler otomatik olarak geri yüklenmelidir.
    Alternatif olarak, çözümün kök dizininde terminalden:
    ```bash
    dotnet restore
    ```

3.  **Bağlantı Dizesini Yapılandırın (gerekirse):**
    Varsayılan veritabanı SQLite'tır ve veritabanı dosyası (`todoapp.db`) çözümün kök dizininde oluşturulacaktır.
    Bağlantı dizesi `TodoListApp.API/appsettings.json` dosyasında bulunur:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Data Source=../todoapp.db"
    }
    ```
    Farklı bir SQLite dosya yolu kullanmak veya başka bir veritabanı sağlayıcısına (örneğin, SQL Server) geçmek için `Program.cs` ve `appsettings.json` dosyalarını güncelleyebilirsiniz.

#### Migration'ları Çalıştırma

Veritabanı şemasını oluşturmak/güncellemek için:

*   **Package Manager Console Kullanarak (Visual Studio):**
    1.  **Tools > NuGet Package Manager > Package Manager Console**'u açın.
    2.  **Default project** olarak `TodoListApp.DataAccess`'i seçin.
    3.  `TodoListApp.API`'nin **Startup Project** olduğundan emin olun (Solution Explorer'da `TodoListApp.API`'ye sağ tıklayın > Set as Startup Project).
    4.  Çalıştırın:
        ```powershell
        Add-Migration SeninMigrationAdin -OutputDir DataAccess/Migrations
        Update-Database
        ```
        *(Not: `-OutputDir DataAccess/Migrations` ile sorun yaşadıysanız, doğrudan `TodoListApp.DataAccess` altında varsayılan `Migrations` klasörü için sadece `-OutputDir Migrations` veya hiç `-OutputDir` kullanmamış olabilirsiniz)*

*   **.NET CLI Kullanarak (`TodoListApp.API` dizininden):**
    ```bash
    # TodoListApp.API proje dizininde olduğunuzdan emin olun
    cd TodoListApp.API
    dotnet ef migrations add SeninMigrationAdin --project ../TodoListApp.DataAccess -o DataAccess/Migrations
    dotnet ef database update --project ../TodoListApp.DataAccess
    cd .. 
    # Çözüm kök dizinine geri dön
    ```
    *(Migration klasör yapınız farklıysa `-o DataAccess/Migrations` kısmını ayarlayın, örneğin `-o Migrations`)*

#### API'yi Çalıştırma

1.  Visual Studio'da `TodoListApp.API`'yi başlangıç projesi olarak ayarlayın.
2.  `F5`'e basın veya "Çalıştır" düğmesine tıklayın (örneğin, IIS Express veya `TodoListApp.API` Kestrel profili).
    Alternatif olarak, `TodoListApp.API` dizininde terminalden:
    ```bash
    dotnet run
    ```
3.  API genellikle `https://localhost:PORT` veya `http://localhost:PORT` adresinde başlayacaktır.
4.  Swagger UI, `/swagger` adresinde (örneğin, `https://localhost:PORT/swagger`) kullanılabilir olacaktır.

### API Uç Noktaları

Temel URL: `/api/Todo`

*   **GET `/api/Todo`**: Tüm yapılacaklar listesini getirir.
*   **GET `/api/Todo/{id}`**: Belirli bir yapılacak öğesini ID'si ile getirir.
*   **POST `/api/Todo`**: Yeni bir yapılacak öğesi oluşturur.
    *   İstek Gövdesi (örnek):
        ```json
        {
          "title": "Yeni Görevim",
          "isCompleted": false
        }
        ```
*   **PUT `/api/Todo/{id}`**: Mevcut bir yapılacak öğesini günceller.
    *   İstek Gövdesi (örnek):
        ```json
        {
          "title": "Güncellenmiş Görev Başlığım",
          "isCompleted": true
        }
        ```
*   **DELETE `/api/Todo/{id}`**: Bir yapılacak öğesini ID'si ile siler.

### Katkıda Bulunma

Katkılarınızı bekliyoruz! Önerileriniz varsa veya uygulamayı geliştirmek isterseniz, lütfen çekinmeyin:
1.  Projeyi Fork'layın
2.  Özellik Dalınızı Oluşturun (`git checkout -b feature/HarikaOzellik`)
3.  Değişikliklerinizi Commit'leyin (`git commit -m 'HarikaOzellik Eklendi'`)
4.  Dala Push'layın (`git push origin feature/HarikaOzellik`)
5.  Bir Pull Request Açın
