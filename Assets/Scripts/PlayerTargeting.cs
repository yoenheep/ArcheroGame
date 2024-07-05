using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    public static PlayerTargeting Instance // ΩÃ±€≈Ê
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerTargeting>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("PlayerTargeting");
                    instance = instanceContainer.AddComponent<PlayerTargeting>();
                }
            }
            return instance;
        }
    }
    private static PlayerTargeting instance;

    public bool getATarget = false;
    float currentDist = 0;
    float closetDist = 100f;
    float TargetDist = 100f;
    int closeDistIndex = 0;
    int TargetIndex = -1;
    public LayerMask layerMask;

    public float atkSpd = 1f;

    public List<GameObject> MonsterList = new List<GameObject>();

    public GameObject PlayerBolt;
    public Transform AttackPoint;

    void OnDrawGizmos()
    {
        if(getATarget)
        {
            for(int i = 0;i < MonsterList.Count; i++)
            {
                RaycastHit hit;
                bool isHit = Physics.Raycast(transform.position, MonsterList[i].transform.position - transform.position, out hit, 20f, layerMask);

                if(isHit && hit.transform.CompareTag("Monster"))
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawRay(transform.position, MonsterList[i].transform.position - transform.position);
            }
        }
    }

    void Update()
    {
        SetTarget();
        AtkTarget();
    }

    void Attack()
    {
        PlayerMovement.Instance.Anim.SetFloat("AttackSpd", atkSpd);
        Instantiate(PlayerBolt, AttackPoint.position, transform.rotation);
    }

    void SetTarget()
    {
        if(MonsterList.Count != 0)
        {
            currentDist = 0f;
            closeDistIndex = 0;
            TargetIndex = -1;

            for(int i = 0; i < MonsterList.Count; i++)
            {
                currentDist = Vector3.Distance(transform.position, MonsterList[i].transform.position);
                
                RaycastHit hit;
                bool isHit = Physics.Raycast(transform.position, MonsterList[i].transform.position - transform.position, out hit, 20f, layerMask);

                if (isHit && hit.transform.CompareTag("Monster"))
                {
                    if(TargetDist >= currentDist)
                    {
                        TargetIndex = i;
                        TargetDist = currentDist;
                    }
                }

                if(closetDist >= currentDist)
                {
                    closeDistIndex = i;
                    closetDist = currentDist;
                }
            }

            if (TargetIndex == -1)
            {
                TargetIndex = closeDistIndex;
            }
            closetDist = 100f;
            TargetDist = 100f;
            getATarget = true;
        }
    }

    void AtkTarget()
    {
        if (getATarget && !JoyStickMovement.Instance.isPlayerMoving)
        {
            transform.LookAt(new Vector3(MonsterList[TargetIndex].transform.position.x, transform.position.y, MonsterList[TargetIndex].transform.position.z));
            Attack();

            if (PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                PlayerMovement.Instance.Anim.SetBool("Idle", false);
                PlayerMovement.Instance.Anim.SetBool("Walk", false);
                PlayerMovement.Instance.Anim.SetBool("Attack", true);
            }
        }
        else if(JoyStickMovement.Instance.isPlayerMoving)
        {
            if (!PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                PlayerMovement.Instance.Anim.SetBool("Attack", false);
                PlayerMovement.Instance.Anim.SetBool("Idle", false);
                PlayerMovement.Instance.Anim.SetBool("Walk", true);
            }
        }
    }
}
