using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    [SerializeField]
    private Rigidbody myBody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(float speed)
    {
        myBody.AddForce((transform.forward.normalized*-1) * speed);
        Invoke("DeactivateGameObject", 3f);
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Obstacle" || target.gameObject.tag == "Zombie")
        {
           gameObject.SetActive(false);
        }
    }
}
