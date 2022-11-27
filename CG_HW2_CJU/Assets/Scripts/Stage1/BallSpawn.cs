using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour
{

    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawn()
    {
       
        while(true)
        {
            Instantiate(ball, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }

    }
}
