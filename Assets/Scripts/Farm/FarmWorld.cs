using System;
using System.Collections.Generic;

public struct Cell {
    public int x, y;
    public Cell(int x,int y){ this.x=x; this.y=y; }
}

[Serializable]
public class Plot {
    public string cropId;   // empty = no crop
    public int stage;
    public float stageTime;
    public bool IsEmpty => string.IsNullOrEmpty(cropId);
}

public class FarmWorld {
    public readonly Dictionary<Cell, Plot> plots = new(new CellComparer());
    public readonly HashSet<Cell> dirty = new();

    public Plot GetOrCreate(Cell c) {
        if (!plots.TryGetValue(c, out var p)) plots[c] = p = new Plot();
        return p;
    }
    public void MarkDirty(Cell c) => dirty.Add(c);

    class CellComparer : IEqualityComparer<Cell> {
        public bool Equals(Cell a, Cell b) => a.x==b.x && a.y==b.y;
        public int GetHashCode(Cell c) => unchecked((c.x*73856093)^(c.y*19349663));
    }
}