using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    private Text coinTextScore;
    private int scoreCount;
    private AudioSource audioManager;
    // Start is called before the first frame update
    void Awake()
    {
        audioManager = GetComponent<AudioSource>();
    }
    void Start()
    {
        coinTextScore = GameObject.Find("Score").GetComponent<Text>() ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.COIN_TAG)
        {
            target.gameObject.SetActive(false);
            scoreCount++;
            audioManager.Play();
            coinTextScore.text = "X " + scoreCount;
        }
    }
}
