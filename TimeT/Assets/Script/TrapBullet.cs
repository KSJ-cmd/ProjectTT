using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBullet : MonoBehaviour
{
    Rigidbody2D rigid;
    CapsuleCollider2D CapsuleCollider2D;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

    }
    void Move()
    {

        Vector2 vector2 = new Vector2(bulletSpeed, rigid.velocity.y);
        rigid.velocity = vector2;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
