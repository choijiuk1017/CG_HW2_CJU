using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public int hp;


    Animator anim;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        hp = 10;

        anim = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (hp <= 0)
        {
            agent.Stop();
            anim.SetBool("Death", true);
            Destroy(gameObject, 1f);
        }
    }

    public int getHp()
    {
        return hp;
    }

    public void takeDamage(int damage)
    {
        hp = hp - damage;
    }
}
