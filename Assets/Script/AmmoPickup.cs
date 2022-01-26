using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{

    [SerializeField] int ammoAmount = 5; //このアイテムを取ることによって何個弾が追加されるか
    [SerializeField] AmmoType ammoType; //「AmmoType.cs」から弾の種類を選んでいる

    private void OnTriggerEnter(Collider other) //他のトリガーの中に入ったとき 飛行船のゲームの「CollisionHandler.cs」のも同じものがある
    {
        if (other.gameObject.tag == "Player") //プレイヤータグがついたもの（今回は純粋にプレイヤー）に当たった時
        {
            //Debug.Log("プレイヤーがアイテムを取りました");

            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount); //「Ammo.cs」にある「IncreaseCurrentAmmo()」を取得し、アイテムを取ると弾が増えることを定義した「ammoType」は弾の種類　「ammoAmount」は弾の数
            Destroy(gameObject); //オブジェクトがなくなる（アイテムがプレイヤーに取られるっていう演出）
        }
    }

}
