# MiniECS

MiniECS is a lightweight ECS framework for Unity, built for projects that want a clear separation between data, behavior, and game flow without adopting a heavier stack. The project combines:

- simple entities and components
- archetype-based system queries
- integration with GameObjects and the Unity Inspector
- component prototypes for editor-driven authoring
- global events and targeted messages

The goal is not to compete with DOTS/Entities, but to provide a smaller, more direct, and Unity-friendly foundation for games and prototypes.

## Overview

The main runtime flow revolves around these types:

- `ECSManager`: coordinates entities, archetypes, systems, `EventBus`, and `MessageBus`
- `EntityPrototypeController`: represents an entity in the scene and stores its component prototypes
- `IComponent`: describes pure data
- `UpdateSystem<...>`: processes entities that contain one or more components
- `IGameMode`: defines the main gameplay lifecycle (`BeforeStart`, `Start`, `Update`, `FixedUpdate`, `LateUpdate`, and more)
- `ECSController`: Unity component responsible for bootstrapping the runtime and connecting an `IGameMode`

In practice, you define components, register entities through `EntityPrototypeController`, create systems inside an `IGameMode`, and run those systems during Unity's game loop.

## Current Features

- Entity queries by component combination through archetypes
- Generic update systems with support for up to 5 component types
- Inspector-driven components using `SerializeReference`
- Prototype generation through the `GeneratePrototype` attribute
- Entity pooling and recycling through `ECSManager`
- ECS events (`IEvent`) and targeted `GameObject` messages (`IMessage`)
- Extension methods for activating/deactivating entities and safely accessing components
- Editor tests covering important archetype and component set behavior

## Requirements

- Unity `6000.2.7f2` or compatible
- `com.unity.test-framework` to run editor tests

This repository is already structured as a complete Unity project. If your goal is to study or extend the framework, you can open the repository root directly in Unity Hub.

## Opening the Project

1. Clone the repository:

```bash
git clone https://github.com/AllanDouglas/MiniECS
```

2. Open the cloned folder in Unity Hub.
3. Wait for scripts, analyzers, and assets to finish importing.
4. Open [`Assets/Tests/Main.unity`](/Volumes/Mac/workspaces/unity/MiniECS/Assets/Tests/Main.unity) to explore the current test scene.

## Core Concepts

### 1. Components

A component is pure data and implements `IComponent`.

```csharp
using System;
using MiniECS;

[Serializable]
public struct Position : IComponent
{
    public float x;
    public float y;
    public float z;
}

[Serializable]
public struct Velocity : IComponent
{
    public float x;
    public float y;
    public float z;
}
```

### 2. Prototypes for the Inspector

To add components through the editor on an `EntityPrototypeController`, create a prototype type based on `ComponentPrototype<T>`.

```csharp
using System;
using MiniECS;

[Serializable]
public sealed class VelocityPrototype : ComponentPrototype<Velocity> { }
```

If you want to automate this, the project also supports prototype generation through `GeneratePrototype`.

```csharp
using System;
using MiniECS;

[Serializable, GeneratePrototype("Game")]
public struct Health : IComponent
{
    public int current;
    public int max;
}

public sealed partial class HealthPrototype { }
```

The source generator lives in [`Source Generators/Generator.cs`](/Volumes/Mac/workspaces/unity/MiniECS/Source%20Generators/Generator.cs), and the generated analyzer assembly is included through [`Assets/Analyzers/MiniECSSourceGenerators.dll`](/Volumes/Mac/workspaces/unity/MiniECS/Assets/Analyzers/MiniECSSourceGenerators.dll).

### 3. Systems

Systems inherit from `UpdateSystem` and receive `ECSManager` through the constructor. Each `UpdateSystem<T...>` variation iterates over entities that contain the requested components.

```csharp
using MiniECS;

public sealed class MovementSystem : UpdateSystem<Position, Velocity>
{
    public MovementSystem(ECSManager ecsManager) : base(ecsManager) { }

    protected override void OnUpdate(FrameContext context, ref Position position, ref Velocity velocity)
    {
        position.x += velocity.x * context.DeltaTime;
        position.y += velocity.y * context.DeltaTime;
        position.z += velocity.z * context.DeltaTime;
    }
}
```

