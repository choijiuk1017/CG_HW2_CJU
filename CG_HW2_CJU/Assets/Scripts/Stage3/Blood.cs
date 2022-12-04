using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
        if (other.gameObject.layer != 3  && other.gameObject.CompareTag("Player"))
        {

            player.GetComponent<Player>().takeDamage(2);
            Destroy(gameObject);
        }

        
    }
}
