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

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        StartCoroutine(move());
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

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance > 2 && distance < 7)
        {
            state = State.Run;

            anim.SetBool("Run Forward", true);
        }
    }

    private void UpdateRun()
    {


        //���� �Ÿ��� 2���Ͷ�� �����Ѵ�.
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= 2)
        {
            state = State.Attack;
            anim.SetTrigger("Attack");
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
        //target�� ã���� Run���·� �����ϰ� �ʹ�.
        if (distance <= 5)
        {
            state = State.Run;
            //�̷��� state���� �ٲ�ٰ� animation���� �ٲ��? no! ����ȭ�� ������Ѵ�.
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

        float dir1 = Random.Range(-1f, 1f);
        float dir2 = Random.Range(-1f, 1f);

        rigid.velocity = new Vector3(dir1, 0, dir2);

        transform.LookAt(transform.position + rigid.velocity * Time.deltaTime);

        Quaternion newRotation = Quaternion.LookRotation(transform.position + rigid.velocity);

        rigid.rotation = Quaternion.Slerp(rigid.rotation, newRotation, 100f * Time.deltaTime);

        anim.SetBool("WalkForward", rigid.velocity != Vector3.zero);

        yield return new WaitForSeconds(5f);
    }
}
