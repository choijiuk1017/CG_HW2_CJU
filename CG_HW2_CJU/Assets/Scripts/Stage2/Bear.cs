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

    public GameObject rightHand;
    public GameObject leftHand;

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
            StopAllCoroutines();
            agent.velocity = Vector3.zero;
        }
        
        transform.LookAt(player.transform.position);

    }

    private void UpdateAttack()
    {
        agent.speed = 0;

        anim.SetBool("Run Forward", false);

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance >= 5  && distance <= 30)
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
        //���� �Ÿ��� 2���Ͷ�� �����Ѵ�.
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 5)
        {
           // StopCoroutine("move");

            state = State.Attack;
            agent.speed = 0;
            StartCoroutine(Think());
            
        }

        //Ÿ�� �������� �̵��ϴٰ�
        agent.speed = 3.5f;

        //������� �������� �˷��ش�.
        agent.destination = player.transform.position;

    }

    private void UpdateIdle()
    {
        agent.speed = 0;

        rigid = GetComponent<Rigidbody>();

        float distance = Vector3.Distance(transform.position, player.transform.position);
        //�����ɶ� ������(Player)�� �O�´�.

        //target�� ã���� Run���·� ����
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

    void resetRightCollider()
    {
        rightHand.GetComponent<BoxCollider>().enabled = false;
    }

    void resetLeftCollider()
    {
        leftHand.GetComponent<BoxCollider>().enabled = false;
    }

    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.1f);

        int ranAction = Random.Range(0, 5);

        switch (ranAction)
        { 
            case 0:
                StartCoroutine(LeftHand());
                break;

            case 1:
            case 2:
                StartCoroutine(TwoHand());
                break;
            case 3:

            case 4:
                StartCoroutine(RightHand());
                break;
        
        }

    }

    IEnumerator RightHand()
    {
        anim.SetTrigger("Attack1");
        rightHand.GetComponent<BoxCollider>().enabled = true;
        Invoke("resetRightCollider", 0.5f);
        yield return new WaitForSeconds(2f);

        StartCoroutine(Think());
    }

    IEnumerator LeftHand()
    {
        anim.SetTrigger("Attack2");
        leftHand.GetComponent<BoxCollider>().enabled = true;
        Invoke("resetLeftCollider", 0.5f);
        yield return new WaitForSeconds(2f);

        StartCoroutine(Think());
    }

    IEnumerator TwoHand()
    {
        anim.SetTrigger("Attack5");
        rightHand.GetComponent<BoxCollider>().enabled = true;
        leftHand.GetComponent<BoxCollider>().enabled = true;
        Invoke("resetLeftCollider", 1f);
        Invoke("resetRightCollider", 1f);
        yield return new WaitForSeconds(3f);

        StartCoroutine(Think());
    }

}
