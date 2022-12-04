using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int nextStageNum;

    GameObject scoreCanvas;
    // Start is called before the first frame update
    void Start()
    {
        scoreCanvas = GameObject.Find("ScoreCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(scoreCanvas);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Stage" + nextStageNum);
            DontDestroyOnLoad(scoreCanvas);
        }
    }
}
