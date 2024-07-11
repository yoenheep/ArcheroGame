using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBolt : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * -10f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            Destroy(gameObject, 0.1f);
        }
        if (collision.transform.CompareTag("Player"))
        {
            Destroy(gameObject, 0.1f);

            PlayerHpbar.Instance.currentHp -= 250f;

            PlayerMovement.Instance.Anim.SetTrigger("Dmg");
            Instantiate(EffectSet.Instance.PlayerDmgEffect, PlayerTargeting.Instance.AttackPoint.position, Quaternion.Euler(90, 0, 0));
        }
    }
}
