using TMPro;
using UnityEngine;

public class CounterUI : MonoBehaviour {
    public TextMeshProUGUI label;
    bool bound;

    void OnEnable()   { TryBind(); if (bound) Refresh(); }
    void Update()     { if (!bound) TryBind(); }  // bind as soon as Inventory exists
    void OnDisable()  { if (bound && InventoryService.I != null) InventoryService.I.OnChanged -= Refresh; bound = false; }

    void TryBind() {
        var inv = InventoryService.I;
        if (inv == null) return;
        inv.OnChanged += Refresh;
        bound = true;
    }

    void Refresh() {
        int n = InventoryService.I?.Get("wheat") ?? 0;
        label.text = $"Wheat: {n}";
    }
}