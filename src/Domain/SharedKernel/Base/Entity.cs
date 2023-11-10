using Domain.Extensions;

namespace Domain.SharedKernel.Base;

public abstract class Entity<TId> where TId : struct
{
    private int? _cachedHashCode;
    public TId Id { get; protected set; }

    protected Entity()
    {
    }

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
            return false;

        return Equals(other);
    }

    public bool Equals(Entity<TId>? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (this.GetUnproxiedType() != other.GetUnproxiedType())
            return false;

        if (IsTransient() || other.IsTransient())
            return false;

        return Id.Equals(other.Id);
    }

    public bool IsTransient()
    {
        return Id.Equals(default(TId));
    }

    public static bool operator ==(Entity<TId>? a, Entity<TId>? b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity<TId>? a, Entity<TId>? b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        /*
         * https://stackoverflow.com/questions/371328/why-is-it-important-to-override-gethashcode-when-equals-method-is-overridden?fbclid=IwAR2yv2PwQ_tfVlPPTYnLV8EWSaaQkXOvKsls04NOVm0gkL_xxC78-LGTm28
         * https://stackoverflow.com/questions/638761/gethashcode-override-of-object-containing-generic-array/639098#639098
         * https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-overriding-gethashcode#263416
         * GetHashCode is important if your item will be used as a key in a dictionary, or HashSet<T>, HashSet,
         *  etc - since this is used (in the absence of a custom IEqualityComparer<T>) to group items 
         *  into buckets. If the hash-code for two items does not match, they may never be considered 
         *  equal (Equals will simply never be called).
         * The GetHashCode() method should reflect the Equals logic; the rules are:
         *   - if two things are equal (Equals(...) == true) then they must return the same value for GetHashCode()
         *   - if the GetHashCode() is equal, it is not necessary for them to be the same; this is a collision, 
         *   and Equals will be called to see if it is a real equality or not.
         *   
         * The algorithm that calculates a hash code needs to be fast. A simple algorithm is usually going to be 
         *  a faster one. One that does not allocate extra memory will also reduce need for garbage collection, 
         *  which will in turn also improve performance.
         * In C# hash functions specifically, you often use the unchecked keyword which stops overflow checking 
         *  to improve performance.
         *  
         * In most cases where Equals() compares multiple fields it doesn't really matter if your GetHash() hashes 
         *  on one field or on many. You just have to make sure that calculating the hash is really cheap 
         *  (No allocations, please) and fast (No heavy computations and certainly no database connections) and 
         *  provides a good distribution.
         * The heavy lifting should be part of the Equals() method; the hash should be a very cheap operation to 
         *  enable calling Equals() on as few items as possible.
         * And one final tip: Don't rely on GetHashCode() being stable over multiple aplication runs. Many .Net 
         *  types don't guarantee their hash codes to stay the same after a restart, so you should only use the 
         *  value of GetHashCode() for in memory data structures.
         *  
         * Hash code is used for hash-based collections like Dictionary, Hashtable, HashSet etc. The purpose of 
         *  this code is to very quickly pre-sort specific object by putting it into specific group (bucket). This 
         *  pre-sorting helps tremendously in finding this object when you need to retrieve it back from 
         *  hash-collection because code has to search for your object in just one bucket instead of in all objects 
         *  it contains. The better distribution of hash codes (better uniqueness) the faster retrieval. In ideal 
         *  situation where each object has a unique hash code, finding it is an O(1) operation. In most cases it 
         *  approaches O(1).
         *  
         * Should not use Guid.GetHashCode because it may lead do duplication hash code
         *  - https://stackoverflow.com/questions/401480/converting-guid-to-integer-and-back
         *  - https://stackoverflow.com/questions/7326593/guid-gethashcode-uniqueness
         * But from Chat GPT, the case where two different Guids can have the same hash code is astronomically rare.
         *  - If you have two different Guid instances, the chances of their hash codes being the same are
         *  astronomically small. The hash algorithm used by Guid is designed to minimize collisions, but due to the
         *  limited number of possible hash codes (2^32), it is theoretically possible for two different Guid instances
         *  to produce the same hash code. However, the probability of this happening is so low that it is considered
         *  negligible in practice.
         *
         * A hash code is a numeric value that is used to insert and identify an object in a hash-based collection such
         *  as the Dictionary<TKey,TValue> class, the Hashtable class, or a type derived from the DictionaryBase class.
         *  The GetHashCode method provides this hash code for algorithms that need quick checks of object equality.
         *  - https://learn.microsoft.com/en-us/dotnet/api/system.object.gethashcode?view=net-5.0#remarks
         *
         * Analysis from Andrew Lock
         * https://andrewlock.net/why-is-string-gethashcode-different-each-time-i-run-my-program-in-net-core/
         *
         * What kind of members should be used in a GetHashCode() implementation
         *  https://softwareengineering.stackexchange.com/questions/317576/what-kind-of-members-should-be-used-in-a-gethashcode-implementation
         *
         * checked and unchecked
         * https://stackoverflow.com/questions/25778554/unchecked-keyword-in-c-sharp
         * 
         * From Feng Yuan, who is Performance Architect at Microsoft
         * https://www.linkedin.com/posts/dryuan_dotnet-performance-azure-activity-7082023333336125440-Wwp4/?utm_source=share&utm_medium=member_ios&fbclid=IwAR2gtc_LDz5l5kKXZmsYu4de3-RxH8ZD0C5Y0E85CCv2TM8rj4TnXnqXh5o
         * Guidelines for GetHashCode/Equals implementation
         * 1. If a type is used as hash container key type, either IEquatable<T> or IEqualityComparer<T> should be 
         *  provided.
         * 2. There should be no allocation in GetHashCode/Equals implementations, except in cached implementation 
         *  of GetHashCode.
         * 3. GetHashCode and Equals should be consistent. If Equals(x, y), then x/y should have the same hash code.
         * 4. GetHashCode and Equals should use the same number of fields. If GetHashCode is using more, then 
         *  rule #3 could be brown, if Equals is using more, then there could be lots of hash duplications.
         * 5. GetHashCode/Equals should not be dependent on current thread culture.
         * 6. Uncommon implementations should be clearly documented.
         * 7. If an object is already stored in a hash container as key, any modification which may affect its hash 
         *  code is not allowed.
         * 8. If hash code is cached, reset/recalculate after modification.
         * 9. Avoid using operators like + or ^ for combining hash codes, prefer bit rotation as in 
         *  Hashing.HashHelpers.Combine.
         * 10. To generate hash code for elements in unordered collection, using symmetric/commutative operators 
         *  (+,*,^) to avoid sorting.
         * 11. Only lower 31 bits of hash codes are used in .Net. Make the best usage of these bits to generate as 
         *  unique/well distributed hash codes as possible.
         * 12. Equals method must be commutative. Equals(x,y) iff Equals(y, x)
         * 13. For classes, add object.ReferenceEquals(x, other) check first.
         * 14. Consider override Equals(object?), avoid double casting.
         * 15. Compare cheaper fields first.
         * 16. If hash codes are cached, compare hash code first, especially for ConcurrentDictionary keys.
         * 17. Avoid using Tuple<T1, T2> or similar classes as key type, without proper IEqualityComparer<T>. Their 
         *  default implementations are not good enough.
         * 18. Avoid heap allocation for key generation for lookup, for example composing string key for MemoryCache lookup.
         * 19. For case-insensitive string key, StringComparer.OrdinalIgnoreCase is normally good enough.
         * 20. String.GetHashCode may not be consistent across different runtimes or versions. If consistency is needed, 
         *  you need your own implementation.
         * 21. GetHashCode/Equals should use the same string comparison rule.
         */
        
        if (_cachedHashCode.HasValue)
            return _cachedHashCode.Value;

        _cachedHashCode = (this.GetUnproxiedType().Name.GetHashCode() + Id.GetHashCode()).GetHashCode();

        return _cachedHashCode.Value;
    }
}


