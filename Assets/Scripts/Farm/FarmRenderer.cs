using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmRenderer : MonoBehaviour {
    public Tilemap tilemap;
    public List<CropDef> cropDefs;

    FarmWorld world; FarmSim sim;
    Dictionary<string, CropDef> crops;
    readonly Dictionary<Sprite, Tile> tileCache = new();

    void Awake() {
        world = new FarmWorld();
        crops = new();
        foreach (var d in cropDefs) crops[d.id]=d;
        sim = new FarmSim(world, cropDefs);
    }

    void Update() {
        sim.Tick(Time.deltaTime);
        if (world.dirty.Count == 0) return;
        foreach (var c in world.dirty) ApplyCell(c);
        world.dirty.Clear();
    }

    public (FarmWorld, FarmSim) Get() => (world, sim);

    void ApplyCell(Cell c) {
        var pos = new Vector3Int(c.x, c.y, 0);
        if (!world.plots.TryGetValue(c, out var p) || p.IsEmpty) { tilemap.SetTile(pos, null); return; }
        var def = crops[p.cropId];
        var sprite = def.growthFrames[Mathf.Clamp(p.stage, 0, def.growthFrames.Length-1)];
        tilemap.SetTile(pos, ToTile(sprite));
    }

    Tile ToTile(Sprite s) {
        if (!tileCache.TryGetValue(s, out var t)) {
            t = ScriptableObject.CreateInstance<Tile>();
            t.sprite = s;
            tileCache[s] = t;
        }
        return t;
    }
}