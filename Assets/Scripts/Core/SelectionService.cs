using UnityEngine;

[DefaultExecutionOrder(-100)]
public class SelectionService : MonoBehaviour {
    public static SelectionService I { get; private set; }
    public System.Action<ItemDef> OnSelectionChanged;

    public ItemDef Current { get; private set; }

    void Awake() {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Select(ItemDef item) {
        Current = item;
        OnSelectionChanged?.Invoke(item);
    }

    public void Clear() => Select(null);
}