using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f; //自分のHP

    public void TakeDamege(float damage)
    {
        hitPoints -= damage; //Weaponで武器の攻撃力を設定している
        if (hitPoints <= 0) //自分のHPがなくなり、死んだ時
        {
            // Debug.Log("私は力尽きて死にました");
            GetComponent<DeathHandler>().HandleDeath(); //死んだ時に「DeathHandler.cs」内の「HandleDeath();」を呼び出してゲームオーバー画面を出す
        }
    }
}
