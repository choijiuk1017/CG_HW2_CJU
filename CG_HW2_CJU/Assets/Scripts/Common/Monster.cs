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
        anim = GetComponentInChildren<Animator>();

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame

    void Update()
    {
        if (hp <= 0)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            anim.SetBool("Death", true);
            Destroy(gameObject, 1f);
        }
    }


    public void takeDamage(int damage)
    {
        hp = hp - damage;
    }
}
