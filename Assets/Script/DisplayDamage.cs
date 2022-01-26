using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    //「Player」に紐付けする
    [SerializeField] Canvas impactCanvas; //どのキャンバスを表示するか入力
    [SerializeField] float impactTime = 0.3f;　//何秒止まるか（今回は何秒表示されるか）

    void Start()
    {
        impactCanvas.enabled = false;
    }

    public void ShowDamageImpact() //これを「EnemyAttack.cs」使う用に定義している
    {
        StartCoroutine(ShowSplatter()); ////「StartCoroutine」はコルーチンという　Weapon.csに説明がある　今回は攻撃が当たったら傷がある画像が表示されるてきな
    }

    IEnumerator ShowSplatter()
    {
        impactCanvas.enabled = true;
        yield return new WaitForSeconds(impactTime);
        impactCanvas.enabled = false;
    }
}
