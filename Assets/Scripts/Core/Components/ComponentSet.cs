// using System;

// namespace MiniECS
// {
//     public readonly struct ComponentSet<TComponent> : IEquatable<ComponentSet<TComponent>>
//         where TComponent : struct, IComponent
//     {
//         public static ComponentSet<TComponent> New() => new(HashCode.Combine(typeof(TComponent)));

//         public readonly int Id;
//         private readonly ComponentID _componentID;

//         private ComponentSet(int id)
//         {
//             Id = id;
//             _componentID = ComponentIdHelper.GetID<TComponent>();
//         }

//         public readonly bool HasComponent<T>()
//             where T : struct, IComponent
//             => _componentID == ComponentIdHelper.GetID<T>();

//         public readonly bool HasComponent(ComponentID componentID)
//             => _componentID == componentID;

//         public bool Equals(ComponentSet<TComponent> other) => Id == other.Id;
//         public override bool Equals(object? obj) => obj is ComponentSet<TComponent> other && Equals(other);
//         public override int GetHashCode() => Id;
//     }

//     public readonly struct ComponentSet<TComponent, TComponent1> : IEquatable<ComponentSet<TComponent, TComponent1>>
//         where TComponent : struct, IComponent
//         where TComponent1 : struct, IComponent
//     {
//         public static ComponentSet<TComponent, TComponent1> New() =>
//             new(HashCode.Combine(typeof(TComponent), typeof(TComponent1)));

//         public readonly int Id;
//         private readonly ComponentID _componentID;
//         private readonly ComponentID _componentID1;

//         private ComponentSet(int id)
//         {
//             Id = id;
//             _componentID = ComponentIdHelper.GetID<TComponent>();
//             _componentID1 = ComponentIdHelper.GetID<TComponent1>();
//         }

//         public readonly bool HasComponent<T>()
//             where T : struct, IComponent
//             => ComponentIdHelper.GetID<T>() == _componentID || ComponentIdHelper.GetID<T>() == _componentID1;

//         public readonly bool HasComponent(ComponentID componentID)
//             => componentID == _componentID || componentID == _componentID1;

//         public bool Equals(ComponentSet<TComponent, TComponent1> other) => Id == other.Id;
//         public override bool Equals(object? obj) => obj is ComponentSet<TComponent, TComponent1> other && Equals(other);
//         public override int GetHashCode() => Id;
//     }

//     public readonly struct ComponentSet<TComponent, TComponent1, TComponent2> : IEquatable<ComponentSet<TComponent, TComponent1, TComponent2>>
//         where TComponent : struct, IComponent
//         where TComponent1 : struct, IComponent
//         where TComponent2 : struct, IComponent
//     {
//         public static ComponentSet<TComponent, TComponent1, TComponent2> New() =>
//             new(HashCode.Combine(typeof(TComponent), typeof(TComponent1), typeof(TComponent2)));

//         public readonly int Id;
//         private readonly ComponentID _componentID;
//         private readonly ComponentID _componentID1;
//         private readonly ComponentID _componentID2;

//         private ComponentSet(int id)
//         {
//             Id = id;
//             _componentID = ComponentIdHelper.GetID<TComponent>();
//             _componentID1 = ComponentIdHelper.GetID<TComponent1>();
//             _componentID2 = ComponentIdHelper.GetID<TComponent2>();
//         }

//         public readonly bool HasComponent<T>()
//             where T : struct, IComponent
//             => ComponentIdHelper.GetID<T>() == _componentID
//                 || ComponentIdHelper.GetID<T>() == _componentID1
//                 || ComponentIdHelper.GetID<T>() == _componentID2;

//         public readonly bool HasComponent(ComponentID componentID)
//             => componentID == _componentID
//                 || componentID == _componentID1
//                 || componentID == _componentID2;

//         public bool Equals(ComponentSet<TComponent, TComponent1, TComponent2> other) => Id == other.Id;
//         public override bool Equals(object? obj) => obj is ComponentSet<TComponent, TComponent1, TComponent2> other && Equals(other);
//         public override int GetHashCode() => Id;
//     }

