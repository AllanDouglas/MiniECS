using System.Collections;
using MiniECS;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ArchetypeTests
{
    private struct AComponent : IComponent
    {
        public char value;
    }
    private struct BComponent : IComponent
    {
        public char value;
    }
    private struct CComponent : IComponent
    {
        public char value;
    }

    [Test]
    public void Should_Contains_Component_Generics()
    {
        var componentSet = ComponentSet.New<AComponent>();
        Assert.True(componentSet.Contains<AComponent>());
    }

    [Test]
    public void Should_Contains_Two_Component_Generics()
    {
        var aComp = ComponentSet.New<AComponent>();
        var bCompId = ComponentIdHelper.GetID<BComponent>();

        var aPlusBComponentSet = ComponentSet.Add(aComp, bCompId);

        Assert.True(aPlusBComponentSet.Contains<AComponent>());
        Assert.True(aPlusBComponentSet.Contains<BComponent>());
    }

    [Test]
    public void Should_Contains_The_Last_Added_Component_Generics()
    {
        var aSet = ComponentSet.New<AComponent>();
        var bCompId = ComponentIdHelper.GetID<BComponent>();
        var cCompId = ComponentIdHelper.GetID<CComponent>();

        var abSet = ComponentSet.Add(aSet, bCompId);
        var abcSet = ComponentSet.Add(abSet, cCompId);

        Assert.True(abcSet.Contains<CComponent>());
    }

    [Test]
    public void Should_No_Contains_Component_Generics()
    {
        var aComp = ComponentSet.New<AComponent>();
        Assert.False(aComp.Contains<BComponent>());
    }

    [Test]
    public void Should_Contains_Entity()
    {
        var arch = new Archetype(1, 1);
        var componentA = new AComponent();
        var e = new Entity(0u);

        arch.Add(e, componentA, 1);

        Assert.True(arch.ContainsEntity(e));
    }

    [Test]
    public void Should_Add_Component_To_Archetype()
    {
        var arch = new Archetype(1, 1);
        var componentA = new AComponent();
        var e = new Entity(0u);

        arch.Add(e, componentA, 1);

        Assert.AreEqual(arch.EntitiesCount, 1);
    }

    [Test]
    public void Should_Remove_Component_To_Archetype()
    {
        var arch = new Archetype(1, 1);
        var componentA = new AComponent();
        var e = new Entity(0u);

        arch.Add(e, componentA);
        arch.Remove(e);

        Assert.AreEqual(arch.EntitiesCount, 0);
    }

    [Test]
    public void Should_Add_Many_Component_To_Entity()
    {
        var arch = new Archetype(1, 2);
        var componentA = new AComponent() { value = 'a' };
        var componentB = new BComponent() { value = 'b' };

        var e = new Entity(0u);

        arch.Add(e, componentA);
        arch.Add(e, componentB);

        var getComponentA = arch.GetComponent<AComponent>(e);
        var getComponentB = arch.GetComponent<BComponent>(e);

        Assert.AreEqual(getComponentA.value, 'a');
        Assert.AreEqual(getComponentB.value, 'b');
    }


}
