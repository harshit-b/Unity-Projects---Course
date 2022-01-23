using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMovement : MonoBehaviour
{
    public float moveSpeed = 1f;

    public LayerMask playerLayer;

    private Rigidbody2D myBody;
    private Animator anim;

    private bool moveLeft;
    private bool canMove = true;
    private bool stunned;

    public Transform left_collision, right_collision, top_collision, down_collision;
    private Vector3 leftCollision_Position, rightCollision_Position;
  

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        leftCollision_Position = left_collision.position;
        rightCollision_Position = right_collision.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
            }

            else
            {
                myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
            }
        }
        CheckCollision();

    }

    void CheckCollision ()
    {

        RaycastHit2D leftHit = Physics2D.Raycast(left_collision.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(right_collision.position, Vector2.right, 0.1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(top_collision.position, 0.2f, playerLayer);

        if(topHit != null)
        {
            if (topHit.gameObject.tag == "Player")
            {
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
                        new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);

                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);

                    anim.Play("Stunned");
                    stunned = true;

                    if(tag == MyTags.BEETLE_TAG)
                    {
                        anim.Play("Stunned");
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }

        if (leftHit)
        {
            if (leftHit.collider.gameObject.tag=="Player")
            {
                if (!stunned)
                {
                    leftHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else
                {
                    if (tag!=MyTags.BEETLE_TAG)
                    {
                        myBody.velocity = new Vector2(15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                        

                }
                    
            }
        }

        if (rightHit)
        {
            if (rightHit.collider.gameObject.tag == "Player")
            {
                if (!stunned)
                {
                    rightHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else
                {
                    if (tag != MyTags.BEETLE_TAG)
                    {
                        myBody.velocity = new Vector2(-15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }
            

        if (!Physics2D.Raycast(down_collision.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }

    }

    void ChangeDirection()
    {
        moveLeft = !moveLeft;
        Vector3 tempScale = transform.localScale;

        if (moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);

            leftCollision_Position = left_collision.position;
            rightCollision_Position = right_collision.position;
        }

        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);

            leftCollision_Position = right_collision.position;
            rightCollision_Position = left_collision.position;
        }

        transform.localScale = tempScale;

        
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.BULLET_TAG)
        {
            if (tag==MyTags.BEETLE_TAG)
            {
                anim.Play("Stunned");

                canMove = false;
                myBody.velocity = new Vector2(0, 0);

                StartCoroutine(Dead(0.4f));
            }

            if (tag==MyTags.SNAIL_TAG)
            {
                if (!stunned)
                {
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);

                    anim.Play("Stunned");
                    stunned = true;
                }

                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    //void OnCollisionEnter2D(Collision2D target)
    //{
    //    if (target.gameObject.tag == "Player")
    //    {
    //        anim.Play("Stunned");
    //    }
    //}
}
