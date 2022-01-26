using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //[SerializeField] Transform target; //攻撃する対照をセット　今回はプレイヤーを選択 下記に変更
    PlayerHealth target;

    [SerializeField] float damage = 40f;　//敵の攻撃力

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>(); //PlayerHealth.csからメソッドを取得？
    }

    public void AttackHitEvent()
    {
        if (target == null) return;

        target.TakeDamege(damage);  //一回攻撃してくるごとの攻撃力（PlayerHealth.cs）と連携している
        //Debug.Log("bang bang");

        target.GetComponent<DisplayDamage>().ShowDamageImpact();//「DisplayDamage.cs」と連携　これにより攻撃をしてきたときに、傷の画像が入るようになる
    }
}
