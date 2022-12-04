using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Knight : MonoBehaviour
{
    Animator anim;

    NavMeshAgent agent;

    Rigidbody rigid;

    GameObject player;

    GameObject stageManager;

    float time;

    public GameObject weapon;
    enum State
    {
        Idle,
        Walk,
        Run,
        Attack
    }

    State state;
    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;

        stageManager = GameObject.Find("GameManager");

        anim = GetComponentInChildren<Animator>();

        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player");

        time = stageManager.GetComponent<Stage3Manager>().setTime;
    }

    // Update is called once per frame
    void Update()
    {

        int hp = GetComponent<Monster>().hp;

        if (state == State.Idle)
        {
            UpdateIdle();
        }
        else if(state == State.Walk)
        {
            UpdateWalk();
        }
        else if (state == State.Run)
        {
            UpdateRun();
        }
        else if (state == State.Attack)
        {
            UpdateAttack();
        }

        if (hp <= 0)
        {
            agent.isStopped = true;
            StopAllCoroutines();
            agent.velocity = Vector3.zero;
        }

        if(time <= 0)
        {
            Destroy(gameObject);
        }

        transform.LookAt(player.transform.position);
    }

    private void UpdateAttack()
    {
        agent.speed = 0;

        anim.SetBool("isRun", false);

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance >= 3 && distance <= 7)
        {
            
            anim.SetBool("isRun", true);

            StopAllCoroutines();

            state = State.Run;
        }


    }

    private void UpdateWalk()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 7)
        {
            anim.SetBool("isWalk", false);
            state = State.Run;
            anim.SetBool("isRun", true);
        }

        agent.speed = 2.5f;
        agent.destination = player.transform.position;
    }
    private void UpdateRun()
    {

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 3)
        {
            anim.SetBool("isRun", false);
            state = State.Attack;
            agent.speed = 0;
            StartCoroutine(Attack());

        }

        //Ÿ�� �������� �̵��ϴٰ�
        agent.speed = 3.5f;

        //������� �������� �˷��ش�.
        agent.destination = player.transform.position;

    }

    private void UpdateIdle()
    {
        agent.speed = 0;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        //�����ɶ� ������(Player)�� �O�´�.

        //target�� ã���� Run���·� ����
        if (distance <= 100)
        {
            state = State.Walk;

            anim.SetBool("isWalk", true);
        }
        else
        {

            anim.SetBool("isWalk", false);
        }
    }
    void resetCollider()
    {
        weapon.GetComponent<BoxCollider>().enabled = false;
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);

        StartCoroutine(Swing());

    }


    IEnumerator Swing()
    {
        anim.SetTrigger("Attack");
        weapon.GetComponent<BoxCollider>().enabled = true;
        Invoke("resetCollider", 0.1f);
        yield return new WaitForSeconds(2f);

        StartCoroutine(Attack());
    }

}
