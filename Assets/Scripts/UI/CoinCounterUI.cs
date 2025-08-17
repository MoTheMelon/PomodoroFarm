using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinCounterUI : MonoBehaviour {
    TextMeshProUGUI label;
    int lastShown = int.MinValue;

    void Awake() {
        label = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        int coins = CurrencyService.I != null ? CurrencyService.I.Coins : 0;
        if (coins != lastShown) {
            label.text = $"Coins: {coins}";
            lastShown = coins;
        }
    }
}