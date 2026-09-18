# SA2DGE

### Shreyas's Advanced 2D Game Engine

> A custom 2D game engine built from scratch in C# to understand, design, and implement the systems that make a game engine work.

---

## 🎯 Project Goal

SA2DGE is being developed **from the ground up**, one subsystem at a time.

The focus is not on quickly making a game. The focus is on understanding the engineering behind an engine:

- How the engine starts and runs
- How it communicates with the operating system
- How a game loop works
- How graphics are rendered
- How assets and scenes are managed
- How entities, physics, audio, and other systems interact

The architecture is designed to stay **modular, testable, and understandable** as the engine grows.

---

## 🗺️ Development Roadmap

```text
┌─────────────────────┐
│        Core         │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│  Platform / Window  │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│      Graphics       │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│        Math         │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│       Scene         │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│        ECS          │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│      Resources      │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│      Physics        │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│     Animation       │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│       Audio         │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│         UI          │
└──────────┬──────────┘
           ↓
┌─────────────────────┐
│ Serialization/Tools │
└─────────────────────┘
```

---

## 📍 Current Status

### ✅ Core — Foundation

The initial engine foundation is being established.

Current core systems:

- `Engine`
- `Application`
- `Game`
- `GameLoop`
- `Time`
- `EngineConfig`
- `EngineException`

### 🔨 Current Focus — Platform / Window

We are currently building the **Platform layer**, starting with window management.

```text
SA2DGE
│
├── Core
│   ├── Engine
│   ├── Application
│   ├── Game
│   ├── GameLoop
│   ├── Time
│   └── EngineConfig
│
└── Platform
    └── Window
        └── Window.cs   ← CURRENT
```

The Platform layer provides the boundary between the engine and the underlying operating system.

---

## 🚀 Immediate Milestone

The current objective is to reach the **Graphics subsystem** with a functioning:

```text
Application
     │
     ↓
Engine
     │
     ↓
Game Loop
     │
     ↓
Platform
     │
     ↓
Window
     │
     ↓
Graphics
     │
     ↓
Renderer
```

Once the Graphics layer begins, development will move toward:

- Rendering backend
- Renderer
- Textures
- Sprites
- Sprite batching
- Cameras
- Shaders
- Render targets
- 2D rendering pipeline

---

## 🧠 Engineering Philosophy

SA2DGE follows a simple development cycle:

```text
        ┌─────────┐
        │  Learn  │
        └────┬────┘
             ↓
        ┌─────────┐
        │ Design  │
        └────┬────┘
             ↓
        ┌─────────┐
        │Implement│
        └────┬────┘
             ↓
        ┌─────────┐
        │  Test   │
        └────┬────┘
             ↓
        ┌─────────┐
        │Refactor │
        └────┬────┘
             │
             └──────────────→ Repeat
```

Every subsystem should have a clear responsibility and a well-defined relationship with the rest of the engine.

---

## 🛠️ Technology

| Area | Technology |
|---|---|
| Language | C# |
| Engine Type | 2D |
| Development Style | From Scratch |
| Architecture | Modular / Subsystem-Based |
| Current Stage | Core → Platform → Graphics |

---

## 📌 Project Status

**Early Development**

> Current target: **Finish Platform / Window → Begin Graphics → Get the first pixels rendered.**

---

### SA2DGE

**Learn the systems. Build the engine. Understand the technology.**
