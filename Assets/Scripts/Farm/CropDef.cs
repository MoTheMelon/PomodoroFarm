using UnityEngine;

[CreateAssetMenu(menuName = "Farm/CropDef")]
public class CropDef : ScriptableObject
{
    public string id;                 // e.g., "wheat"
    public Sprite[] growthFrames;     // stage0..N-1 (last = mature)
    public float[] stageDurations;    // seconds for each transition
    public int harvestYield = 1;
    public bool regrowAfterHarvest = false;
    public int price;
}