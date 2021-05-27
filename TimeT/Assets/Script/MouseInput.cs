using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour 
{ 

    Vector3 MousePosition;
    public LayerMask whatisPlatform;

    // Start is called before the first frame update
   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(MousePosition, 0.2f);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D overCollider2d = Physics2D.OverlapCircle(MousePosition, 0.01f, whatisPlatform);
            if (overCollider2d != null)
            {
                overCollider2d.transform.GetComponent<Bricks>().MakeDot(MousePosition);
            }
        }   
    }
}
