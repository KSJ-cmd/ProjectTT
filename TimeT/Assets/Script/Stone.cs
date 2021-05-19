using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public float stoneSpeed;
    public float stoneRotate;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector2 = new Vector2(stoneSpeed,0);
        Rigidbody2D.position += vector2;
        //µ¹ È¸Àü
        Vector3 vector3 = new Vector3(0, 0, stoneRotate);
        transform.Rotate(vector3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}
