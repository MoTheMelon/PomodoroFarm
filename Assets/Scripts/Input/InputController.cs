using UnityEngine;
using UnityEngine.Tilemaps;

public class InputController : MonoBehaviour {
    public Camera cam;
    public Tilemap tilemap;
    public FarmRenderer farm;
    public string currentCropId = "wheat";

    FarmSim sim;

    void Start() { sim = farm.Get().Item2; }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var cell = MouseCell();
            sim.Plant(cell, currentCropId);
        }
    }

    Cell MouseCell() {
        var worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int v = tilemap.WorldToCell(worldPos);
        return new Cell(v.x, v.y);
    }
}