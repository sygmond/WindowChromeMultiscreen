# Window Chrome Multiscreen

This is a sample WPF project named "WindowChromeMultiscreen" that demonstrates the usage of the WindowChrome style to customize the title bar in a WPF window.

## Description

The project showcases how to use the WindowChrome class in WPF to create a custom title bar that matches the style of the application. It provides a multi-screen functionality by utilizing native methods to retrieve information about the connected monitors. Additionally, it incorporates features such as dependency injection, customized window chrome with title bar buttons and styles, and an AppState that remembers the window size and position using the Jot library.

## Features

- Customized title bar using WindowChrome style
- Multi-screen support using native methods
- Dependency injection for enhanced modularity
- Window chrome with title bar buttons and styles
- AppState to remember window size and position using the Jot library

## Getting Started

To get started with the WindowChromeMultiscreen project, follow these steps:

1. Clone the repository:

   ```bash
   git clone https://github.com/sygmond/WindowChromeMultiscreen.git
2. Open the solution file, `WindowChromeMultiscreen.sln`, in Visual Studio.

3. Build the solution to restore NuGet packages and compile the project.

4. Run the application to see the sample window with a customized title bar and multi-screen support.

## Commit History

### Added Monitor Native Methods

- Implement native methods to retrieve information about connected monitors.
- Enable multi-screen support in the application.

### Added Dependency Injection

- Introduce dependency injection to enhance modularity and testability of the application.
- Use the appropriate DI container to manage dependencies.

### Added Window Chrome with Titlebar Buttons and Styles

- Customize the window chrome to match the application's style.
- Add minimize, maximize/restore, and close buttons to the title bar.

### Added AppState to remember Window Size and Position

- Implement AppState class to remember and restore window size and position.
- Utilize the Jot library for serialization and deserialization of the window state.

## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request. Make sure to follow the existing code style and conventions.

## Acknowledgements

Special thanks to the author of the Jot library for providing a convenient solution for serialization and deserialization of the window state. The Jot library greatly simplifies the process of storing and retrieving the window size and position, enhancing the functionality of this project.

- Jot Library: [GitHub](https://github.com/anakic/Jot)


## License

This project is licensed under the [MIT License](License.md). Feel free to modify and use the code as per the license terms.