### 4. Game Mode

`IGameMode` owns the main lifecycle, but in practice you will usually want to inherit from `DefaultGameMode`.

`DefaultGameMode` already implements the runtime loop using `SystemsManager`:

- systems registered in `ecs.SystemsManager` run during `Update`
- systems registered with `runAtFixedUpdate = true` run during `FixedUpdate`
- queued events and messages are flushed during `LateUpdate`

That means you typically register your systems once and let `DefaultGameMode` drive them for you, instead of calling each system manually every frame.

```csharp
using MiniECS;

public sealed class ExampleGameMode : DefaultGameMode
{
    public void Start(ECSManager ecs)
    {
        ecs.SystemsManager.Register(new MovementSystem(ecs));
    }
}
```

If you need custom lifecycle behavior, you can still implement `IGameMode` directly. The previous `README` used an outdated example that called systems manually through an older `UpdateSystem.Update` pattern; the current API uses `FrameTime`, and `DefaultGameMode` already handles that internally.

## Minimal Usage Flow

1. Create a `GameObject` with `ECSController`.
2. In `ECSController`, assign an `IGameMode` to `_gameMode`.
3. Create one or more `GameObjects` with `EntityPrototypeController`.
4. Add the required `IComponentPrototype` instances to each entity.
5. Enter play mode. `ECSController` initializes `ECSManager`, registers discovered entities, and forwards the Unity loop to your `IGameMode`.

During initialization, `ECSController` also uses `FindObjectsByType<EntityPrototypeController>()`, so scene entities can be registered automatically.

## Accessing Components at Runtime

`ECSManager` exposes extension methods for retrieving components from an entity:

```csharp
ref var position = ref ecs.GetComponent<Position>(entity);
ref var velocity = ref ecs.TryGetComponent<Velocity>(entity, out bool hasVelocity);
```

Inside an `EntityPrototypeController`, you can also query the associated ECS components:

```csharp
if (entityController.HasComponent<Health>())
{
    ref var health = ref entityController.TryGetECSComponent<Health>(out bool hasHealth);
}
```

## Events and Messages

The project includes two communication mechanisms:

- `EventBus`: global ECS events based on `IEvent`, queued and flushed during `LateUpdate`
- `MessageBus`: targeted messages sent to a specific `GameObject`, based on `IMessage`

Use `EventBus` when communication is global and decoupled. Use `MessageBus` when there is a specific target in the scene.

## Pooling and Recycling

`ECSManager` provides helpers for reusing entities through a pool:

- `GetPooledEntityInstance(...)`
- `Recycle(entity)`

This is useful for bullets, enemies, pickups, or any short-lived object that still needs to remain integrated with the ECS runtime.

## Project Structure

- [`Assets/Scripts/Core`](/Volumes/Mac/workspaces/unity/MiniECS/Assets/Scripts/Core): entities, components, archetypes, systems, events, and messages
- [`Assets/Scripts/Behaviours`](/Volumes/Mac/workspaces/unity/MiniECS/Assets/Scripts/Behaviours): bridge layer between ECS and GameObjects/MonoBehaviours
- [`Assets/Editor`](/Volumes/Mac/workspaces/unity/MiniECS/Assets/Editor): custom drawers and editor tests
- [`Source Generators`](/Volumes/Mac/workspaces/unity/MiniECS/Source%20Generators): prototype generation

## Tests

The current tests live in [`Assets/Editor/Tests/ArchetypeTests.cs`](/Volumes/Mac/workspaces/unity/MiniECS/Assets/Editor/Tests/ArchetypeTests.cs) and mainly cover:

- `ComponentSet` composition
- component lookup behavior
- adding and removing entities from an `Archetype`

Run them through the Unity Test Runner in Edit Mode.

## License

This project is licensed under the MIT License.
