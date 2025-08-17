using System.Collections.Generic;
using UnityEngine;

public class FarmSim {
    readonly FarmWorld world;
    readonly Dictionary<string, CropDef> crops;

    public bool GrowthEnabled { get; set; } = true;

    public FarmSim(FarmWorld w, IEnumerable<CropDef> defs) {
        world = w; crops = new();
        foreach (var d in defs) if (!string.IsNullOrEmpty(d.id)) crops[d.id]=d;
    }

    public void Tick(float dt) {
        if (!GrowthEnabled) return;
        foreach (var kv in world.plots) {
            var c = kv.Key; var p = kv.Value;
            if (p.IsEmpty) continue;
            var def = crops[p.cropId];
            int last = def.growthFrames.Length - 1;
            if (p.stage >= last) continue; // mature
            p.stageTime += dt;
            int idx = Mathf.Clamp(p.stage, 0, def.stageDurations.Length-1);
            if (p.stageTime >= def.stageDurations[idx]) {
                p.stageTime = 0f;
                p.stage++;
                world.MarkDirty(c);
            }
        }
    }

    public bool Plant(Cell c, string cropId) {
        var p = world.GetOrCreate(c);
        if (!p.IsEmpty) return false;
        p.cropId = cropId; p.stage = 0; p.stageTime = 0f;
        world.MarkDirty(c);
        return true;
    }
}