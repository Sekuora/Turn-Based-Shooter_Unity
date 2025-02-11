// Copyright(c) 2025 Fyragic. All rights reserved.
public struct GridPosition
{
   
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public override string ToString()
    {
        return x + ":" + z;
    }
}