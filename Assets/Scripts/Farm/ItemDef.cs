using UnityEngine;

public enum ItemType { CropSeed /*, Tool, etc.*/ }

[CreateAssetMenu(menuName="Farm/ItemDef")]
public class ItemDef : ScriptableObject {
    public string id;           // e.g., "wheat_seed"
    public ItemType type = ItemType.CropSeed;
    public Sprite icon;         // toolbar button icon
    public int cost = 1;        // coins per placement
    public string cropId;       // for seeds: CropDef.id
}