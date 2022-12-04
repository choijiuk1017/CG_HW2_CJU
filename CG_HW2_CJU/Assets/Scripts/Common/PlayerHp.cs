using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Slider hpBar;

    public GameObject player;

    float maxHp;
    float currentHp;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = player.GetComponent<Player>().hp;
    }

    // Update is called once per frame
    void Update()
    {
        currentHp = player.GetComponent<Player>().hp;

        hpBar.value = currentHp / maxHp;
    }
}
