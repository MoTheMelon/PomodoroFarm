using UnityEngine;

[DefaultExecutionOrder(-100)]
public class CurrencyService : MonoBehaviour {
    public static CurrencyService I { get; private set; }
    public System.Action<int> OnChanged;

    [SerializeField] int startingCoins = 50;
    public int Coins { get; private set; }

    void Awake(){
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
        Coins = startingCoins;
    }

    public bool CanAfford(int cost) => Coins >= cost;

    public bool Spend(int cost) {
        if (!CanAfford(cost)) return false;
        Coins -= cost;
        OnChanged?.Invoke(Coins);
        return true;
    }

    public void Add(int amount) {
        Coins += amount;
        OnChanged?.Invoke(Coins);
    }
}