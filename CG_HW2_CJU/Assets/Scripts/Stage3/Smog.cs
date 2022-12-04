using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smog : MonoBehaviour
{
    GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.Find("Weapon");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void reduceDamage()
    {
        weapon.GetComponentInChildren<Weapon>().damage--;

        Invoke("resetDamage", 2f);
    }

    void resetDamage()
    {
        weapon.GetComponentInChildren<Weapon>().damage++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }

        if (other.gameObject.layer != 3 && other.gameObject.CompareTag("Player"))
        {
            reduceDamage();
            
            Destroy(gameObject,3f);
        }
    }
}
