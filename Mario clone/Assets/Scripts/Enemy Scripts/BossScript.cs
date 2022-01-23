using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject stone;
    private Animator anim;
    public Transform attackInstantiate;
    private string coroutine_Name = "StartAttack";

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(coroutine_Name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        GameObject obj = Instantiate(stone, attackInstantiate.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, -700f), 0f));
    }

    void BackToIdle()
    {
        anim.Play("BossIdleAnimation");
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        anim.Play("BossAttackAnimation");
        StartCoroutine(coroutine_Name);
    }

    public void DeactivateBossScript()
    {
        StopCoroutine(coroutine_Name);
        enabled = false;
        StartCoroutine(Dead(0.5f));
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}
