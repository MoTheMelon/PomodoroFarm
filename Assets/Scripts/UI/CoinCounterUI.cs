using TMPro;
using UnityEngine;

public class CoinCounterUI : MonoBehaviour {
    public TextMeshProUGUI label;
    void OnEnable() {
        CurrencyService.I.OnChanged += Refresh;
        Refresh(CurrencyService.I.Coins);
    }
    void OnDisable() {
        if (CurrencyService.I != null) CurrencyService.I.OnChanged -= Refresh;
    }
    void Refresh(int coins) {
        label.text = $"Coins: {coins}";
    }
}