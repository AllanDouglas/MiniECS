using UnityEngine;
using MiniECS;
using System;

[Serializable, GeneratePrototype]
public struct TestComponent : IComponent
{
    public int x;
    public float y;
}


public sealed class GameMode : IGameMode
{
    public void FixedUpdate(ECSManager ecs)
    {

    }

    public void LateUpdate(ECSManager ecs)
    {

    }

    public void OnDestroy(ECSManager ecs)
    {

    }

    public void OnDisable(ECSManager ecs)
    {

    }

    public void OnEnable(ECSManager ecs)
    {

    }

    public void Start(ECSManager ecs)
    {
        ecs.SystemsManager.Register<TestSystem>(new(ecs));
    }

    public void Update(ECSManager ecs)
    {
        for (int i = 0; i < ecs.SystemsManager.Systems.Length; i++)
        {
            ecs.SystemsManager.Systems[i].Update(new(Time.deltaTime, Time.time));
        }
    }
}

public sealed class TestSystem : UpdateSystem<TestComponent>
{
    private int i;
    private float startTime;
    public TestSystem(ECSManager eCSManager) : base(eCSManager)
    {
    }

    protected override void OnUpdate(FrameContext context, ref TestComponent component)
    {

        // if (i > 10_000)
        // {
        //     Debug.Log($"Time {context.time - startTime}");
        //     Debug.Break();
        // }

        // if (i == 0)
        // {
        //     startTime = context.time;
        // }
        
        // i++;


    }
}
