using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public Vector2 playerWorldPosition = new Vector2(float.NegativeInfinity, float.NegativeInfinity);

    public Tilemap groundMap;
    public Tilemap terrainMap;

    public List<Vector2Int> groundTilePositions;
    public List<Vector2Int> terrainTilePositions;

    public void InitialiseTilemapDatas(Tilemap groundMap, Tilemap terrainMap)
    {
        this.groundMap = groundMap;
        this.terrainMap = terrainMap;

        Debug.Log(this.groundMap);

        InitialiseTilePositionData(groundMap, groundTilePositions);
        InitialiseTilePositionData(terrainMap, terrainTilePositions);
    }

    private void InitialiseTilePositionData(Tilemap tileMap, List<Vector2Int> availablePlaces)
    {
        availablePlaces = new List<Vector2Int>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    availablePlaces.Add(new Vector2Int((int)place.x, (int)place.y));
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
    }

    public Vector3Int WorldToTilePosition(Vector2 worldPos, Tilemap tilemap)
    {
        return tilemap.WorldToCell(worldPos);
    }
}
