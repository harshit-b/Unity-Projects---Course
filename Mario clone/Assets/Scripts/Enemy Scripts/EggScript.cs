using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2d(Collision2D target)
    {
        if (target.gameObject.tag==MyTags.PLAYER_TAG)
        {
            //Damage the player
        }
        gameObject.SetActive(false);
    }
}
