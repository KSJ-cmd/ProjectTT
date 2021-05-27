using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator Anim;
    SpriteRenderer SpriteRenderer;
    public int nextMove;
    // Start is called before the first frame update

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Think", 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 vector2 = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        RaycastHit2D raycastHit2D = Physics2D.Raycast(vector2, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if(raycastHit2D.collider == null)
        {
            Turn();
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);


        Anim.SetInteger("WalkSpeed", nextMove);

        if(nextMove != 0)
        {
            SpriteRenderer.flipX = nextMove == 1;
        }
        
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove *= -1;
        SpriteRenderer.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think", 2);
    }
}
