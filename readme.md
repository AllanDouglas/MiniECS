# MiniECS

MiniECS is a lightweight Entity Component System (ECS) framework designed for Unity projects. It aims to provide a simple, efficient, and flexible architecture for managing ECSManager entities and their behaviors.

## Features

- Minimal and easy-to-understand ECS implementation
- Decouples data (components) from logic (systems)
- Seamless integration with Unity workflows

## Getting Started

1. **Clone the repository:**
    ```bash
    git clone https://github.com/AllanDouglas/MiniECS
    ```

2. **Import into Unity:**
    - Copy the `MiniECS` folder into your Unity project's `Assets` directory.

3. **Usage Example:**
    ```csharp
    // Define a component
    [Serializable, GeneratePrototype(targetNamespace: "Game")]
    public struct Position : IComponent
    {
        public float x, y, z;
    }

    // optional: Define a Component Prototype to be able to add a this component on editor
    public sealed partial class PositionPrototype { }
    
    [Serializable]
    public struct Velocity : IComponent
    {
        public float x, y, z;
    }
    [Serializable]
    public sealed class VelocityPrototype : ComponentPrototype<Velocity> { }

    // Define a system using UpdateSystem
    public class MovementSystem : UpdateSystem<Position, Velocity>
    {
        public MovementSystem(ECSManager ECSManager) : base(ECSManager) { }

        protected override void OnUpdate(FilterContext context, ref Position pos, ref Velocity vel)
        {
            pos.x += vel.x * context.DeltaTime;
            pos.y += vel.y * context.DeltaTime;
            pos.z += vel.z * context.DeltaTime;
        }
    }

    // Example ECSManagerMode using MovementSystem
    public class ExampleGameMode : IGameMode
    {
        private MovementSystem movementSystem;

        public void OnEnable(ECSManager ecsManager)
        {
            movementSystem = new MovementSystem(ecsManager);
        }

        public void OnDisable(ECSManager ecsManager) { }
        public void Start(ECSManager ecsManager) { }

        public void Update(ECSManager ecsManager)
        {
            movementSystem.Update(ecsManager, Time.deltaTime);
        }

        public void FixedUpdate(ECSManager ecsManager) { }
        public void LateUpdate(ECSManager ecsManager) { }
    }
    //TODO : Work in progress
    ```
4. Create a EntityController
    - Add the EntityController to a GameObject:

    - In the Unity Editor, create an empty GameObject.
    Attach the EntityController script to this GameObject.
    Assign Component Prototypes:

    - In the Inspector, you’ll see a list where you can add component prototypes (like PositionPrototype, VelocityPrototype).
    Add and configure these prototypes as needed.
    Access and Modify Components in Code:

    - You can get a reference to the EntityController and use its API to add, remove, or query components at runtime.
## License

This project is licensed under the MIT License.
