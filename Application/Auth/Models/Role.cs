using HotelAutomationApp.Shared.Common.Abstractions;

namespace HotelAutomationApp.Application.Auth.Models;

public abstract class Role : Enumeration<Role>, IEquatable<Role>, IComparable<Role>
{
    public Role(int key, string name, int accessLevel) : base(key, name)
    {
        AccessLevel = accessLevel;
    }

    public int AccessLevel { get; }

    public static Role User { get; } = new UserRole();

    public static Role Admin { get; } = new AdminRole();

    public static Role Root { get; } = new RootRole();

    public static Role Guest { get; } = new GuestRole();

    public bool IsGuest => Name == Guest.Name;

    public bool Equals(Role? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return AccessLevel == other.AccessLevel;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Role) obj);
    }

    public override int GetHashCode()
    {
        return AccessLevel;
    }

    public int CompareTo(Role? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return -1;
        return AccessLevel.CompareTo(other.AccessLevel);
    }

    public override string ToString() => Name;

    public static bool operator <(Role left, Role right)
    {
        return left.AccessLevel > right.AccessLevel;
    }

    public static bool operator >(Role left, Role right)
    {
        return left.AccessLevel < right.AccessLevel;
    }

    public static bool operator ==(Role left, Role right)
    {
        return left.AccessLevel == right.AccessLevel;
    }

    public static bool operator !=(Role left, Role right)
    {
        return left.AccessLevel != right.AccessLevel;
    }
}

public class RootRole : Role
{
    public RootRole() : base(1, "Root", 1)
    {
    }
}

public class AdminRole : Role
{
    public AdminRole() : base(2, "Admin", 2)
    {
    }
}

public class UserRole : Role
{
    public UserRole() : base(3, "User", 3)
    {
    }
}

public class GuestRole : Role
{
    public GuestRole() : base(4, "Guest", 3)
    {
    }
}