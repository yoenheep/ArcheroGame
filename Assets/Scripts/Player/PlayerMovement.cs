using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance // ΩÃ±€≈Ê
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerMovement>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("PlayerMovement");
                    instance = instanceContainer.AddComponent<PlayerMovement>();
                }
            }
            return instance;
        }
    }
    private static PlayerMovement instance;

    Rigidbody rb;
    public float moveSpeed = 5f;
    public Animator Anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (JoyStickMovement.Instance.joyVec.x != 0 || JoyStickMovement.Instance.joyVec.y != 0)
        {
            rb.velocity = new Vector3(JoyStickMovement.Instance.joyVec.x, 0, JoyStickMovement.Instance.joyVec.y) * moveSpeed;
            rb.rotation = Quaternion.LookRotation(new Vector3(JoyStickMovement.Instance.joyVec.x, 0, JoyStickMovement.Instance.joyVec.y));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("NextRoom"))
        {
            StageMgr.Instance.NextStage();
        }
        if (other.transform.CompareTag("HpBooster"))
        {
            PlayerHpbar.Instance.GetHpBoost();
        }
        if (other.transform.CompareTag("MeleeAtk"))
        {
            other.transform.parent.GetComponent<EnemyDuck>().meleeAtkArea.SetActive(false);
            PlayerHpbar.Instance.currentHp -= other.transform.parent.GetComponent<EnemyDuck>().damage * 2f;

            if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Dmg"))
            {
                Anim.SetTrigger("Dmg");
                Instantiate(EffectSet.Instance.PlayerDmgEffect, PlayerTargeting.Instance.AttackPoint.position, Quaternion.Euler(90, 0, 0));
            }
        }
    }
}
