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

        anim = GetComponent<Animator>();

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

        transform.LookAt(player.transform.position);
    }

    private void UpdateAttack()
    {
        agent.speed = 0;

        anim.SetBool("isRun", false);

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance >= 5 && distance <= 30)
        {

            state = State.Run;

            anim.SetBool("isRun", true);
        }

        if (distance > 30)
        {

            state = State.Idle;

            anim.SetBool("isRun", false);
        }

    }

    private void UpdateRun()
    {

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 5)
        {

            state = State.Attack;
            agent.speed = 0;
            StartCoroutine(attack());

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
        if (distance <= 30)
        {
            state = State.Run;

            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }

    IEnumerator attack()
    {
        yield return new WaitForSeconds(0.1f);

        StartCoroutine(swing());

    }

    IEnumerator swing()
    {
        anim.SetTrigger("Attack1");

        yield return new WaitForSeconds(2f);

        StartCoroutine(attack());
    }

}