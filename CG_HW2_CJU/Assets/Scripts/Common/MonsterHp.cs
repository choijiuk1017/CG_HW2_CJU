using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHp : MonoBehaviour
{
    public Slider hpBar;

    public GameObject monster;

    float maxHp;
    float currentHp;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = monster.GetComponent<Monster>().hp;
    }

    // Update is called once per frame
    void Update()
    {
        currentHp = monster.GetComponent<Monster>().hp;

        hpBar.value = currentHp / maxHp;
    }
}
