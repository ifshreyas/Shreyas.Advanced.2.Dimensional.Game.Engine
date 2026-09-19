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

# 🎮 SA2DGE — Development Checkpoint

SA2DGE made a major step forward today. The project structure and solution were established, the available SDK was migrated to **.NET 10**, and the runtime foundation was connected to a real Windows graphics backend using **Vortice D3D11/DXGI**. The core runtime systems including `Game`, `Application`, `Engine`, and `GameLoop` are now in place. The Windows native window backend was implemented, along with native memory utilities and the `GraphicsBackend` abstraction. The Direct3D 11 backend can now create a D3D11 device, initialize a flip-model swap chain, create the render target, clear the frame, and present it to the screen. We also verified the fixed 60 Hz timestep and proper runtime shutdown/disposal behavior. Most importantly, the runtime smoke test succeeded and a **red frame was actually rendered on the Windows window**, proving that SA2DGE has successfully reached the real graphics pipeline.

---

## 🚀 Current Stage

The runtime bootstrap is now **essentially complete**. We have proven that the engine can start, create its native window, initialize Direct3D 11, render a frame, present it, and shut down cleanly. This means we are no longer working only with engine architecture and placeholders — SA2DGE has a functioning native runtime foundation.

The next stage is **Phase 2.2 — Windows Platform**. The focus will now move toward making the platform layer more complete and properly integrated instead of treating the native window as only a graphics bootstrap. This will establish the foundation needed for input, window events, resizing, lifecycle handling, and other platform services. After that, we will move into the input backend, strengthen the graphics backend, and eventually build the first real 2D renderer capable of drawing actual game objects and sprites.

---

## 🧭 Next Path

**Windows Platform → Input Backend → Graphics Backend → First Real 2D Renderer → Runtime Integration → Tests & Validation**

The next goal is simple:

> **Build the proper Windows platform layer on top of the runtime foundation we have already verified.**

🔴 **Today:** SA2DGE successfully rendered its first real frame.  
🟢 **Next:** Make the platform layer production-ready and continue toward the first real 2D renderer.

---

### SA2DGE

**The foundation works. Now we build the engine.**
