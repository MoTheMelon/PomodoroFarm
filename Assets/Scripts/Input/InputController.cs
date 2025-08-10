using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputController : MonoBehaviour {
    public Camera cam;
    public Tilemap tilemap;
    public FarmRenderer farm;

    FarmSim sim;

    // drag states
    bool planting, harvesting;
    HashSet<Cell> visitedPlant = new();
    HashSet<Cell> visitedHarvest = new();

    void Start() { sim = farm.Get().Item2; }

    void Update() {
        // Begin / end planting drag (Left mouse)
        if (Input.GetMouseButtonDown(0)) { planting = true;  visitedPlant.Clear(); }
        if (Input.GetMouseButtonUp(0))   { planting = false; }

        // Begin / end harvest drag (Right mouse)
        if (Input.GetMouseButtonDown(1)) { harvesting = true; visitedHarvest.Clear(); }
        if (Input.GetMouseButtonUp(1))   { harvesting = false; }

        var cell = MouseCell();

        // Plant while dragging (cost per placement)
        if (planting) TryPlant(cell);

        // Harvest while dragging
        if (harvesting) TryHarvest(cell);
    }

    void TryPlant(Cell cell) {
        if (visitedPlant.Contains(cell)) return;
        visitedPlant.Add(cell);

        var sel = SelectionService.I.Current;
        if (sel == null || sel.type != ItemType.CropSeed) return;

        // Not enough money? auto-deselect and bail
        if (!CurrencyService.I.CanAfford(sel.cost)) {
            SelectionService.I.Clear();
            return;
        }

        // Attempt to plant. If the plot is occupied, we don't charge.
        bool planted = sim.Plant(cell, sel.cropId);
        if (planted) {
            // Spend coins; if this made us broke, auto-deselect so painting stops
            CurrencyService.I.Spend(sel.cost);
            if (!CurrencyService.I.CanAfford(sel.cost)) SelectionService.I.Clear();
        }
    }

    void TryHarvest(Cell cell) {
        if (visitedHarvest.Contains(cell)) return;
        visitedHarvest.Add(cell);

        int got = sim.Harvest(cell);
        if (got > 0) InventoryService.I?.Add("wheat", got);
    }

    Cell MouseCell() {
        var worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int v = tilemap.WorldToCell(worldPos);
        return new Cell(v.x, v.y);
    }
}