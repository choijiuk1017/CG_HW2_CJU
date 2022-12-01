using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bear : MonoBehaviour
{
    Animator anim;

    NavMeshAgent agent;

    Rigidbody rigid;

    public GameObject player;

    bool isFireReady;

    public float attackRate;
    float attackDelay;

    enum State
    { 
        Idle,
        Run,
        Attack
    }

    State state;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame

    void Update()
    {
        int hp = GetComponent<Monster>().hp;

        if (state == State.Idle )
        {
            UpdateIdle();
        }
        else if (state == State.Run )
        {
            //StopCoroutine("move");
            UpdateRun();
        }
        else if (state == State.Attack )
        {
            //StopCoroutine("move");
            UpdateAttack();
        }

        if(hp <= 0)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }
        
        transform.LookAt(player.transform.position);

    }

    private void UpdateAttack()
    {
        agent.speed = 0;

        anim.SetBool("Run Forward", false);

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance >= 3 && distance <= 30)
        {

            state = State.Run;

            anim.SetBool("Run Forward", true);
        }
        
        if(distance > 30)
        {

            state = State.Idle;

            anim.SetBool("Run Forward", false);
        }
        
    }

    private void UpdateRun()
    {
       // StopCoroutine("move");
        //남은 거리가 2미터라면 공격한다.
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 3)
        {
           // StopCoroutine("move");

            state = State.Attack;
            agent.speed = 0;
            StartCoroutine(think());
            
        }

        //타겟 방향으로 이동하다가
        agent.speed = 3.5f;

        //요원에게 목적지를 알려준다.
        agent.destination = player.transform.position;

    }

    private void UpdateIdle()
    {
        agent.speed = 0;

        rigid = GetComponent<Rigidbody>();

        float distance = Vector3.Distance(transform.position, player.transform.position);
        //생성될때 목적지(Player)를 찿는다.

        //target을 찾으면 Run상태로 전이
        if (distance <= 30)
        {
            state = State.Run;

            anim.SetBool("Run Forward", true);
        }
        else
        {
            anim.SetBool("Run Forward", false);
        }
    }

    IEnumerator think()
    {
        yield return new WaitForSeconds(0.1f);

        int ranAction = Random.Range(0, 5);

        switch (ranAction)
        { 
            case 0:
                StartCoroutine(leftHand());
                break;

            case 1:
            case 2:
                StartCoroutine(twoHand());
                break;
            case 3:

            case 4:
                StartCoroutine(rightHand());
                break;
        
        }

    }

    IEnumerator rightHand()
    {
        anim.SetTrigger("Attack1");
        
        yield return new WaitForSeconds(1f);

        StartCoroutine(think());
    }

    IEnumerator leftHand()
    {
        anim.SetTrigger("Attack2");
        
        yield return new WaitForSeconds(1f);

        StartCoroutine(think());
    }

    IEnumerator twoHand()
    {
        anim.SetTrigger("Attack5");
        
        yield return new WaitForSeconds(2f);

        StartCoroutine(think());
    }

}
