using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bricks : MonoBehaviour
{
    public Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    public void MakeDot(Vector3 pos)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(pos);

        tilemap.SetTile(cellPosition, null);
    }
}