//     public readonly struct ComponentSet<TComponent, TComponent1, TComponent2, TComponent3> : IEquatable<ComponentSet<TComponent, TComponent1, TComponent2, TComponent3>>
//         where TComponent : struct, IComponent
//         where TComponent1 : struct, IComponent
//         where TComponent2 : struct, IComponent
//         where TComponent3 : struct, IComponent
//     {
//         public static ComponentSet<TComponent, TComponent1, TComponent2, TComponent3> New() =>
//             new(HashCode.Combine(typeof(TComponent), typeof(TComponent1), typeof(TComponent2), typeof(TComponent3)));

//         public readonly int Id;
//         private readonly ComponentID _componentID;
//         private readonly ComponentID _componentID1;
//         private readonly ComponentID _componentID2;
//         private readonly ComponentID _componentID3;

//         private ComponentSet(int id)
//         {
//             Id = id;
//             _componentID = ComponentIdHelper.GetID<TComponent>();
//             _componentID1 = ComponentIdHelper.GetID<TComponent1>();
//             _componentID2 = ComponentIdHelper.GetID<TComponent2>();
//             _componentID3 = ComponentIdHelper.GetID<TComponent3>();
//         }

//         public readonly bool HasComponent<T>()
//             where T : struct, IComponent
//             => ComponentIdHelper.GetID<T>() == _componentID
//                 || ComponentIdHelper.GetID<T>() == _componentID1
//                 || ComponentIdHelper.GetID<T>() == _componentID2
//                 || ComponentIdHelper.GetID<T>() == _componentID3;

//         public readonly bool HasComponent(ComponentID componentID)
//             => componentID == _componentID
//                 || componentID == _componentID1
//                 || componentID == _componentID2
//                 || componentID == _componentID3;

//         public bool Equals(ComponentSet<TComponent, TComponent1, TComponent2, TComponent3> other) => Id == other.Id;
//         public override bool Equals(object? obj) => obj is ComponentSet<TComponent, TComponent1, TComponent2, TComponent3> other && Equals(other);
//         public override int GetHashCode() => Id;
//     }

//     public readonly struct ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4> : IEquatable<ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4>>
//         where TComponent : struct, IComponent
//         where TComponent1 : struct, IComponent
//         where TComponent2 : struct, IComponent
//         where TComponent3 : struct, IComponent
//         where TComponent4 : struct, IComponent
//     {
//         public static ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4> New() =>
//             new(HashCode.Combine(typeof(TComponent), typeof(TComponent1), typeof(TComponent2), typeof(TComponent3), typeof(TComponent4)));

//         public readonly int Id;
//         private readonly ComponentID _componentID;
//         private readonly ComponentID _componentID1;
//         private readonly ComponentID _componentID2;
//         private readonly ComponentID _componentID3;
//         private readonly ComponentID _componentID4;

//         private ComponentSet(int id)
//         {
//             Id = id;
//             _componentID = ComponentIdHelper.GetID<TComponent>();
//             _componentID1 = ComponentIdHelper.GetID<TComponent1>();
//             _componentID2 = ComponentIdHelper.GetID<TComponent2>();
//             _componentID3 = ComponentIdHelper.GetID<TComponent3>();
//             _componentID4 = ComponentIdHelper.GetID<TComponent4>();
//         }

//         public readonly bool HasComponent<T>()
//             where T : struct, IComponent
//             => ComponentIdHelper.GetID<T>() == _componentID
//                 || ComponentIdHelper.GetID<T>() == _componentID1
//                 || ComponentIdHelper.GetID<T>() == _componentID2
//                 || ComponentIdHelper.GetID<T>() == _componentID3
//                 || ComponentIdHelper.GetID<T>() == _componentID4;

