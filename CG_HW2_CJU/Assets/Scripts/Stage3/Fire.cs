using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    GameObject player;

    private float damageTime;
    private float currentDamageTime;

    private float durationTime;
    private float currentDurationTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        durationTime = 3f;

        currentDurationTime = durationTime;

        damageTime = 2f;

    }

    // Update is called once per frame
    void Update()
    {
        ElapseTime();
    }

    private void ElapseTime()
    {
        currentDurationTime -= Time.deltaTime;



        if (currentDamageTime > 0)
        {
            currentDamageTime -= Time.deltaTime;  // 1ÃÊ¿¡ 1¾¿
        }
            
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }

        if (other.gameObject.layer != 3 &&  other.gameObject.CompareTag("Player"))
        {
            

            if (currentDamageTime <= 0)
            {
                player.GetComponent<Player>().takeDamage(1);
                currentDamageTime = damageTime;

            }

            if(currentDurationTime <= 0)
            {
                Destroy(gameObject);
            }
        }

        
    }
}
