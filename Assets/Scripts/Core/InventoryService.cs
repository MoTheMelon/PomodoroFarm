using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class InventoryService : MonoBehaviour {
    public static InventoryService I { get; private set; }
    public System.Action OnChanged;

    Dictionary<string, int> counts = new();

    void Awake() {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    public int Get(string id) => counts.TryGetValue(id, out var v) ? v : 0;

    public void Add(string id, int amount) {
        if (amount <= 0) return;
        counts[id] = Get(id) + amount;
        OnChanged?.Invoke();
    }

    public bool Spend(string id, int amount) {
        int have = Get(id);
        if (amount <= 0 || have < amount) return false;
        counts[id] = have - amount;
        OnChanged?.Invoke();
        return true;
    }
}