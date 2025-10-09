namespace Infrastructure.Persistence;

//StronglyTypedIdInterfaces.cs
// public interface IStronglyTypedId<T> where T : IEquatable<T>
// {
//     T Value { get; }
// }
//
// public interface IGuidStronglyTypedId : IStronglyTypedId<Guid>
// {
// }
//
// // Add a marker interface to help with reflection
// public interface IStronglyTypedId
// {
// }

// GenericConverter.cs
// using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
// using System.Linq.Expressions;
//
// public class StronglyTypedIdConverter<TStronglyTypedId, TValue> : ValueConverter<TStronglyTypedId, TValue>
//     where TStronglyTypedId : IStronglyTypedId<TValue>
//     where TValue : IEquatable<TValue>
// {
//     public StronglyTypedIdConverter(ConverterMappingHints? mappingHints = null)
//         : base(
//             CreateToProviderExpression(),
//             CreateFromProviderExpression(),
//             mappingHints)
//     {
//     }
//
//     private static Expression<Func<TStronglyTypedId, TValue>> CreateToProviderExpression()
//     {
//         var parameter = Expression.Parameter(typeof(TStronglyTypedId), "value");
//         var property = Expression.Property(parameter, nameof(IStronglyTypedId<TValue>.Value));
//         return Expression.Lambda<Func<TStronglyTypedId, TValue>>(property, parameter);
//     }
//
//     private static Expression<Func<TValue, TStronglyTypedId>> CreateFromProviderExpression()
//     {
//         var parameter = Expression.Parameter(typeof(TValue), "value");
//         var constructor = typeof(TStronglyTypedId).GetConstructor(new[] { typeof(TValue) });
//         
//         if (constructor != null)
//         {
//             var newExpression = Expression.New(constructor, parameter);
//             return Expression.Lambda<Func<TValue, TStronglyTypedId>>(newExpression, parameter);
//         }
//
//         // Fallback to explicit operator if constructor is not available
//         var explicitOperator = typeof(TStronglyTypedId)
//             .GetMethods(BindingFlags.Public | BindingFlags.Static)
//             .FirstOrDefault(m => m.Name == "op_Explicit" && 
//                            m.ReturnType == typeof(TStronglyTypedId) && 
//                            m.GetParameters().Length == 1 && 
//                            m.GetParameters()[0].ParameterType == typeof(TValue));
//
//         if (explicitOperator != null)
//         {
//             var callExpression = Expression.Call(explicitOperator, parameter);
//             return Expression.Lambda<Func<TValue, TStronglyTypedId>>(callExpression, parameter);
//         }
//
//         throw new InvalidOperationException(
//             $"No suitable constructor or explicit operator found for {typeof(TStronglyTypedId).Name}");
//     }
// }

//GenericValueGenerator.cs
// using Microsoft.EntityFrameworkCore.ChangeTracking;
// using Microsoft.EntityFrameworkCore.ValueGeneration;
// using System.Reflection;
//
// public class StronglyTypedIdValueGenerator<TStronglyTypedId> : ValueGenerator<TStronglyTypedId>
//     where TStronglyTypedId : IGuidStronglyTypedId
// {
//     private static readonly SequentialGuidValueGenerator _generator = new();
//     private static readonly Func<Guid, TStronglyTypedId> _factory = CreateFactory();
//
//     public override bool GeneratesTemporaryValues => false;
//
//     public override TStronglyTypedId Next(EntityEntry entry)
//     {
//         return _factory(_generator.Next(default!));
//     }
//
//     private static Func<Guid, TStronglyTypedId> CreateFactory()
//     {
//         var type = typeof(TStronglyTypedId);
//         
//         // Try constructor first
//         var constructor = type.GetConstructor(new[] { typeof(Guid) });
//         if (constructor != null)
//         {
//             return guid => (TStronglyTypedId)constructor.Invoke(new object[] { guid });
//         }
//
//         // Try explicit operator
//         var explicitOperator = type
//             .GetMethods(BindingFlags.Public | BindingFlags.Static)
//             .FirstOrDefault(m => m.Name == "op_Explicit" && 
//                                  m.ReturnType == type && 
//                                  m.GetParameters().Length == 1 && 
//                                  m.GetParameters()[0].ParameterType == typeof(Guid));
//
//         if (explicitOperator != null)
//         {
//             return guid => (TStronglyTypedId)explicitOperator.Invoke(null, new object[] { guid });
//         }
//
//         throw new InvalidOperationException($"No suitable factory method found for {type.Name}");
//     }
// }

