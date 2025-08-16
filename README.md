# TrackStar 🎬

> **⚠️ Work in Progress**: This project is currently unfinished and under active development.

A WPF (Windows Presentation Foundation) desktop application for tracking and managing your film collection using data from the OMDB (Open Movie Database) API.

## Overview

TrackStar is designed to help movie enthusiasts organize and track their film collections. The application fetches detailed movie information from the OMDB API and provides a user-friendly interface for browsing, searching, and managing films.

## Features

### Currently Implemented
- 🎯 **Movie Search**: Search for films using the OMDB API
- 📋 **Film Storage**: Save movie information locally
- 🖼️ **Movie Details**: Display comprehensive film information
- 💾 **Data Persistence**: Store film data for offline access

### Planned Features (To Do)
- ⭐ **Personal Ratings**: Rate and review your watched films
- 📊 **Statistics Dashboard**: View watching statistics and insights
- 🏷️ **Categories/Tags**: Organize films by genre, year, or custom tags
- 📱 **Export/Import**: Backup and restore your film collection
- 🔍 **Advanced Filtering**: Filter by genre, rating, year, etc.
- 👥 **Watchlist Management**: Track films you want to watch
- 📈 **Progress Tracking**: Mark films as watched/unwatched

## Technology Stack

- **Framework**: WPF (.NET Framework/Core)
- **Language**: C#
- **API**: OMDB (Open Movie Database) API
- **Data Storage**: [Specify database/storage method used]
- **Architecture**: MVVM (Model-View-ViewModel) pattern

## Prerequisites

- Windows 10 or later
- .NET Framework 4.7.2 or .NET Core 3.1+ (depending on project target)
- Internet connection for OMDB API access
- OMDB API key (free registration at [omdbapi.com](http://www.omdbapi.com/))

## Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yagizerdem/TrackStar.git
   cd TrackStar
   ```

2. **Open in Visual Studio**
   - Open the solution file (`.sln`) in Visual Studio 2019 or later
   - Restore NuGet packages if prompted

3. **Configure API Key**
   - Obtain a free API key from [OMDB API](http://www.omdbapi.com/apikey.aspx)
   - Add your API key to the application configuration:
     ```xml
     <!-- In app.config or appsettings.json -->
     <add key="OMDBApiKey" value="your-api-key-here" />
     ```

4. **Build and Run**
   - Build the solution (`Ctrl + Shift + B`)
   - Run the application (`F5`)

## Usage

1. **Search for Movies**
   - Enter a movie title in the search box
   - Browse through the search results
   - Click on a movie to view detailed information

2. **Add to Collection**
   - Click "Add to Collection" to save a movie to your local database
   - View your saved films in the "My Collection" section

3. **Manage Your Collection**
   - Browse your saved movies
   - View detailed information for each film
   - [Additional features as they're implemented]

## Project Structure

```
TrackStar/
├── Models/              # Data models and entities
├── Views/               # WPF user interface files (.xaml)
├── ViewModels/          # MVVM view models
├── Services/            # API services and data access
├── Utilities/           # Helper classes and utilities
├── Resources/           # Images, styles, and other resources
└── App.xaml             # Application entry point
```

## API Reference

This application uses the [OMDB API](http://www.omdbapi.com/) to fetch movie information.

**Example API endpoints used:**
- Search: `http://www.omdbapi.com/?s={title}&apikey={key}`
- Details: `http://www.omdbapi.com/?i={imdbID}&apikey={key}`

## Development Status

### Completed ✅
- [x] Basic project structure
- [x] OMDB API integration
- [x] Movie search functionality
- [x] Data models for film information
- [x] Basic WPF UI layout

### In Progress 🚧
- [ ] Complete UI/UX implementation
- [ ] Data persistence layer
- [ ] Error handling and validation
- [ ] Unit tests

### Planned 📋
- [ ] Advanced search filters
- [ ] User preferences and settings
- [ ] Export/import functionality
- [ ] Performance optimizations
- [ ] Documentation completion

## Contributing

This project is currently in development. Contributions are welcome once the core functionality is stabilized.

### How to Contribute
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/new-feature`)
3. Commit your changes (`git commit -am 'Add new feature'`)
4. Push to the branch (`git push origin feature/new-feature`)
5. Create a Pull Request

## Known Issues

- [List current known bugs or limitations]
- Application is not fully functional yet
- Some UI elements may be placeholder implementations
- Data persistence may not be fully implemented

## License

[Specify license type - e.g., MIT, GPL, etc.]

## Contact

**Developer**: yagizerdem  
**GitHub**: [https://github.com/yagizerdem](https://github.com/yagizerdem)

## Acknowledgments

- [OMDB API](http://www.omdbapi.com/) for providing movie data
- [Any other libraries or resources used]

---

**Note**: This project is under active development. Features and documentation may change as development progresses.