//https://github.com/dotnet/efcore/blob/main/src/EFCore/ValueGeneration/SequentialGuidValueGenerator.cs
//https://davecallan.com/generating-sequential-guids-which-sort-sql-server-in-net/

//using mac address for sequential guid will not create if you use fake address: https://devblogs.microsoft.com/oldnewthing/20191120-00/?p=103118
//using mac address for sequential guid might result in fragmentation and performance issue if you have many virtual machine
// that contain your application (multi instances) access the same db server.

/*
 * https://dev.to/connerphillis/sequential-guids-in-entity-framework-core-might-not-be-sequential-3408
 * setting a column to use a GUID as a key with DatabaseGeneratedOption.Identity does not mean that it 
 *  will be generated by the database. Instead, EF Core generates the sequential GUID itself, and then 
 *  inserts it into the database
 */
//https://learn.microsoft.com/en-us/ef/core/modeling/generated-properties?tabs=fluent-api
/*
 * For example, on SQL Server, when a GUID property is configured as a primary key, 
 *  the provider automatically performs value generation client-side, using an algorithm 
 *  to generate optimal sequential GUID values.
 */
// From that links, The sequential Guid using SequentialGuidValueGenerator() as a default and generate
// at client side, not database side. So you don't do anything because SequentialGuidValueGenerator() is
// configured as a default of behaviour. You can test it when calling Attach or Add method from DbContext.

public abstract class Entity : Entity<Guid>
{
    protected Entity()
    {
    }

    //If your application use Guid.NewGuid() and set it here, it would be a problem. Because now your application
    //use guid instead of Sequential Guid
    protected Entity(Guid id)
        : base(id)
    {
    }
}