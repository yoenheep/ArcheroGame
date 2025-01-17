using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    public Slider hpSlider;
    public Transform enemy;
    public float maxHp = 1000f;
    public float currentHp = 1000f;

    public Slider BackHpSlider;
    bool backHpHit;

    // Update is called once per frame
    void Update()
    {
        transform.position = enemy.position;
        hpSlider.value = Mathf.Lerp(hpSlider.value, currentHp / maxHp, Time.deltaTime * 5f);

        if(backHpHit )
        {
            BackHpSlider.value = Mathf.Lerp(BackHpSlider.value, hpSlider.value, Time.deltaTime * 10f);

            if(hpSlider.value >= BackHpSlider.value - 0.01f)
            {
                backHpHit = false;
                BackHpSlider.value = hpSlider.value;
            }
        }
    }

    public void Dmg()
    {
        Debug.Log("EnemyDmg");

        currentHp -= 300f;
        Invoke("BackHpFun", 0.5f);
    }

    void BackHpFun()
    {
        backHpHit = true;
    }
}
