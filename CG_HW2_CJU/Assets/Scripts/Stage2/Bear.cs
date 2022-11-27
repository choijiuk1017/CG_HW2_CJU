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

        StartCoroutine("move");
    }

    // Update is called once per frame
    void Update()
    {

        if (state == State.Idle)
        {
            UpdateIdle();
        }
        else if (state == State.Run)
        {
            UpdateRun();
        }
        else if (state == State.Attack)
        {
            UpdateAttack();
        }

    }

    private void UpdateAttack()
    {
        agent.speed = 0;

        anim.SetBool("Run Forward", false);


        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance >= 3 && distance <= 10)
        {
            state = State.Run;

            anim.SetBool("Run Forward", true);
        }
        
        if(distance > 10)
        {
            state = State.Idle;

            anim.SetBool("Run Forward", false);
        }
        
    }

    private void UpdateRun()
    {
        anim.SetBool("WalkBackward", false);
        //남은 거리가 2미터라면 공격한다.
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 3)
        {
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
        anim.SetBool("WalkBackward", false);
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //생성될때 목적지(Player)를 찿는다.

        //target을 찾으면 Run상태로 전이하고 싶다.
        if (distance <= 10)
        {
            StopCoroutine("move");

            state = State.Run;

            anim.SetBool("Run Forward", true);
        }
        else
        {
            anim.SetBool("Run Forward", false);
        }
    }



    IEnumerator move()
    {
        rigid = GetComponent<Rigidbody>();

        while(true)
        {
            float dir1 = Random.Range(-1f, 1f);
            float dir2 = Random.Range(-1f, 1f);

            yield return new WaitForSeconds(5f);

            rigid.velocity = new Vector3(dir1, 0, dir2);

            transform.LookAt(transform.position + rigid.velocity * Time.deltaTime);

            Quaternion newRotation = Quaternion.LookRotation(transform.position + rigid.velocity);

            rigid.rotation = Quaternion.Slerp(rigid.rotation, newRotation, 100f * Time.deltaTime);

            anim.SetBool("WalkForward", rigid.velocity != Vector3.zero);
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
        
        yield return new WaitForSeconds(2f);
    }

    IEnumerator leftHand()
    {
        anim.SetTrigger("Attack2");
        
        yield return new WaitForSeconds(2f);

    }

    IEnumerator twoHand()
    {
        anim.SetTrigger("Attack5");
        
        yield return new WaitForSeconds(3f);
    }
}
