# 🎮 SA2DGE

## Shreyas's Advanced 2 Dimensional Game Engine

SA2DGE is a personal **2D game engine project built from scratch in C#**. The purpose of the project is not simply to make a framework capable of running a game, but to understand what actually happens inside a game engine and how its different systems work together. The engine is being developed from the foundation upward, with each subsystem studied, designed, implemented, tested, and connected to the rest of the engine. The long-term goal is to create a complete and usable 2D game engine while learning the engineering behind game development, graphics, architecture, mathematics, memory, resources, physics, audio, and other systems along the way.

---

## 🕹️ What We Are Building

The idea behind SA2DGE is to build the technology that normally sits underneath a 2D game. Eventually, a game built with SA2DGE should be able to start the engine, create a window, process input, run a game loop, update game objects, render sprites, use cameras, load resources, manage scenes, handle collision and physics, play audio, and communicate with these systems through a clean engine API.

The project is intentionally being built **from the foundation toward the actual game-facing systems**. We are not starting with an editor or a large demo game. The priority right now is the engine runtime itself. Once the runtime becomes stable, games can be built on top of it.

The overall direction is:

**🎮 Game Development → ⚙️ 2D Game Engine → 🧠 Core → 🖥️ Platform → 🪟 Window → 🎨 Graphics → 📐 Math → 🌍 Scene → 🧩 ECS → 📦 Resources → 💥 Physics → 🎞️ Animation → 🔊 Audio → 🖱️ UI → 💾 Serialization → 🛠️ Tools**

This is a development direction rather than a rigid rule. Engine systems depend on one another, so earlier systems will sometimes be revisited and improved when later systems introduce new requirements.

---

## 📍 Where We Are Right Now

SA2DGE is currently in the **early engine-foundation stage**.

The first work is concentrated around the **Core** and **Platform** layers. The Core establishes how SA2DGE itself starts and runs. This includes the engine lifecycle, application entry point, game representation, main game loop, time management, configuration, and engine-level exception handling.

The current foundation contains concepts such as `Engine`, `Application`, `Game`, `GameLoop`, `Time`, `EngineConfig`, and `EngineException`. These are intentionally being kept relatively simple at this stage. The goal is to establish a reliable runtime flow before adding complicated systems on top of it.

After the Core comes the **Platform layer**, which is where the engine starts communicating with the operating system. The current work is focused specifically on the **Window system**.

A game needs a real application window before the graphics system has somewhere to display its output. The Window layer will provide that functionality while keeping operating-system-specific implementation details isolated from the rest of SA2DGE. The rest of the engine should not need to care about the exact platform API being used to create or manage the window.

So, right now, the main development path is:

**⚙️ Engine → 🚀 Application → 🎮 Game → 🔄 Game Loop → 🖥️ Platform → 🪟 Window → 🎨 Graphics**

The immediate goal is to finish the Window foundation and reach the Graphics subsystem.

---

## 🔨 How We Are Developing It

SA2DGE is not being developed by simply creating a large number of classes and connecting them until something works. The approach is to understand the problem first and then design the smallest appropriate system around it.

Before implementing a subsystem, we look at what the subsystem is responsible for, what information it needs, what systems it depends on, what systems depend on it, and which details should remain hidden behind its interface. After that, the implementation is tested and integrated into the engine before moving further.

For example, with the current Window work, the goal is not merely to create a window. We need to understand how the engine owns the window, how its lifetime is managed, how the window communicates with the application loop, how events are processed, how resizing will eventually work, and how the graphics backend will obtain the information it needs from the platform layer.

The same process will be used throughout the project:

**🧠 Understand → 🏗️ Design → 💻 Implement → 🧪 Test → 🔗 Connect → ♻️ Refactor**

The goal is to avoid blindly generating large amounts of code. Each part of the engine should exist because there is an actual engine problem that it solves.

---

## 🎨 Reaching the Graphics System

The next major stage is **Graphics**.

Once the platform and window foundation is ready, the engine will begin dealing directly with rendering. This is one of the most important parts of SA2DGE because it is where the engine starts turning game data into something visible on the screen.

Before implementing the renderer, we will study the graphics concepts required to understand what we are building. This includes the graphics pipeline, GPU and CPU responsibilities, rendering APIs, buffers, textures, shaders, blending, coordinate systems, cameras, transformations, render targets, draw calls, and 2D sprite rendering.

The initial Graphics objective is intentionally simple:

> **✨ Get SA2DGE to render its first pixels.**

The expected flow will eventually look like:

**🚀 Application → ⚙️ Engine → 🔄 Game Loop → 🖥️ Platform → 🪟 Window → 🎨 Graphics → 🖌️ Renderer → GPU → 🖥️ Screen**

