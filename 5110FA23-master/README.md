# Super Heroes

A C# web application built with ASP.NET Core that offers superhero-themed t-shirts. This project was converted from ContosoCrafts to SuperHeroes, maintaining a clean architecture with well-organized components.

## 📁 Project Structure

```
SuperHeroes.WebSite/
├── Components/               # Blazor/Razor components
├── Controllers/             # API Controllers
├── Enums/                   # Enumeration definitions
├── Models/                  # Data models (including ProductModel, Material)
├── Pages/                   # Razor Pages
├── Properties/
│   └── ServiceDependencies/ # Azure deployment configurations
├── Services/                # Service classes (including JsonFileProductService)
├── wwwroot/                 # Static files and products.json
├── Program.cs              # Application entry point
├── Startup.cs              # Application configuration
├── NuGet.config            # NuGet package sources configuration
├── appsettings.json        # Application settings
├── appsettings.Development.json # Development settings
└── SuperHeroes.WebSite.csproj   # Project file
```

## 🚀 Features

- C# based web application using .NET Core
- Razor Pages for server-side rendering
- Blazor Components for interactive UI
- JSON-based product storage
- Material model implementation for product variants
- Custom update functionality with null checks
- Responsive button design with proper margins

## 🛠 Technical Stack

- **Language**: C# (.NET Core)
- **Framework**: ASP.NET Core
- **UI Technologies**:
  - Razor Pages
  - Blazor Components
- **Data Storage**: JSON file-based (`products.json`)
- **Testing**: Unit Tests (ProductListTests.cs)

## ⚙️ Key Components

### Models
- Product Model with Material support
- Custom data models for the shop

### Services
- `JsonFileProductService`: Handles product data management
  - Includes null checks for UpdateData
  - JSON file operations for product storage

### Controllers
- RESTful API endpoints
- Product management functionality

### Pages
- Responsive design
- Update functionality with proper UI margins

### Components
- Reusable Blazor components
- Unit tested (ProductListTests.cs)

## 🚦 Getting Started

### Prerequisites
- .NET 6.0 SDK or later
- Visual Studio 2022 (recommended) or VS Code

### Installation

1. Clone the repository:
```bash
git clone [your-repository-url]
```

2. Navigate to the project directory:
```bash
cd SuperHeroes.WebSite
```

3. Restore NuGet packages:
```bash
dotnet restore
```

4. Run the application:
```bash
dotnet run
```

The application will be available at `https://localhost:5001`

## 💻 Development

### Configuration Files
- `appsettings.json`: Production configuration
- `appsettings.Development.json`: Development-specific settings
- `NuGet.config`: Package source configuration

### Development Environment
When running in development mode:
- Use `appsettings.Development.json` configuration
- Detailed error pages
- Runtime compilation enabled

### Testing
Unit tests are available in the Components directory:
- ProductListTests.cs for component testing

## 🚀 Deployment
The project includes Azure deployment configurations under:
```
Properties/ServiceDependencies/5110SuperHeroes - Web Deploy
```

## 🔒 Security Features
- HTTPS enforcement
- Development/Production environment-specific error handling
- Proper null checking in data operations

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Coding Standards
- Follow existing project structure
- Include unit tests for new components
- Update products.json for any product-related changes
- Maintain null checks in service methods
- Document any new Models or Enums

## 🔗 Additional Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core)
- [Blazor Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/)
- [Azure Deployment Guide](https://docs.microsoft.com/en-us/azure/app-service/deploy-github-actions)
