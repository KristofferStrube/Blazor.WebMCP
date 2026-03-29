[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](/LICENSE)
[![GitHub issues](https://img.shields.io/github/issues/KristofferStrube/Blazor.WebMCP)](https://github.com/KristofferStrube/Blazor.WebMCP/issues)
[![GitHub forks](https://img.shields.io/github/forks/KristofferStrube/Blazor.WebMCP)](https://github.com/KristofferStrube/Blazor.WebMCP/network/members)
[![GitHub stars](https://img.shields.io/github/stars/KristofferStrube/Blazor.WebMCP)](https://github.com/KristofferStrube/Blazor.WebMCP/stargazers)
<!-- [![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/KristofferStrube.Blazor.WebMCP?label=NuGet%20Downloads)](https://www.nuget.org/packages/KristofferStrube.Blazor.WebMCP/)-->

# Blazor.WebMCP
A Blazor wrapper for the [WebMCP browser API.](https://github.com/webmachinelearning/webmcp)
The Web API enables the browser to provide tools that can be accessed by AI agents and assistive technologies to create collaborative, human-in-the-loop workflows. This project implements a wrapper around the API for Blazor so that we can easily and safely expose our own tools in C# through the WebMCP API.

**The browser API is not stable yet, so this project might break in the future if the API changes**

# Demo
The sample project can be demoed at https://kristofferstrube.github.io/Blazor.WebMCP/

On each page, you can find the corresponding code for the example in the top right corner.

On the [API Coverage Status](https://kristofferstrube.github.io/Blazor.WebMCP/Status) page, you can see how much of the WebIDL specs this wrapper has covered.

# Related repositories
The library uses the following other packages to support its features:
- https://github.com/KristofferStrube/Blazor.WebIDL (To make error handling JSInterop)

# Related articles
This repository was built with help from the following series of articles:

- [Typed exceptions for JSInterop in Blazor](https://kristoffer-strube.dk/post/typed-exceptions-for-jsinterop-in-blazor/)
- [Blazor WASM 404 error and fix for GitHub Pages](https://blog.elmah.io/blazor-wasm-404-error-and-fix-for-github-pages/)
- [How to fix Blazor WASM base path problems](https://blog.elmah.io/how-to-fix-blazor-wasm-base-path-problems/)
