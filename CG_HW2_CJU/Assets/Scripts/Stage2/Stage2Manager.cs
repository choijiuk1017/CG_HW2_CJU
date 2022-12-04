using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Manager : MonoBehaviour
{

    public GameObject mission;

    public GameObject success;

    // Start is called before the first frame update
    void Start()
    {
        mission.SetActive(true);
        Invoke("resetCanvas", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Bear") == null)
        {
            success.SetActive(true);
        }
    }
    void resetCanvas()
    {
        mission.SetActive(false);
    }

}
