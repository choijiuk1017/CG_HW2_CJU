using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneButton : MonoBehaviour
{
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startButton()
    {
        SceneManager.LoadScene("Stage 1");
    }

    public void optionButton()
    {
        panel.SetActive(true);
    }

    public void optionExit()
    {
        panel.SetActive(false);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

}
