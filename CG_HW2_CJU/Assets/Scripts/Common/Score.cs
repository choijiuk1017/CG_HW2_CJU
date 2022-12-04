using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text= "Á¡¼ö: " + score;
    }

    public void increaseScore(int incscore)
    {
        score += incscore;
    }

    public void setScore(int incscore)
    {
        score = incscore;
    }
}
