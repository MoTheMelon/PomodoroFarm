using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolbarButton : MonoBehaviour {
    public ItemDef item;
    public Image icon;
    public TextMeshProUGUI costLabel;
    public Button button;
    public CanvasGroup group;

    void Awake() {
        if (icon) icon.sprite = item.icon;
        if (costLabel) costLabel.text = item.cost.ToString();
    }

    void OnEnable() {
        CurrencyService.I.OnChanged += Refresh;
        Refresh(CurrencyService.I.Coins);
        button.onClick.AddListener(OnClick);
    }

    void OnDisable() {
        if (CurrencyService.I != null) CurrencyService.I.OnChanged -= Refresh;
        button.onClick.RemoveListener(OnClick);
    }

    void OnClick() {
        if (!CurrencyService.I.CanAfford(item.cost)) return;
        SelectionService.I.Select(item);
    }

    void Refresh(int _) {
        bool afford = CurrencyService.I.CanAfford(item.cost);
        button.interactable = afford;
        if (group) group.alpha = afford ? 1f : 0.5f;
    }
}