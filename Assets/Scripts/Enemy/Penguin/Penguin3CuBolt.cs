using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin3CuBolt : MonoBehaviour
{
    Rigidbody rb;
    Vector3 NewDir;
    int bounceCnt = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        NewDir = transform.up;
        rb.velocity = NewDir * -10f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision Name : " + collision.transform.name);
        if (collision.transform.CompareTag("Wall"))
        {
            bounceCnt--;
            if(bounceCnt > 0)
            {
                Debug.Log("hit wall");
                NewDir = Vector3.Reflect(NewDir, collision.contacts[0].normal);
                rb.velocity = NewDir * -10f;
            }
            else
            {
                Destroy(gameObject, 0.1f);
            }
        }

        if (collision.transform.CompareTag("Player"))
        {
            bounceCnt--;
            if (bounceCnt > 0)
            {
                Debug.Log("hit Player");
                NewDir = Vector3.Reflect(NewDir, collision.contacts[0].normal);
                rb.velocity = NewDir * -10f;
            }
            else
            {
                Destroy(gameObject, 0.1f);
            }

            PlayerHpbar.Instance.currentHp -= 250f;

            PlayerMovement.Instance.Anim.SetTrigger("Dmg");
            Instantiate(EffectSet.Instance.PlayerDmgEffect, PlayerTargeting.Instance.AttackPoint.position, Quaternion.Euler(90, 0, 0));
        }
    }
}
