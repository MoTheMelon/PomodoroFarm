using UnityEngine;

public class CurrencyService : MonoBehaviour {
    public static CurrencyService I;
    public int Coins { get; private set; }

    void Awake() {
        I = this;
    }

    public void AddCoins(int amount) {
        Coins += amount;
    }
}