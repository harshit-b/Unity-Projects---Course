using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private Animator anim;
    private int health = 1;

    private bool canDamage;

    void Awake()
    {
        anim = GetComponent<Animator>();
        canDamage = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag==MyTags.BULLET_TAG)
        {
            if (canDamage)
            {
                health--;
                canDamage = false;
                if (health == 0)
                {
                    GetComponent<BossScript>().DeactivateBossScript();
                    anim.Play("BossDead");
                }

                StartCoroutine(WaitForDamage());
            }
        }
    }
}
