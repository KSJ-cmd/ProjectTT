using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 10f;
    private float reversetime = 0f;
    private float attackspeed = 0.0f;
    private int dir = 1;
    private BoxCollider2D MonsterCollider;
    private Rigidbody2D MonsterRigidbody;
    public GameObject targetPosition;
    private bool isCamera=false;
   

    // Start is called before the first frame update
    void Start()
    {
        MonsterCollider = GetComponent<BoxCollider2D>();
        MonsterRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        MonsterRigidbody.velocity = new Vector2(speed, 0);
        reversetime += Time.deltaTime;

        if (isCamera)
        {
            Vector2 playervector = new Vector2(targetPosition.transform.position.x, gameObject.transform.position.y);
            transform.position = Vector2.MoveTowards(gameObject.transform.position, playervector, attackspeed);
        }
        else if(reversetime>1)
        {
            speed *= -1;
            dir *= -1;
            reversetime = 0f;
        }
        Vector2 rayVector = new Vector2(transform.position.x+dir, transform.position.y);
        Ray2D ray = new Ray2D(rayVector,transform.forward);
        int layerMask = 1 << LayerMask.NameToLayer("Player");
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, new Vector3(dir, 0, 0), 5f,layerMask);
        if (hit)
        {
            Debug.Log("raycast");
            StartCoroutine(Attack());
        }
       


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag=="Wall")
        {
            Debug.Log("trigger");
            reversetime = 0f;
            speed *= -1;
            dir *= -1;
        }
        if(collision.tag=="MainCamera")
        {
            Debug.Log("Camera");
            isCamera = true;
        }
    }

    IEnumerator Attack()
    {
        MonsterRigidbody.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1);
        MonsterRigidbody.AddForce(new Vector2(speed*10,0),ForceMode2D.Impulse);
        yield return new WaitForSeconds(3);

    }
    
}

