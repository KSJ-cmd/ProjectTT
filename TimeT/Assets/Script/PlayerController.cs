using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public float playerJumpSpeed;
    public float playerMoveSpeed;
    public bool onJump;
    public bool onWalk;
    public bool onAttack;
    public bool onSit;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        Move();

        if (Input.GetButtonDown("Fire1")&& !onAttack)
        {
            Attack();
        }
        if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxis("Vertical") < 0)
                Sit();
        }
    
    }
    void Jump()
    {
        Vector2 vector2 = new Vector2(0, playerJumpSpeed);
        Rigidbody2D.AddForce(vector2);
        Debug.Log("Jump");
        onJump = true;
        Rigidbody2D.gravityScale = 5;
    }
    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        if (h > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            onWalk = true;
        }
        else if (h == 0)
        {
            onWalk = false;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            onWalk = true;
        }
        Vector2 vector2 = new Vector2();
        vector2.x = h * Time.deltaTime * playerMoveSpeed;
        transform.Translate(vector2);
        
    }
    void Sit()
    {
        onSit = true;
        if (Input.GetAxis("Vertical")==0)
        {
            onSit = false;
        }
    }
    void Attack() {
        onAttack = true;
        GameObject attack = transform.Find("Attack").gameObject;
        attack.SetActive(true);
        StartCoroutine(attackDelay());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Rigidbody2D.gravityScale = 1;
            onJump = false;
            Debug.Log("JUMPFALSE");
        }
    }
    IEnumerator attackDelay()
    {
        if (onAttack)
        {
            yield return new WaitForSeconds(1f);
            transform.Find("Attack").gameObject.SetActive(false);
            onAttack = false;
            Debug.Log("attackDelay");

        }
    }
}
