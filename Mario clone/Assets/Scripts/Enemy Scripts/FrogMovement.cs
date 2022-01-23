using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private bool animation_Started;
    private bool animation_Finished;

    private int jumpedTimes=1;
    private bool jumpLeft = true;

    private string coroutine_Name = "FrogJump";

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine(coroutine_Name);
    }

    void LateUpdate()
    {
        if (animation_Finished && animation_Started)
        {
            animation_Started = true;
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;

        }
    }

    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));
        animation_Started = true;
        animation_Finished = false;

        jumpedTimes++;

        if (jumpLeft)
        {
            anim.Play("FrogAnimationLeft");
        }
        else
        {
            anim.Play("FrogAnimationRight");
        }

        StartCoroutine(coroutine_Name);
    }

    void AnimationFinished()
    {
        animation_Finished = true;
        if (jumpLeft)
        {
            anim.Play("FrogIdle");
        }
        else
        {
            anim.Play("FrogIdleRight");
        }

        if (jumpedTimes==3)
        {
            jumpedTimes = 1;
            jumpLeft = !jumpLeft;
            
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1;
            transform.localScale = tempScale;
        }
    }
}
