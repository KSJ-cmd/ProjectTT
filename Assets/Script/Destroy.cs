using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject destroyArea;
    public LayerMask whaiisPlatform;

    public CapsuleCollider2D CapsuleCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {

        }
    }
}
