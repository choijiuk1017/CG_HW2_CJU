using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Manager : MonoBehaviour
{
    public GameObject mission;

    // Start is called before the first frame update
    void Start()
    {
        mission.SetActive(true);
        Invoke("resetCanvas", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void resetCanvas()
    {
        mission.SetActive(false);
    }
}