//         public readonly bool HasComponent(ComponentID componentID)
//             => componentID == _componentID
//                 || componentID == _componentID1
//                 || componentID == _componentID2
//                 || componentID == _componentID3
//                 || componentID == _componentID4;

//         public bool Equals(ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4> other) => Id == other.Id;
//         public override bool Equals(object? obj) => obj is ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4> other && Equals(other);
//         public override int GetHashCode() => Id;
//     }

//     public readonly struct ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5> : IEquatable<ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5>>
//         where TComponent : struct, IComponent
//         where TComponent1 : struct, IComponent
//         where TComponent2 : struct, IComponent
//         where TComponent3 : struct, IComponent
//         where TComponent4 : struct, IComponent
//         where TComponent5 : struct, IComponent
//     {
//         public static ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5> New() =>
//             new(HashCode.Combine(typeof(TComponent), typeof(TComponent1), typeof(TComponent2), typeof(TComponent3), typeof(TComponent4), typeof(TComponent5)));

//         public readonly int Id;
//         private readonly ComponentID _componentID;
//         private readonly ComponentID _componentID1;
//         private readonly ComponentID _componentID2;
//         private readonly ComponentID _componentID3;
//         private readonly ComponentID _componentID4;
//         private readonly ComponentID _componentID5;

//         private ComponentSet(int id)
//         {
//             Id = id;
//             _componentID = ComponentIdHelper.GetID<TComponent>();
//             _componentID1 = ComponentIdHelper.GetID<TComponent1>();
//             _componentID2 = ComponentIdHelper.GetID<TComponent2>();
//             _componentID3 = ComponentIdHelper.GetID<TComponent3>();
//             _componentID4 = ComponentIdHelper.GetID<TComponent4>();
//             _componentID5 = ComponentIdHelper.GetID<TComponent5>();
//         }

//         public readonly bool HasComponent<T>()
//             where T : struct, IComponent
//             => ComponentIdHelper.GetID<T>() == _componentID
//                 || ComponentIdHelper.GetID<T>() == _componentID1
//                 || ComponentIdHelper.GetID<T>() == _componentID2
//                 || ComponentIdHelper.GetID<T>() == _componentID3
//                 || ComponentIdHelper.GetID<T>() == _componentID4
//                 || ComponentIdHelper.GetID<T>() == _componentID5;

//         public readonly bool HasComponent(ComponentID componentID)
//             => componentID == _componentID
//                 || componentID == _componentID1
//                 || componentID == _componentID2
//                 || componentID == _componentID3
//                 || componentID == _componentID4
//                 || componentID == _componentID5;

//         public bool Equals(ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5> other) => Id == other.Id;
//         public override bool Equals(object? obj) => obj is ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5> other && Equals(other);
//         public override int GetHashCode() => Id;
//     }

//     public readonly struct ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6> : IEquatable<ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6>>
//         where TComponent : struct, IComponent
//         where TComponent1 : struct, IComponent
//         where TComponent2 : struct, IComponent
//         where TComponent3 : struct, IComponent
//         where TComponent4 : struct, IComponent
//         where TComponent5 : struct, IComponent
//         where TComponent6 : struct, IComponent
//     {
//         public static ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6> New() =>
//             new(HashCode.Combine(typeof(TComponent), typeof(TComponent1), typeof(TComponent2), typeof(TComponent3), typeof(TComponent4), typeof(TComponent5), typeof(TComponent6)));

//         public readonly int Id;
//         private readonly ComponentID _componentID;
//         private readonly ComponentID _componentID1;
//         private readonly ComponentID _componentID2;
//         private readonly ComponentID _componentID3;
//         private readonly ComponentID _componentID4;
//         private readonly ComponentID _componentID5;
//         private readonly ComponentID _componentID6;

