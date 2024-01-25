using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] prefabTile;
    
    Dictionary<Vector2Int, GameObject> tiles;

    public  void Init(int width, int height)
    {
        tiles = new Dictionary<Vector2Int, GameObject>(width * height);
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                int idx = x + y * width;
                int nTileIdx = 0;
                if (idx % 2 == 0) nTileIdx = 1;
                GameObject objTile = Instantiate(prefabTile[nTileIdx], this.transform);
                Vector2Int vTilePos = new Vector2Int(x, y);
                objTile.name = vTilePos.ToString();
                objTile.transform.position = new Vector3(x, y,0);
                tiles.Add(vTilePos, objTile);
            }
        }
    }
}
