using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    private float dis;
    private float speed;
    private float waitTime;
    GameObject player;


    void Start()
    {
        player = GameObject.Find("Player");

        dis = Vector3.Distance(transform.position,player.transform.position);

        //포탄생성후 초반에 포탄이 벌어지듯이 연출하기위해
        //포탄의 회전을 캐릭터위치에서 포탄의 위치의 방향으로 놓습니다
        transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);

    }


    void Update()
    {
        diffusionMagic();
    }

    void diffusionMagic()
    {
        if (player.transform.position == null) return;


        waitTime += Time.deltaTime;
        if (waitTime < 0.5f)
        {
            speed = Time.deltaTime;
            transform.Translate(transform.forward * speed, Space.World);
        }
        else
        {
            // 1.5초 이후 타겟방향으로 lerp위치이동 합니다

            speed += Time.deltaTime;
            float t = speed / dis;

            transform.position = Vector3.LerpUnclamped(transform.position, player.transform.position, t);

        }


        Vector3 directionVec = player.transform.position - transform.position;
        Quaternion qua = Quaternion.LookRotation(directionVec);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, Time.deltaTime * 2f);

    }

    
}