//StronglyTypedIdExtension.cs
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using System.Reflection;
//
// public static class StronglyTypedIdConfigurationExtensions
// {
//     public static void ConfigureStronglyTypedIds(this ModelBuilder modelBuilder, Assembly assembly)
//     {
//         var stronglyTypedIdTypes = assembly.GetTypes()
//             .Where(t => t.GetInterfaces().Any(i => 
//                 i.IsGenericType && 
//                 i.GetGenericTypeDefinition() == typeof(IStronglyTypedId<>)))
//             .ToList();
//
//         foreach (var idType in stronglyTypedIdTypes)
//         {
//             ConfigureStronglyTypedId(modelBuilder, idType);
//         }
//     }
//
//     private static void ConfigureStronglyTypedId(ModelBuilder modelBuilder, Type stronglyTypedIdType)
//     {
//         var valueType = GetValueType(stronglyTypedIdType);
//         if (valueType == null) return;
//
//         // Create converter type
//         var converterType = typeof(StronglyTypedIdConverter<,>).MakeGenericType(stronglyTypedIdType, valueType);
//         var converter = Activator.CreateInstance(converterType);
//
//         // Configure all properties of this type across all entities
//         foreach (var entityType in modelBuilder.Model.GetEntityTypes())
//         {
//             var properties = entityType.ClrType.GetProperties()
//                 .Where(p => p.PropertyType == stronglyTypedIdType);
//
//             foreach (var property in properties)
//             {
//                 modelBuilder.Entity(entityType.ClrType)
//                     .Property(property.PropertyType, property.Name)
//                     .HasConversion(converter as ValueConverter);
//
//                 // Add value generator for Guid-based IDs
//                 if (valueType == typeof(Guid) && 
//                     stronglyTypedIdType.GetInterfaces().Contains(typeof(IGuidStronglyTypedId)))
//                 {
//                     var valueGeneratorType = typeof(StronglyTypedIdValueGenerator<>)
//                         .MakeGenericType(stronglyTypedIdType);
//                     
//                     modelBuilder.Entity(entityType.ClrType)
//                         .Property(property.PropertyType, property.Name)
//                         .HasValueGenerator(valueGeneratorType);
//                 }
//             }
//         }
//     }
//
//     private static Type? GetValueType(Type stronglyTypedIdType)
//     {
//         var interfaces = stronglyTypedIdType.GetInterfaces();
//         var stronglyTypedInterface = interfaces
//             .FirstOrDefault(i => i.IsGenericType && 
//                            i.GetGenericTypeDefinition() == typeof(IStronglyTypedId<>));
//
//         return stronglyTypedInterface?.GetGenericArguments().FirstOrDefault();
//     }
// }

// StronglyTypedIdAttribute.cs - Alternative: Attribute-Based Approach
// [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
// public class StronglyTypedIdAttribute : Attribute
// {
//     public Type ValueType { get; }
//     public bool GenerateValue { get; set; } = true;
//
//     public StronglyTypedIdAttribute(Type valueType)
//     {
//         ValueType = valueType;
//     }
// }
//AttributeBasedConfiguration.cs
// public static class AttributeBasedConfigurationExtensions
// {
//     public static void ConfigureStronglyTypedIdsWithAttributes(this ModelBuilder modelBuilder, Assembly assembly)
//     {
//         var stronglyTypedIdTypes = assembly.GetTypes()
//             .Where(t => t.GetCustomAttribute<StronglyTypedIdAttribute>() != null)
//             .ToList();
//
//         foreach (var idType in stronglyTypedIdTypes)
//         {
//             var attribute = idType.GetCustomAttribute<StronglyTypedIdAttribute>()!;
//             ConfigureStronglyTypedIdWithAttribute(modelBuilder, idType, attribute);
//         }
//     }
//
//     private static void ConfigureStronglyTypedIdWithAttribute(
//         ModelBuilder modelBuilder, 
//         Type stronglyTypedIdType, 
//         StronglyTypedIdAttribute attribute)
//     {
//         // Similar implementation as above but using attribute information
//         var converterType = typeof(StronglyTypedIdConverter<,>)
//             .MakeGenericType(stronglyTypedIdType, attribute.ValueType);
//         var converter = Activator.CreateInstance(converterType);
//
//         foreach (var entityType in modelBuilder.Model.GetEntityTypes())
//         {
//             var properties = entityType.ClrType.GetProperties()
//                 .Where(p => p.PropertyType == stronglyTypedIdType);
//
//             foreach (var property in properties)
//             {
//                 var propertyBuilder = modelBuilder.Entity(entityType.ClrType)
//                     .Property(property.PropertyType, property.Name)
//                     .HasConversion(converter as ValueConverter);
//
//                 if (attribute.GenerateValue && attribute.ValueType == typeof(Guid))
//                 {
//                     var valueGeneratorType = typeof(StronglyTypedIdValueGenerator<>)
//                         .MakeGenericType(stronglyTypedIdType);
//                     propertyBuilder.HasValueGenerator(valueGeneratorType);
//                 }
//             }
//         }
//     }
// }
//
// [StronglyTypedId(typeof(Guid))]
// public readonly record struct MaterialId(Guid Value) : IGuidStronglyTypedId, IStronglyTypedId
// {
//     public static explicit operator MaterialId(Guid value) => new(value);
// }

//Use in DbContext
// public class YourDbContext : DbContext
// {
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         // Option 1: Automatic discovery by interface
//         modelBuilder.ConfigureStronglyTypedIds(typeof(MaterialId).Assembly);
//
//         // Option 2: Attribute-based (if using attributes)
//         // modelBuilder.ConfigureStronglyTypedIdsWithAttributes(typeof(MaterialId).Assembly);
//
//         base.OnModelCreating(modelBuilder);
//     }
// }