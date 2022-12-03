using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wizard : MonoBehaviour
{
    Animator anim;

    NavMeshAgent agent;

    Rigidbody rigid;

    GameObject player;

    GameObject stageManager;

    enum State
    {
        Idle,
        Combat,
        Move
    }

    State state;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;

        anim = GetComponentInChildren<Animator>();

        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int hp = GetComponent<Monster>().hp;

        if (state == State.Idle)
        {
            UpdateIdle();
        }
        else if(state == State.Move)
        {
            updateMove();
        }


        if (hp <= 0)
        {
            agent.isStopped = true;
            StopAllCoroutines();
            agent.velocity = Vector3.zero;
        }

        transform.LookAt(player.transform.position);
    }

    private void UpdateIdle()
    {
        agent.speed = 0;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        //생성될때 목적지(Player)를 찿는다.

        //target을 찾으면 Run상태로 전이
        if (distance <= 50)
        {
            state = State.Move;

            anim.SetBool("move_forward", true);
        }
        else
        {

            anim.SetBool("move_forward", false);
        }


    }

    private void updateMove()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 10)
        {
            anim.SetBool("move_forward", false);
            anim.SetBool("idle_normal", true);
            state = State.Combat;
            agent.speed = 0;
        }

        //타겟 방향으로 이동하다가
        agent.speed = 3.5f;

        //요원에게 목적지를 알려준다.
        agent.destination = player.transform.position;
    }

    private void updateCombat()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 10)
        {
            anim.SetBool("move_forward", false);
            anim.SetBool("idle_normal", true);
            state = State.Combat;
            agent.speed = 0;
        }

        //타겟 방향으로 이동하다가
        agent.speed = 3.5f;

        //요원에게 목적지를 알려준다.
        agent.destination = player.transform.position;
    }

}
