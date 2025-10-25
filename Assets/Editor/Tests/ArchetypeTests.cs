using MiniECS;
using NUnit.Framework;
using static MiniECS.Archetype;
public class ArchetypeTests
{


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

        var aPlusBComponentSet = ComponentSet.Sum(aComp, bCompId);

        Assert.True(aPlusBComponentSet.Contains<AComponent>());
        Assert.True(aPlusBComponentSet.Contains<BComponent>());
    }

    [Test]
    public void Should_Component_Position_Be_3()
    {
        var set = ComponentSet.New<AComponent>();
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<BComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<CComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<DComponent>());

        Assert.AreEqual(3, set.GetPositionOf<DComponent>());
    }

    [Test]
    public void Should_Contains_15_Components()
    {
        var set = ComponentSet.New<AComponent>();
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<BComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<CComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<DComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<EComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<FComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<GComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<HComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<I_Component>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<JComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<KComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<LComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<MComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<NComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<OComponent>());
        //set = ComponentSet.Sum(set, ComponentIdHelper.GetID<PComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<QComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<RComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<SComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<TComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<UComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<VComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<WComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<XComponent>());

        Assert.AreEqual(15, set.count);
        Assert.AreEqual(0, set.GetPositionOf<AComponent>());
        Assert.AreEqual(3, set.GetPositionOf<DComponent>());
        Assert.AreEqual(10, set.GetPositionOf<KComponent>());
        Assert.AreEqual(14, set.GetPositionOf<OComponent>());

    }

    [Test]
    public void First_Component_Should_Be_At_0()
    {
        var set = ComponentSet.New<AComponent>();
        Assert.AreEqual(0, set.GetPositionOf<AComponent>());
    }
    [Test]
    public void First_Component_Should_Remaining_At_0()
    {
        var set = ComponentSet.New<AComponent>();
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<BComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<CComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<DComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<EComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<FComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<GComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<HComponent>());
        set = ComponentSet.Sum(set, ComponentIdHelper.GetID<I_Component>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<JComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<KComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<LComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<MComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<NComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<OComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<PComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<QComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<RComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<SComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<TComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<UComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<VComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<WComponent>());
        // set = ComponentSet.Sum(set, ComponentIdHelper.GetID<XComponent>());

        Assert.AreEqual(0, set.GetPositionOf<AComponent>());
    }


    [Test]
    public void Should_Contains_The_Last_Added_Component_Generics()
    {
        var aSet = ComponentSet.New<AComponent>();
        var bCompId = ComponentIdHelper.GetID<BComponent>();
        var cCompId = ComponentIdHelper.GetID<CComponent>();

        var abSet = ComponentSet.Sum(aSet, bCompId);
        var abcSet = ComponentSet.Sum(abSet, cCompId);

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
        arch.RemoveEntity(e);

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
    private struct DComponent : IComponent
    {
        public char value;
    }

    private struct EComponent : IComponent
    {
        public char value;
    }


    private struct FComponent : IComponent
    {
        public char value;
    }
    private struct GComponent : IComponent
    {
        public char value;
    }

    private struct HComponent : IComponent
    {
        public char value;
    }

    private struct I_Component : IComponent
    {
        public char value;
    }

    private struct JComponent : IComponent
    {
        public char value;
    }

    private struct KComponent : IComponent
    {
        public char value;
    }

    private struct LComponent : IComponent
    {
        public char value;
    }

    private struct MComponent : IComponent
    {
        public char value;
    }

    private struct NComponent : IComponent
    {
        public char value;
    }

    private struct OComponent : IComponent
    {
        public char value;
    }

    private struct PComponent : IComponent
    {
        public char value;
    }

    private struct QComponent : IComponent
    {
        public char value;
    }

    private struct RComponent : IComponent
    {
        public char value;
    }

    private struct SComponent : IComponent
    {
        public char value;
    }

    private struct TComponent : IComponent
    {
        public char value;
    }

    private struct UComponent : IComponent
    {
        public char value;
    }

    private struct VComponent : IComponent
    {
        public char value;
    }

    private struct WComponent : IComponent
    {
        public char value;
    }

    private struct XComponent : IComponent
    {
        public char value;
    }


}