//         private ComponentSet(int id)
//         {
//             Id = id;
//             _componentID = ComponentIdHelper.GetID<TComponent>();
//             _componentID1 = ComponentIdHelper.GetID<TComponent1>();
//             _componentID2 = ComponentIdHelper.GetID<TComponent2>();
//             _componentID3 = ComponentIdHelper.GetID<TComponent3>();
//             _componentID4 = ComponentIdHelper.GetID<TComponent4>();
//             _componentID5 = ComponentIdHelper.GetID<TComponent5>();
//             _componentID6 = ComponentIdHelper.GetID<TComponent6>();
//         }

//         public readonly bool HasComponent<T>()
//             where T : struct, IComponent
//             => ComponentIdHelper.GetID<T>() == _componentID
//                 || ComponentIdHelper.GetID<T>() == _componentID1
//                 || ComponentIdHelper.GetID<T>() == _componentID2
//                 || ComponentIdHelper.GetID<T>() == _componentID3
//                 || ComponentIdHelper.GetID<T>() == _componentID4
//                 || ComponentIdHelper.GetID<T>() == _componentID5
//                 || ComponentIdHelper.GetID<T>() == _componentID6;

//         public readonly bool HasComponent(ComponentID componentID)
//             => componentID == _componentID
//                 || componentID == _componentID1
//                 || componentID == _componentID2
//                 || componentID == _componentID3
//                 || componentID == _componentID4
//                 || componentID == _componentID5
//                 || componentID == _componentID6;

//         public bool Equals(ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6> other) => Id == other.Id;
//         public override bool Equals(object? obj) => obj is ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6> other && Equals(other);
//         public override int GetHashCode() => Id;
//     }

//     public readonly struct ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7> : IEquatable<ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7>>
//         where TComponent : struct, IComponent
//         where TComponent1 : struct, IComponent
//         where TComponent2 : struct, IComponent
//         where TComponent3 : struct, IComponent
//         where TComponent4 : struct, IComponent
//         where TComponent5 : struct, IComponent
//         where TComponent6 : struct, IComponent
//         where TComponent7 : struct, IComponent
//     {
//         public static ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7> New() =>
//             new(HashCode.Combine(typeof(TComponent), typeof(TComponent1), typeof(TComponent2), typeof(TComponent3), typeof(TComponent4), typeof(TComponent5), typeof(TComponent6), typeof(TComponent7)));

//         public readonly int Id;
//         private readonly ComponentID _componentID;
//         private readonly ComponentID _componentID1;
//         private readonly ComponentID _componentID2;
//         private readonly ComponentID _componentID3;
//         private readonly ComponentID _componentID4;
//         private readonly ComponentID _componentID5;
//         private readonly ComponentID _componentID6;
//         private readonly ComponentID _componentID7;

//         private ComponentSet(int id)
//         {
//             Id = id;
//             _componentID = ComponentIdHelper.GetID<TComponent>();
//             _componentID1 = ComponentIdHelper.GetID<TComponent1>();
//             _componentID2 = ComponentIdHelper.GetID<TComponent2>();
//             _componentID3 = ComponentIdHelper.GetID<TComponent3>();
//             _componentID4 = ComponentIdHelper.GetID<TComponent4>();
//             _componentID5 = ComponentIdHelper.GetID<TComponent5>();
//             _componentID6 = ComponentIdHelper.GetID<TComponent6>();
//             _componentID7 = ComponentIdHelper.GetID<TComponent7>();
//         }

//         public readonly bool HasComponent<T>()
//             where T : struct, IComponent
//             => ComponentIdHelper.GetID<T>() == _componentID
//                 || ComponentIdHelper.GetID<T>() == _componentID1
//                 || ComponentIdHelper.GetID<T>() == _componentID2
//                 || ComponentIdHelper.GetID<T>() == _componentID3
//                 || ComponentIdHelper.GetID<T>() == _componentID4
//                 || ComponentIdHelper.GetID<T>() == _componentID5
//                 || ComponentIdHelper.GetID<T>() == _componentID6
//                 || ComponentIdHelper.GetID<T>() == _componentID7;

