using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Board: MonoBehaviour{
    public Dictionary<Vector3Int, TileLogic> tiles;
    public List<Floor> floors;
    public static Board instance; /* Isso só se faz se você tiver certeza de que nunca haverá múltiplas instâncias dessa classe rodando simultaneamente */
    [HideInInspector] 

    public Grid grid;

    void Awake(){
        tiles = new Dictionary<Vector3Int, TileLogic>();
        instance = this;
        grid = GetComponent<Grid>();
    }

    void Start(){
        InitSequence();
    }

    public void InitSequence() {
        LoadFloors();
        Debug.Log("Foram criados " + tiles.Count + " tiles.");
    }

    void LoadFloors(){
        for (int i = 0; i < floors.Count; i++) {
            List<Vector3Int> floorTiles = floors[i].LoadTiles();
            for (int j = 0; j < floorTiles.Count; j++) {
                if (!tiles.ContainsKey(floorTiles[j])) {
                    CreateTile(floorTiles[j], floors[i]);
                }
            }
        }
    }

    void CreateTile(Vector3Int pos, Floor floor) {
        Vector3 worldPos = grid.CellToWorld(pos);
        worldPos.y += floor.tilemap.tileAnchor.y / 2;
        TileLogic tileLogic = new TileLogic(pos, worldPos, floor);
        tiles.Add(pos, tileLogic);
    }
}