From there, the renderer can grow into a proper 2D rendering system with textures, sprites, cameras, batching, shaders, render targets, and other rendering functionality.

The first working renderer will be more important than premature optimization. Once rendering works correctly, profiling and measurement can be used to determine where optimization is actually necessary.

---

## 🧰 Technology Stack

SA2DGE is being developed primarily in **C#** using the **.NET** ecosystem. C# provides the language features needed to build a modular engine architecture, including classes, structs, interfaces, generics, delegates, events, collections, exception handling, and managed resource facilities.

### 💻 Core Stack

**Language:** C#

**Runtime:** .NET

**Engine Type:** 2D Game Engine

**Architecture:** Modular, subsystem-based architecture

**Development Tools:** JetBrains Rider / Visual Studio Code

**Version Control:** Git

**Repository:** GitHub

As development reaches the Graphics subsystem, the graphics backend and supporting libraries will be chosen based on what SA2DGE actually needs. The goal is to keep the engine architecture independent from unnecessary implementation details so that the core engine does not become tightly coupled to one specific external library too early.

---

## 🏗️ How the Engine Is Designed

The engine is being divided into logical subsystems so that each part has a clear responsibility.

The **🧠 Core** is responsible for the engine lifecycle and fundamental runtime behavior. It provides the foundation on which the rest of the engine operates.

The **🖥️ Platform** layer isolates operating-system-specific functionality. This includes the application window and eventually input devices and other platform services.

The **🎨 Graphics** subsystem will be responsible for rendering and communication with the graphics backend. This is the next major subsystem we are moving toward.

The **📐 Math** subsystem will eventually provide vectors, matrices, transformations, geometry, and the mathematical operations required by the engine.

The **🌍 Scene** system will represent the game world and its organization.

The **🧩 ECS** system will provide entities, components, and systems for organizing game objects and their behavior.

The **📦 Resources** system will manage assets such as textures, audio, and other game data while handling loading, caching, and lifetime.

The **💥 Physics** system will eventually handle collision detection, collision resolution, forces, velocity, gravity, and other physical behavior.

The **🎞️ Animation** system will handle sprite animation and other animation-related functionality.

The **🔊 Audio** system will provide sound effects, music, playback, and eventually mixing functionality.

The **🖱️ UI** system will provide functionality for game interfaces and engine-level UI.

Serialization and **🛠️ Tools** will be developed later as the engine becomes capable of representing more complex scenes, assets, and game data.

The important architectural principle is that these systems should have clear boundaries. A subsystem should not need to know the internal implementation details of another subsystem when a clean interface or abstraction can be used instead.

---

## 🧭 Current Development Path

### 🎮 Game Development
Building games requires understanding the systems underneath them.

↓

### ⚙️ 2D Game Engine
SA2DGE is being built as that underlying runtime.

↓

### 🧠 Core
The engine lifecycle and runtime foundation are being established.

↓

### 🖥️ Platform
The engine begins communicating with the operating system.

↓

### 🪟 Window
**🔨 Current work.**

↓

### 🎨 Graphics
**🎯 Next major target.**

↓

### 🖌️ Renderer
The first rendering foundation will be implemented.

↓

### 🖼️ Textures / Sprites / Camera / Shaders
The engine will begin producing real 2D graphics.

---

## 🌱 Long-Term Direction

The long-term goal is for SA2DGE to grow from a small engine runtime into a complete 2D game-development foundation. Eventually, the engine should provide enough functionality that an actual 2D game can be created using SA2DGE itself.

That means the project will eventually need to solve problems involving rendering, input, scenes, entities, components, resources, collision, physics, animation, audio, UI, serialization, debugging, memory usage, and performance.

The project is also intentionally being treated as a software-engineering learning project. Every subsystem gives us an opportunity to understand a different area of computer science and game development. Rendering teaches GPU architecture and graphics pipelines. ECS teaches data organization and architecture. Physics teaches mathematics and simulation. Resource management teaches ownership and lifetime. The game loop teaches timing and real-time systems. Platform abstraction teaches how software communicates with the operating system.

The objective is therefore not to rush toward a finished engine. The objective is to **build the engine properly while understanding why it works**.

---

## 🎯 Current Target

The immediate target is:

**🪟 Finish Platform / Window → 🎨 Reach Graphics → 🖌️ Build the rendering foundation → ✨ Render the first pixels with SA2DGE.**

From there, the engine will continue growing subsystem by subsystem.

---

<div align="center">

### 🎮 SA2DGE

**Learn how engines work. Build one from scratch. Then build games with it.**

</div>
