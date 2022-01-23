using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private float jumpPower = 10f;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    private Rigidbody2D myBody;
    private Animator anim;

    private bool jumped;
    private bool doubleJumped;
    private bool isGrounded;


    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        PlayerJump();

    }

    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            ChangeDirection(1);
        }

        else if (h < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-1);
        }

        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    void FixedUpdate()
    {
        PlayerWalk();
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

        if (isGrounded)
        {
            if (jumped) {
                jumped = false;
                anim.SetBool("Jump", false);
            }
        }
    }
    void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                //print(jumped);
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);

                anim.SetBool("Jump", true);
            }
        }
    }
    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    //void OnCollisionEnter2D(Collision2D target)
    //{
    //    if (target.gameObject.tag == "Snail")
    //    {
    //        print("Hit the snail");
    //    }
    //}
}
