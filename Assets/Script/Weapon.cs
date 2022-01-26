using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;//弾の数を表示するため

public class Weapon : MonoBehaviour
{

    [SerializeField] Camera FPCamera; //どこから発射するかを指定している「今回はメインカメラから」
    [SerializeField] float range = 100f; //発射圏内
    [SerializeField] float damage = 30f; //一回撃つごとの攻撃力
    [SerializeField] ParticleSystem muzzleFlash; //弾を撃った時のアニメーション
    [SerializeField] GameObject hitEffect; //弾が打った先のオブジェクトのアニメーション

    [SerializeField] Ammo ammoSlot; //ここでは「Ammo」はPlayと紐付けする設定にする（弾の数を判定するためのもの）
    [SerializeField] AmmoType ammoType; //「AmmoType.cs」から弾の種類を選ぶ入力欄

    [SerializeField] float timeBetweenShots = 0.5f; //撃つまでのタイムラグ

    [SerializeField] TextMeshProUGUI ammoText;　//弾の数を表示させる入力欄　ここではキャンバスを選択する

    bool canShoot = true; //撃つまでのタイムラグを設定する時に使っている


    void Update()
    {

        DisplayAmmo(); //弾を表示する

        //
        //if (Input.GetButton("Fire1"))//Project Settingを見たらわかる　ここではマウスをクリックすると撃つ
        //if (Input.GetButton("Fire1") && canShoot == true)
        if (Input.GetMouseButtonDown(0) && canShoot == true) //上の書き方でも一緒
        {
            //Shoot();
            StartCoroutine(Shoot()); //「StartCoroutine」はコルーチンという　意味は「中断できる処理のまとまり」「数秒後に何か処理を行いたいときや非同期のような処理」
                                     //　コルーチンを実行するにはStartCoroutineメソッドを使います。
                                     //　使うには下記で「IEnumerator」を定義しなくてはいけない（公式みたいなもの）
                                     //　例文は35行目に書いておく
        }
    }

    /*
    IEnumerator 関数名()
    {
        //ここに処理を書く

        //1フレーム停止
        yield return new WaitForSeconds(止めたい秒数);

        //ここに再開後の処理を書く
    }
    */

    private void DisplayAmmo()//弾を表示する
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    //private void Shoot()
    IEnumerator Shoot()
    {
        //PlayMuzzleFlash(); //弾を撃った時のアニメーション
        //ProcessRaycast(); //弾を撃った判定

        canShoot = false; //タイムラグがある時にfalseになり弾が撃てなくなる

        if (ammoSlot.GetCurrentAmmo(ammoType) > 0) //もし弾が０より多かった場合（発砲できる）「Ammo.cs」で定義　 「ammoType」は弾の種類
        {
            PlayMuzzleFlash(); //弾を撃った時のアニメーション
            ProcessRaycast(); //弾を撃った判定
            ammoSlot.ReduceCurrentAmmo(ammoType); //弾を1回撃ったごとに弾が減っていくシステム「Ammo.cs」で定義　　「ammoType」は弾の種類
        }

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;

    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play(); //弾を撃った時に設定した（muzzleFlashの入力欄）アニメーションが流れる
    }

    private void ProcessRaycast()
    {
        RaycastHit hit; //調べたら「RaycastHit」レイキャストによる情報を得るための構造体

        //raycastとは指定した場所から透明な光線を打ち、光線に当たったオブジェクトの情報を取得する機能
        //Physics.Raycast(Vector3 origin(rayの開始地点), Vector3 direction(rayの向き),RaycastHit hitInfo(当たったオブジェクトの情報を格納), float distance(rayの発射距離), int layerMask(レイヤマスクの設定));
        //必要な要素はorigin(rayの開始地点)とdirection(rayの向き)のみ
        //Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range); //拳銃から放った弾が当たった構造体（オブジェクト）の情報を得る

        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);

            //Debug.Log("私は撃つよ" + hit.transform.name); //hit.transform.nameはレーザーが当たったものの名前　→　空に当たったらエラーが出る

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>(); //EnemyHealth.csからメソッドを取得？
            if (target == null) return; //敵のオブジェクトではない場合無効？的な！これがないと敵以外を撃つとエラーが出る！
            target.TakeDamege(damage);  //一回撃つごとの攻撃力（EnemyHealth.cs）と連携している
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal)); //位置を指定しなくても真ん中に爆発アニメーションが表示された？
        Destroy(impact, 0.1f); //撃ったらファイルができるが1秒後にすぐに削除されるようにしている
    }
}
