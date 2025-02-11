// Copyright(c) 2025 Fyragic. All rights reserved.

// Defines struct to act as Vector Position for x, z grid cell positions.
using System;

public struct GridPosition : IEquatable<GridPosition>
{
    // Data   
    public int x;
    public int z;

    // Constructor
    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    // Overrides
    // ToString Override
    public override string ToString()
    {
        return x + ":" + z;
    }

    // == Operator
    public static bool operator ==(GridPosition a, GridPosition b)
    {
        // If the compared values are equal returns true
        return a.x == b.x && a.z == b.z;
    }

    // != Operator
    public static bool operator !=(GridPosition a, GridPosition b)
    {
        return !(a == b);
    }

    // Equals
    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
               x == position.x &&
               z == position.z;
    }

    // GetHashCode
    public override int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }

    // Interface IEquatable override
    public bool Equals(GridPosition other)
    {
        return this == other;
    }
}