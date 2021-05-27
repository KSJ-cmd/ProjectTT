using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Stone : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public float stoneSpeed;
    public float stoneRotate;

    private float stoneRayDist_x;
    private float stoneRayDist_y;
    Vector3 DestroyPosition;
    public LayerMask whatisPlatform;
    // Start is called before the first frame update
    void Start()
    {
        stoneRayDist_x = GetComponent<CircleCollider2D>().transform.position.x;
        stoneRayDist_y = GetComponent<CircleCollider2D>().transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector2 = new Vector2(stoneSpeed,0);
        Rigidbody2D.position += vector2;
        //µ¹ È¸Àü
        Vector3 vector3 = new Vector3(0, 0, stoneRotate);
        transform.Rotate(vector3);
        //CheckRay();
        Destroy();
    }

    private void Destroy()
    {
        //Vector2 vector2 = new Vector2(transform.position.x + stoneRayDist_y, transform.position.y + stoneRayDist_y-1f);
        //Debug.DrawRay(vector2, new Vector3(0, -(transform.position.y * 2), 0), Color.red);
        
        for (float i = (transform.position.y + stoneRayDist_y);i>0;i-=1f)
        {
            DestroyPosition = new Vector2(transform.position.x +stoneRayDist_y, i);
            //Debug.Log("x:" + transform.position.x + stoneRayDist_x + "y:" + i);
           // Debug.Log("x:" + GetComponent<CircleCollider2D>().transform.position.x  + "y:" + GetComponent<CircleCollider2D>().transform.position.x);
            Collider2D overCollider2d = Physics2D.OverlapCircle(DestroyPosition, 0.01f, whatisPlatform);
            if (overCollider2d != null)
            {
                overCollider2d.transform.GetComponent<Bricks>().MakeDot(DestroyPosition);
            }
        }
    }
    private void CheckRay()
    {

        LayerMask layerMask = new LayerMask();
        layerMask = LayerMask.GetMask("Platform");
        Vector2 vector2 = new Vector2(transform.position.x + stoneRayDist_x, transform.position.y + stoneRayDist_y);

        //Collider2D overCollider2d = Physics2D.OverlapCircle(MousePosition, 0.01f, whatisPlatform);
        RaycastHit2D raycast = Physics2D.Raycast(vector2, new Vector2(0, -(transform.position.y*2)), 5f,layerMask.value);
        Debug.DrawRay(vector2, new Vector3(0, -(transform.position.y * 2), 0), Color.red);
        if(raycast.collider != null)
        {

            Debug.Log("x:" + transform.position.x + "y:" + transform.position.y);
        }
        //for (int i = 0; i < raycast.Length; i++)
        //{
        //    RaycastHit2D hit2D = raycast[i];
        //    Vector3 pos = new Vector3(hit2D.transform.localPosition.x, hit2D.transform.localPosition.y, 0);
        //    Debug.Log("x:"+raycast.transform.localPosition.x + "y:"+raycast.transform.localPosition.y);
        //    hit2D.transform.GetComponent<Bricks>().MakeDot(pos);

        //    //hit2D.transform.GetComponent<Tilemap>().SetTile(new Vector3Int(hit2D.transform.position.x, hit2D.transform.position.y, 0), null);
        //}
    }
}
