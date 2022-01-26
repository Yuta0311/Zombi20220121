using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    //「Player」と紐付けし、「AmmoType.cs」で定義した色んな弾（今回は３種類）の数を決めている

    [SerializeField] AmmoSlot[] ammoSlots;
    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType; //「AmmoType.cs」から弾の種類を取得
        public int ammoAmount;
    }

    public int GetCurrentAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoAmount;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount--;
    }

    public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoAmount) //「AmmoPickup.cs」と連動して、アイテムをとると弾が増えるコードを書いている
    {
        GetAmmoSlot(ammoType).ammoAmount += ammoAmount;
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot slot in ammoSlots)
        {
            if (slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        return null;
    }

    /* 元のコード
    //「Player」と紐付けし弾の数を決める
    [SerializeField] int ammoAmount = 10;

    public int GetCurrentAmmo()
    {
        return ammoAmount; //ammoAmountの数を常に取得
    }

    public void ReduceCurrentAmmo()
    {
        ammoAmount--; //１ずづ減っていく　ここでは「Weapon.cs」内にて定義し、発砲1回ごとに１ずつ弾が減っていくことになっている
    }
    */
}
