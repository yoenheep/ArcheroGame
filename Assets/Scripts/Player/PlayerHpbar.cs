using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpbar : MonoBehaviour
{
    public static PlayerHpbar Instance // ΩÃ±€≈Ê
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerHpbar>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("PlayerHpbar");
                    instance = instanceContainer.AddComponent<PlayerHpbar>();
                }
            }
            return instance;
        }
    }
    private static PlayerHpbar instance;

    public Transform player;
    public Slider hpBar;
    public float maxHp;
    public float currentHp;

    public GameObject HpLineFolder;
    float unitHp = 200f;

    void Update()
    {
        transform.position = player.position;
        hpBar.value = currentHp / maxHp;
        //playerHpText.text = "" + currentHp;
    }

    public void GetHpBoost()
    {
        maxHp += 150;
        //currentHp += 150;
        float scaleX = (1000f / unitHp) / (maxHp / unitHp);
        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);

        foreach(Transform child in HpLineFolder.transform)
        {
            child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
        }

        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);
    }
}
