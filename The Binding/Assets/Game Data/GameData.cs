using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public Vector2 playerWorldPosition;

    public List<Vector2> pickupWorldPositions;

    public Tilemap groundMap;
    public Tilemap terrainMap;
    public Tilemap objectMap;

    public List<Vector2Int> groundTilePositions;
    public List<Vector2Int> terrainTIlePositions;
    public List<Vector2Int> objectTilePositions;

    public void InitialiseTilemapDatas(Tilemap groundMap, Tilemap terrainMap, Tilemap objectMap)
    {
        this.groundMap = groundMap;
        this.terrainMap = terrainMap;
        this.objectMap = objectMap;

        InitialiseTilePositionData(groundMap, groundTilePositions);
        InitialiseTilePositionData(terrainMap, terrainTIlePositions);
        InitialiseTilePositionData(objectMap, objectTilePositions);
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

    public Vector3Int WorldToTIlePosition(Vector2 worldPos, Tilemap tilemap)
    {
        return tilemap.WorldToCell(worldPos);
    }
}
