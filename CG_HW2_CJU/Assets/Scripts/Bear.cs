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

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        StartCoroutine(move());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 7f)
        {
            StopCoroutine(move());

            agent.destination = player.transform.position;

            if (Vector3.Distance(transform.position, player.transform.position) > 2f)
            {
                agent.ResetPath();

            }
        }
        


    }

    IEnumerator move()
    {
        rigid = GetComponent<Rigidbody>();

        while (true)
        {
            float dir1 = Random.Range(-1f, 1f);
            float dir2 = Random.Range(-1f, 1f);


            yield return new WaitForSeconds(5f);

            rigid.velocity = new Vector3(dir1, 0, dir2);

            transform.LookAt(transform.position + rigid.velocity);

            Quaternion newRotation = Quaternion.LookRotation(transform.position + rigid.velocity);

            rigid.rotation = Quaternion.Slerp(rigid.rotation, newRotation, 100f*Time.deltaTime);

            anim.SetBool("WalkForward", rigid.velocity != Vector3.zero);
        }
    }
}
