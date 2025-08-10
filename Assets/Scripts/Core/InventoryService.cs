using System.Collections.Generic;
using UnityEngine;

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
}