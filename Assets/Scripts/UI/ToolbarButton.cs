using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(CanvasGroup))]
public class ToolbarButton : MonoBehaviour {
    [Header("Data")]
    public ItemDef item;                 // e.g., WheatSeed.asset

    [Header("UI Refs")]
    public Image icon;                   // Image on child "Icon"
    public TextMeshProUGUI costLabel;    // TMP on child "CostLabel"

    Button button;
    CanvasGroup group;

    void Awake() {
        button = GetComponent<Button>();
        group  = GetComponent<CanvasGroup>();

        // Populate visuals from ItemDef
        if (icon)      icon.sprite   = item ? item.icon : null;
        if (costLabel) costLabel.text = item ? item.cost.ToString() : "";

        // Click selects the item (we still re-check coins on click)
        button.onClick.AddListener(OnClick);
    }

    void OnDestroy() {
        button.onClick.RemoveListener(OnClick);
    }

    void Update() {
        // Poll coins each frame (simple & robust)
        int coins = CurrencyService.I != null ? CurrencyService.I.Coins : int.MaxValue;
        int cost  = item != null ? item.cost : 0;

        bool afford = coins >= cost;

        // Grey out and disable when unaffordable
        if (group)  group.alpha = afford ? 1f : 0.5f;
        if (button) button.interactable = afford;
    }

    void OnClick() {
        // Only select if affordable right now
        if (CurrencyService.I == null || item == null) return;
        if (CurrencyService.I.Coins < item.cost) return;

        SelectionService.I?.Select(item);
    }
}