using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerJumpSpeed;
    public float playerMoveSpeed;
    public bool onJump;
    public bool onWalk;
    public bool onAttack;
    public bool onSit;
    public bool isHurt;
    public bool isHurtknockback;
    public float sitBoxColliderSizeX;
    public float sitBoxColliderSizeY;
    public float IdleBoxColliderSizeX;
    public float IdleBoxColliderSizeY;
    public float knockBackPower;
    public int HP = 3;


    Rigidbody2D rid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    BoxCollider2D Boxcollider;
    AudioSource audioSource;

    // Start is called before the first frame update
    private void Awake()
    {
        rid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Boxcollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        IdleBoxColliderSizeX = transform.GetComponent<BoxCollider2D>().size.x;
        IdleBoxColliderSizeY = transform.GetComponent<BoxCollider2D>().size.y;

    }

    public void OnDie()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        spriteRenderer.flipY = true;

        Boxcollider.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        Sit();
        Attack();
        SitUp();
    }
    private void FixedUpdate()
    {
        Jump();
        Move();
    }
    void Jump()
    {
        if (Input.GetButton("Jump") && !onSit && !onJump && !isHurt && !isHurtknockback)
        {
            Vector2 vector2 = new Vector2(0, playerJumpSpeed);
            rid.AddForce(vector2);
            onJump = true;
        }
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
        if (!onSit)
        {
            Vector2 vector2 = new Vector2(h * playerMoveSpeed, rid.velocity.y);
            rid.velocity = vector2;
        }
    }
    void Attack() {
        if (Input.GetButton("Fire1") && !onAttack)
        {
            onAttack = true;
            GameObject attack = transform.Find("Attack").gameObject;
            attack.SetActive(true);
            StartCoroutine(attackDelay());
        }
    }
    void Sit()
    {
        if (!onJump && Input.GetButton("Vertical"))
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                if (!onSit)
                {
                    Vector2 vector21 = new Vector2(transform.localPosition.x, transform.localPosition.y-1);
                    transform.position = vector21;
                }
                onWalk = false;
                onSit = true;
                rid.GetComponent<Animator>().SetBool("Sit", true);

                Vector2 vector2 = new Vector2(sitBoxColliderSizeX, sitBoxColliderSizeY);
                transform.GetComponent<BoxCollider2D>().size = vector2;
                //transform.position = new Vector2(transform.localPosition.x, transform.localPosition.y);

                
            }

        }
    }
    void SitUp()
    {
        if (Input.GetAxisRaw("Vertical") >= 0)
        {
            rid.GetComponent<Animator>().SetBool("Sit", false);
            Vector2 vector2 = new Vector2(IdleBoxColliderSizeX, IdleBoxColliderSizeY);
            transform.GetComponent<BoxCollider2D>().size = vector2;
            if (onSit)
            {
                Vector2 vector21 = new Vector2(transform.localPosition.x, transform.localPosition.y + 1);
                transform.position = vector21;
            }
            onSit = false;
        }
    }

    void Status()
    {
        if (HP <= 0)
        {
            OnDie();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onJump = false;
            Debug.Log("JUMPFALSE");
        }
        
        if (collision.gameObject.CompareTag("Monster"))
        {
            onJump = false;

            Hurt(collision.transform.position);

            //HP -= 1;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 2f);
        }
    }

    void Hurt(Vector2 pos)
    {
        isHurt = true;
        //HP--;
        if (HP <= 0)
        {
            Status();
        }
        else
        {
            float x = transform.position.x - pos.x;
            if (x < 0)
                x = 1;
            else
                x = -1;

            StartCoroutine(Knockback(x));
            StartCoroutine(alphablink());
            StartCoroutine(HurtRoutine());
        }
        float h = Input.GetAxis("Horizontal");
        
        Vector2 vector2 = new Vector2((h) * knockBackPower, knockBackPower);

        rid.AddForce(vector2);
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
    IEnumerator Knockback(float dir)
    {
        isHurtknockback = true;
        float ctime = 0;
        while (ctime < 0.2f)
        {
            if (transform.rotation.y == 0)
                transform.Translate(Vector2.left * knockBackPower * Time.deltaTime * dir);
            else
                transform.Translate(Vector2.left * knockBackPower * Time.deltaTime * -1f * dir);
            ctime += Time.deltaTime;
            yield return null;
        }
        isHurtknockback = false;
    }
    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(5f);
        isHurt = false;
    }
    IEnumerator alphablink()
    {
        while (isHurt) {
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(1, 1, 1, 1); ;
        }
    }
}