//         public readonly bool HasComponent(ComponentID componentID)
//             => componentID == _componentID
//                 || componentID == _componentID1
//                 || componentID == _componentID2
//                 || componentID == _componentID3
//                 || componentID == _componentID4
//                 || componentID == _componentID5
//                 || componentID == _componentID6
//                 || componentID == _componentID7;

//         public bool Equals(ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7> other) => Id == other.Id;
//         public override bool Equals(object? obj) => obj is ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7> other && Equals(other);
//         public override int GetHashCode() => Id;
//     }

//     public readonly struct ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8> : IEquatable<ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8>>
//         where TComponent : struct, IComponent
//         where TComponent1 : struct, IComponent
//         where TComponent2 : struct, IComponent
//         where TComponent3 : struct, IComponent
//         where TComponent4 : struct, IComponent
//         where TComponent5 : struct, IComponent
//         where TComponent6 : struct, IComponent
//         where TComponent7 : struct, IComponent
//         where TComponent8 : struct, IComponent
//     {
//         public static ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8> New()
//         {
//             var hash = new HashCode();
//             hash.Add(typeof(TComponent));
//             hash.Add(typeof(TComponent1));
//             hash.Add(typeof(TComponent2));
//             hash.Add(typeof(TComponent3));
//             hash.Add(typeof(TComponent4));
//             hash.Add(typeof(TComponent5));
//             hash.Add(typeof(TComponent6));
//             hash.Add(typeof(TComponent7));
//             hash.Add(typeof(TComponent8));
//             return new ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8>(hash.ToHashCode());
//         }

//         public readonly int Id;
//         private readonly ComponentID _componentID;
//         private readonly ComponentID _componentID1;
//         private readonly ComponentID _componentID2;
//         private readonly ComponentID _componentID3;
//         private readonly ComponentID _componentID4;
//         private readonly ComponentID _componentID5;
//         private readonly ComponentID _componentID6;
//         private readonly ComponentID _componentID7;
//         private readonly ComponentID _componentID8;

//         private ComponentSet(int id)
//         {
//             Id = id;
//             _componentID = ComponentIdHelper.GetID<TComponent>();
//             _componentID1 = ComponentIdHelper.GetID<TComponent1>();
//             _componentID2 = ComponentIdHelper.GetID<TComponent2>();
//             _componentID3 = ComponentIdHelper.GetID<TComponent3>();
//             _componentID4 = ComponentIdHelper.GetID<TComponent4>();
//             _componentID5 = ComponentIdHelper.GetID<TComponent5>();
//             _componentID6 = ComponentIdHelper.GetID<TComponent6>();
//             _componentID7 = ComponentIdHelper.GetID<TComponent7>();
//             _componentID8 = ComponentIdHelper.GetID<TComponent8>();
//         }

//         public readonly bool HasComponent<T>()
//             where T : struct, IComponent
//             => ComponentIdHelper.GetID<T>() == _componentID
//                 || ComponentIdHelper.GetID<T>() == _componentID1
//                 || ComponentIdHelper.GetID<T>() == _componentID2
//                 || ComponentIdHelper.GetID<T>() == _componentID3
//                 || ComponentIdHelper.GetID<T>() == _componentID4
//                 || ComponentIdHelper.GetID<T>() == _componentID5
//                 || ComponentIdHelper.GetID<T>() == _componentID6
//                 || ComponentIdHelper.GetID<T>() == _componentID7
//                 || ComponentIdHelper.GetID<T>() == _componentID8;

//         public readonly bool HasComponent(ComponentID componentID)
//             => componentID == _componentID
//                 || componentID == _componentID1
//                 || componentID == _componentID2
//                 || componentID == _componentID3
//                 || componentID == _componentID4
//                 || componentID == _componentID5
//                 || componentID == _componentID6
//                 || componentID == _componentID7
//                 || componentID == _componentID8;

//         public bool Equals(ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8> other) => Id == other.Id;
//         public override bool Equals(object? obj) => obj is ComponentSet<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8> other && Equals(other);
//         public override int GetHashCode() => Id;
//     }

// }
