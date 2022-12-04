using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Wizard : MonoBehaviour
{
    Animator anim;

    NavMeshAgent agent;

    Rigidbody rigid;

    GameObject player;

    public GameObject fire;
    public GameObject blood;
    public GameObject smog;

    public GameObject spawnPoint;

    public GameObject canvas;

    enum State
    {
        Idle,
        Combat,
        Move
    }

    State state;

    Score score;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;

        anim = GetComponentInChildren<Animator>();

        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player");
        score = GameObject.Find("ScoreCanvas").GetComponentInChildren<Score>();

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
            UpdateMove();
        }
        else if(state == State.Combat)
        {
           UpdateCombat();
        }


        if (hp <= 0)
        {
            SceneManager.LoadScene("Stage4");
            if (score.score >= 1000 && score.score <= 10000)
            {
                score.setScore(10000);
            }
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


        if (distance <= 50)
        {
            canvas.SetActive(true);
            state = State.Move;

            anim.SetBool("move_forward", true);
        }
        else
        {

            anim.SetBool("move_forward", false);
        }


    }

    private void UpdateMove()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 10)
        {
            anim.SetBool("move_forward", false);
            anim.SetBool("idle_normal", true);
            state = State.Combat;
            agent.speed = 0;
            StartCoroutine(Think());
        }

        //타겟 방향으로 이동하다가
        agent.speed = 3.5f;

        //요원에게 목적지를 알려준다.
        agent.destination = player.transform.position;
    }
    

    private void UpdateCombat()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        anim.SetBool("idle_normal", false);

        anim.SetBool("idle_combat", true);

        if(distance > 10)
        {
            anim.SetBool("move_forward", true);
            anim.SetBool("idle_normal", false);
            StopAllCoroutines();
            state = State.Move;
        }

    }

    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.1f);

        int ranAction = Random.Range(0, 5);

        switch (ranAction)
        {
            case 0:
                StartCoroutine(Fire());
                break;

            case 1:
            case 2:
                StartCoroutine(Blood());
                break;
            case 3:

            case 4:
                StartCoroutine(Smog());
                break;

        }
    }

    IEnumerator Fire()
    {
        anim.SetTrigger("Attack");

        Instantiate(fire, spawnPoint.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(2f);
        StartCoroutine(Think());
    }

    IEnumerator Blood()
    {
        anim.SetTrigger("Attack");

        Instantiate(blood, spawnPoint.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(2f);
        StartCoroutine(Think());
    }

    IEnumerator Smog()
    {
        anim.SetTrigger("Attack");

        Instantiate(smog, spawnPoint.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(2f);
        StartCoroutine(Think());
    }
}
