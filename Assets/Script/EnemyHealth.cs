using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f; //敵のHP

    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamege(float damage) 
    {
        //ゲームオブジェクトまたは子オブジェクトにあるすべての MonoBehaviour を継承したクラスにある methodName 名のメソッドを呼び出します。
        BroadcastMessage("OnDamageTaken"); //ここでは「EnemyAI.cs」の「OnDamageTaken()」と連動している　これによりダメージを受けた場合、「EnemyAI.cs」の「OnDamageTaken()」内にある「isProvoked = true;」になることが指示される

        hitPoints -= damage; //Weaponで武器の攻撃力を設定している
        if(hitPoints <= 0) //敵のHPがなくなり、死んだ時
        {
            //Destroy(gameObject); //これではゾンビが死んだ時に消えるだけ
            Die();
        }
    }

    private void Die() //「EnemyAI.cs」連携している　よーわからん
    {
        if (isDead) return;
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
    }
}
