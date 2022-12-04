using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject door;

    public GameObject goalText;

    Score score;
    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("ScoreCanvas").GetComponentInChildren<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if(score.score <= 0)
            {
                score.increaseScore(100);
            }
            
            door.SetActive(true);
            goalText.SetActive(true);
            
        }
    }
}
