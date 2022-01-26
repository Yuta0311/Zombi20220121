using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //これを追加してAIを使えるようにする

public class EnemyAI : MonoBehaviour
{

    //[SerializeField] Transform target; //誰を追いかけるか入力 いちいち全部の敵にプレイヤーを選択させるのはめんどくさいので下でTransform target;で定義するのに変更
    [SerializeField] float chaseRange = 5f; //どれくらいの距離で追いかけてくるか入力欄作成

    [SerializeField] float turnSpeed = 5f; //敵が追いかけてくる時にこっちを見るターンスピード


    NavMeshAgent navMeshAgent; //AIを使えるように定義
    float distanceToTarget = Mathf.Infinity; // Mathf.Infinity; が正の無限大？らしい笑
    bool isProvoked = false; //isProvokedは単語で挑発って意味

    EnemyHealth health; //EnemyHealth.csを取得　死んだ時のアクションの為　

    Transform target; //誰を追いかけるか定義（ここでは一律でプレイヤーにしている） いちいち全部の敵にプレイヤーを選択させるのはめんどくさいので下でTransform target;で定義するのに変更

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); //AIで敵を追いかけるシステム（navMeshAgent）
        health = GetComponent<EnemyHealth>(); //EnemyHealth.csを取得　死んだ時のアクションの為
        target = FindObjectOfType<PlayerHealth>().transform; //誰を追いかけるか定義（ここでは一律でプレイヤーにしている）
    }

    void Update()
    {
        if (health.IsDead())　//意味は死んだ時　//EnemyHealth.csを取得　死んだ時のアクションの為　よくわからんけどとりあえず死んだ時に、他のアニメーターのトリガーが外れている
        {
            enabled = false;
            navMeshAgent.enabled = false; //死んだ時に敵のメニューにある追いかけてくるシステム「navMeshAgent」のチェックが外れる
        }
        else　//死んでない時
        {
            distanceToTarget = Vector3.Distance(target.position, transform.position); //更新時に全てのフレームでプレイヤーとの距離を測定している
            if (isProvoked)//下がisProvoked = true;になると発動される
            {
                EngageTarget();
            }
            else if (distanceToTarget <= chaseRange)//プレイヤーとの距離がもし上で入力した距離（５）より短くなった場合
            {
                isProvoked = true; //今回はプレイヤーが設定している「５」より近づいた場合、定義している「isProvoked」がtrueとなり、上のif文が発動！
                navMeshAgent.SetDestination(target.position); //「SetDestination();」で移動するって意味　今回は指定したプレイヤーをAI（navMeshAgent）が追いかける感じ
            }
        }

    }

    public void OnDamageTaken() //遠くからダメージを受けた時 ここでpublicにする理由は「EnemyHealth.cs」で「OnDamageTaken()」を使っている　これによりダメージを受けると「isProvoked = true;」になることが指示される
    {
        Debug.Log("ちょっと、、攻撃を受けてるんですけど、、");
        isProvoked = true; //挑発定義が有効になる
    }

    private void EngageTarget()
    {
        FaceTarget(); //敵がこっちを見る

        if (distanceToTarget >= navMeshAgent.stoppingDistance) //「distanceToTarget」はプレイヤーとの距離（上で定義している）　、　「navMeshAgent」はUnity内のEnemyの設定　、　「stoppingDistance」はnavMeshAgent内の設定　初期は０なので１にする　すると敵が距離１になるまで追いかけてくるようになる
        {
            ChaseTarget(); //敵が「stoppingDistance」で設定した距離より離れてたら永遠にずっと追いかけてくる
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance) //上記と同じ意味
        {
            AttackTarget(); //敵が「stoppingDistance」で設定した距離より近くなったら攻撃してくる
        }
    }



    private void ChaseTarget() //追いかけてくる時
    {
        GetComponent<Animator>().SetBool("attack", false); //アニメーターの「attack」トリガーのチェックを外す　これにより一回攻撃してきた敵と離れると再度追いかけてくるイメージ
        GetComponent<Animator>().SetTrigger("move"); //アニメーターの「move」トリガーにチェックを入れる
        navMeshAgent.SetDestination(target.position); //「SetDestination();」で移動するって意味　今回は指定したプレイヤーをAI（navMeshAgent）で追いかける感じ
    }

    private void AttackTarget() //攻撃してくる時
    {
        GetComponent<Animator>().SetBool("attack", true); //アニメーターの「attack」トリガーにチェックを入れる
        //Debug.Log(name + "敵が自分に攻撃してるよ〜ん" + target.name);
    }


    private void FaceTarget()//敵がこっちを見て追いかけてくる方法
    {
        //「Animator」の「Apply Root Motion」にチェック入れないとアニメーターに邪魔されて見てくれないので必ずチェックする
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

    }

    void OnDrawGizmosSelected() //爆発範囲的な！Unity.docのテンプレートから取ってきた　視覚的に見えるようにしている
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange); //どれくらいの範囲かは「chaseRange」で上で入力している
    }
}
