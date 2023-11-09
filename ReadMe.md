<a name="readme-top"></a>

<h3 align="center">Window Chrome Multiscreen</h3>
<p align="center">
   This is a sample WPF project named "WindowChromeMultiscreen" that demonstrates the usage of the WindowChrome 
   style to customize the title bar in a WPF window.
   <br />
   <a href="https://github.com/sygmond/WindowChromeMultiscreen/issues/new?assignees=&labels=bug&projects=&title=">Report Bug</a>
   ·
   <a href="https://github.com/sygmond/WindowChromeMultiscreen/issues/new?assignees=&labels=enhancement&projects=&title=">Request Feature</a>
</p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
   <summary>Table of Contents</summary>
   <ol>
      <li>
         <a href="#description">Description</a>
      </li>
      <li>
         <a href="#getting-started">Getting Started</a>
         <ul>
            <li><a href="#prerequisites">Prerequisites</a></li>
            <li><a href="#setup">Setup</a></li>
         </ul>
      </li>
      <li><a href="#commit-history">Commit history</a></li>
      <li><a href="#contributing">Contributing</a></li>
      <li><a href="#license">License</a></li>
      <li><a href="#acknowledgements">Acknowledgements</a></li>
   </ol>
</details>

<!-- DESCRIPTION -->
## Description

The project showcases how to use the `WindowChrome` class in WPF to create a custom title bar that matches the style of
 the application. 
 
 It provides a multi-screen functionality by utilizing native methods to retrieve information about 
 the connected monitors.

 Additionally, it incorporates following features:
- Customized title bar using WindowChrome style
- Multi-screen support using native methods
- Dependency injection for enhanced modularity
- Window chrome with title bar buttons and styles
- AppState to remember window size and position using the [Jot library](https://github.com/anakic/Jot)

<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

* Visual Studio 17.5+
* WPF workload
* .NET 7.0  

### Setup
To get started with the WindowChromeMultiscreen project, follow these steps:

1. Clone the repository:

   ```bash
   git clone https://github.com/sygmond/WindowChromeMultiscreen.git
2. Open the solution file, `WindowChromeMultiscreen.sln`, in Visual Studio.
3. Build the solution to restore NuGet packages and compile the project.
4. Run the application to see the sample window with a customized title bar and multi-screen support.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- COMMIT HISTORY -->
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


<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- CONTRIBUTING -->
## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request. Make sure to follow the existing code style and conventions.
<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- LICENSE -->
## License

This project is licensed under the [MIT License](LICENSE). Feel free to modify and use the code as per the license terms.


<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements

Special thanks to the author of the Jot library for providing a convenient solution for serialization and deserialization of the window state. The Jot library greatly simplifies the process of storing and retrieving the window size and position, enhancing the functionality of this project.

- Jot Library: [GitHub](https://github.com/anakic/Jot)

<p align="right">(<a href="#readme-top">back to top</a>)</p>