# MiniECS

MiniECS is a lightweight Entity Component System (ECS) framework designed for Unity projects. It aims to provide a simple, efficient, and flexible architecture for managing game entities and their behaviors.

## Features

- Minimal and easy-to-understand ECS implementation
- Decouples data (components) from logic (systems)
- Seamless integration with Unity workflows

## Getting Started

1. **Clone the repository:**
    ```bash
    git clone https://github.com/allandouglas/MiniECS.git
    ```

2. **Import into Unity:**
    - Copy the `MiniECS` folder into your Unity project's `Assets` directory.

3. **Usage Example:**
    ```csharp
    // Define a component
    public struct Position : IComponent
    {
        public float x, y, z;
    }

    public struct Velocity : IComponent
    {
        public float x, y, z;
    }

    // Define a system using UpdateSystem
    public class MovementSystem : UpdateSystem<Position, Velocity>
    {
        public MovementSystem(Game game) : base(game) { }

        protected override void OnUpdate(FilterContext context, ref Position pos, ref Velocity vel)
        {
            pos.x += vel.x * context.DeltaTime;
            pos.y += vel.y * context.DeltaTime;
            pos.z += vel.z * context.DeltaTime;
        }
    }

    // Example GameMode using MovementSystem
    public class ExampleGameMode : IGameMode
    {
        private MovementSystem movementSystem;

        public void OnEnable(Game game)
        {
            movementSystem = new MovementSystem(game);
        }

        public void OnDisable(Game game) { }
        public void Start(Game game) { }

        public void Update(Game game)
        {
            movementSystem.Update(game, Time.deltaTime);
        }

        public void FixedUpdate(Game game) { }
        public void LateUpdate(Game game) { }
    }

    ```

## License

This project is licensed under the MIT License.
