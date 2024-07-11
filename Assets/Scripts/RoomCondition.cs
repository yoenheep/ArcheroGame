using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCondition : MonoBehaviour
{
    public static RoomCondition Instance // ΩÃ±€≈Ê
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RoomCondition>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("RoomCondition");
                    instance = instanceContainer.AddComponent<RoomCondition>();
                }
            }
            return instance;
        }
    }
    private static RoomCondition instance;

    List<GameObject> MonsterListInRoom = new List<GameObject>();
    public bool playerInThisRoom = false;
    public bool isClearRoom = false;

    void Update()
    {
        if (playerInThisRoom)
        {
            if(MonsterListInRoom.Count<= 0 && !isClearRoom)
            {
                isClearRoom = true;
                Debug.Log("Clear");
            }
        }
    }

    void PlayerEnterRoom()
    {
        playerInThisRoom = true;
        PlayerTargeting.Instance.MonsterList = new List<GameObject>(MonsterListInRoom);
        Debug.Log("Enter New Room, Mob Count : " + PlayerTargeting.Instance.MonsterList.Count);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("PlayerEnterRoom", 0.1f);
        }
        if (other.CompareTag("Monster"))
        {
            MonsterListInRoom.Add(other.gameObject);
            Debug.Log("Mob name ;" + other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInThisRoom= false;
            PlayerTargeting.Instance.MonsterList.Clear();
        }
        if (other.CompareTag("Monster"))
        {
            MonsterListInRoom.Remove(other.gameObject);
        }
    }
}
