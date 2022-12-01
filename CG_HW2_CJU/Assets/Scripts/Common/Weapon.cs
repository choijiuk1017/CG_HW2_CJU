using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bear"))
        {
            other.gameObject.GetComponent<Monster>().takeDamage(damage);
        }

        if(other.gameObject.CompareTag("Knight"))
        {
            other.gameObject.GetComponent<Monster>().takeDamage(damage);
        }
    }

}
