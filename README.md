# Input System Prototype

This repository implements a modular input system based on the provided technical specification. It contains three class library projects:

- **Input.Abstractions** – shared interfaces and action logic
- **Input.Config** – YAML configuration loader using YamlDotNet
- **Input.Raylib** – platform implementation using Raylib-cs

The solution targets **.NET 6** and organizes input actions into reusable traits and binding sets. The YAML loader deserializes configuration files similar to the example in the specification.
