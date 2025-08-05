# 🎵 Concert Booking App

En modern konsertbokningsapplikation byggd med .NET 9 MAUI och ASP.NET Core Web API.

## 📋 Innehållsförteckning

- [Teknisk stack](#-teknisk-stack)
- [Förutsättningar](#-förutsättningar)
- [Installation & Setup](#-installation--setup)
- [Hur man kör applikationen](#-hur-man-kör-applikationen)
- [Debugging i VS Code](#-debugging-i-vs-code)
- [Android Emulator](#-android-emulator)
- [Projektstruktur](#-projektstruktur)
- [API Endpoints](#-api-endpoints)

## 🛠 Teknisk stack

- **Backend**: ASP.NET Core 9.0 Web API
- **Frontend**: .NET MAUI (Multi-platform App UI)
- **Databas**: SQL Server LocalDB
- **ORM**: Entity Framework Core 9.0
- **Mapping**: AutoMapper
- **API Dokumentation**: Swagger/OpenAPI

## 📋 Förutsättningar

Innan du börjar, se till att du har följande installerat:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (rekommenderas för Android-utveckling)
- [Visual Studio Code](https://code.visualstudio.com/) (för API-utveckling)
- SQL Server LocalDB (ingår i Visual Studio)
- **För Android-utveckling**: Android SDK och emulatorer

## ⚙️ Installation & Setup

### 1. Klona repository

```bash
git clone https://github.com/RonnieSamson/ConcertBookingApp.git
cd ConcertBookingApp
```

### 2. Återställ NuGet-paket

```bash
dotnet restore
```

### 3. Bygg hela lösningen

```bash
dotnet build
```

### 4. Uppdatera databasen

Kör Entity Framework migrationer för att skapa databasen:

```bash
# Navigera till Data-projektet
cd Concert.Data

# Uppdatera databasen med senaste migrationen
dotnet ef database update --startup-project ../Concert.API/Concert.API.csproj

# Gå tillbaka till root
cd ..
```

> **💡 Tips**: Om du får fel med EF-verktyg, installera dem globalt:
> ```bash
> dotnet tool install --global dotnet-ef
> ```

## 🚀 Hur man kör applikationen

### Option 1: Fullständig app (API + Windows App)

Den enklaste metoden är att använda VS Code med förkonfigurerade launch-inställningar:

1. Öppna projektet i VS Code
2. Tryck `F5` eller gå till **Run and Debug** (`Ctrl+Shift+D`)
3. Välj **"Launch Full App (API + Windows)"**
4. Detta startar både API:et och Windows-appen automatiskt

### Option 2: Kör komponenter separat

#### Starta API:et

```bash
cd Concert.API
dotnet run
```

API:et kommer att vara tillgängligt på:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`
- Swagger UI: `https://localhost:5001/swagger`

#### Starta MAUI Windows App

```bash
cd Concert.MAUI
dotnet run --framework net9.0-windows10.0.19041.0
```

## 🐛 Debugging i VS Code

Projektet är förkonfigurerat med flera debug-alternativ i `.vscode/launch.json`:

### Tillgängliga launch-konfigurationer:

- **🖥️ Launch MAUI App (Windows)**: Kör Windows-appen med debugging
- **⚡ Run MAUI App (Windows) - Fast Start**: Snabb start utan rebuild
- **🌐 Launch API Server**: Kör bara API:et med Swagger
- **📱 Launch MAUI App (Android)**: Kör Android-appen
- **🔄 Launch Full App (API + Windows)**: Kombinerad start (rekommenderas)
- **🔄 Launch Full App (API + Android)**: API + Android app

### Användning:

1. Öppna **Run and Debug**-panelen (`Ctrl+Shift+D`)
2. Välj önskad konfiguration från dropdown
3. Tryck `F5` eller klicka på ▶️-knappen

## 📱 Android Emulator

För Android-utveckling rekommenderar vi **Visual Studio 2022** som har bättre stöd för:

- Android SDK Manager
- AVD (Android Virtual Device) Manager
- Enklare emulator-hantering
- Bättre debugging-upplevelse

### Starta Android-appen:

**I Visual Studio:**
1. Öppna `ConcertBookingApp.sln`
2. Välj `Concert.MAUI` som startup project
3. Välj Android-emulator från dropdown
4. Tryck `F5`

**I VS Code:**
- Använd launch-konfigurationen **"Launch Full App (API + Android)"**

## 📂 Projektstruktur

```
ConcertBookingApp/
├── Concert.API/                 # Web API projekt
│   ├── Controllers/            # API controllers
│   ├── Profiles/              # AutoMapper profiler
│   └── Program.cs             # API startup
├── Concert.Data/               # Data access layer
│   ├── Entity/                # Entity models
│   ├── Repository/            # Repository pattern
│   ├── Migrations/            # EF migrationer
│   └── ApplicationDbContext.cs
├── Concert.Data.DTO/          # Data Transfer Objects
├── Concert.MAUI/              # MAUI cross-platform app
│   ├── Models/                # UI models
│   ├── Views/                 # XAML sidor
│   ├── ViewModels/            # MVVM ViewModels
│   └── Services/              # App services
└── .vscode/                   # VS Code konfiguration
    ├── launch.json            # Debug konfigurationer
    └── tasks.json             # Build tasks
```

## 🌐 API Endpoints

När API:et körs kan du utforska alla endpoints via Swagger UI:

**Swagger URL**: `https://localhost:5001/swagger`

### Huvudsakliga endpoints:

- **🎼 Concerts**: `/api/concerts` - Hantera konserter
- **🎭 Performances**: `/api/performances` - Hantera föreställningar
- **🎫 Bookings**: `/api/bookings` - Hantera bokningar
- **👤 Users**: `/api/users` - Användarhantering

## 🔧 Felsökning

### Vanliga problem:

**"Concert.API.dll does not exist"**
```bash
dotnet build Concert.API/Concert.API.csproj
```

**Entity Framework-fel**
```bash
dotnet tool install --global dotnet-ef
dotnet ef database update --startup-project Concert.API/Concert.API.csproj
```

**MAUI build-fel**
- Se till att du har installerat MAUI workload:
```bash
dotnet workload install maui
```


Om du stöter på problem:

1. Kontrollera att alla förutsättningar är installerade
2. Kör `dotnet build` för att bygga projektet
3. Verifiera att databasen är uppdaterad med `dotnet ef database update`
4. Kontrollera API:et fungerar genom att besöka Swagger UI

---


