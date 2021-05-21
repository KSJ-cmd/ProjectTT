using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : MonoBehaviour
{
    public Texture2D srcTexture;
    Texture2D newTexture;
    SpriteRenderer sr;

    float worldWidth, worldHeight;
    int pixelWidth, pixelHeight;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        newTexture = Instantiate(srcTexture);
        //newTexture = new Texture2D(100, 20);

        //for(int i = 0; i < newTexture.width; i++)
        //{
        //    for(int j = 0; i < newTexture.height; j++)
        //    {
        //        newTexture.SetPixel(i, j, Color.white);
        //    }
        //}

        //newTexture = new Texture2D(srcTexture.width, srcTexture.height);
        //Color[] colors = srcTexture.GetPixels();
        //newTexture.SetPixels(colors);

        newTexture.Apply();
        sr.sprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), Vector2.one * 0.5f);

        worldWidth = sr.bounds.size.x;
        worldHeight = sr.bounds.size.y;
        pixelWidth = sr.sprite.texture.width;
        pixelHeight = sr.sprite.texture.height;



        gameObject.AddComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
