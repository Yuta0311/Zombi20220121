using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    //武器を切り替えるために「Player」の下の「Weapons」に紐付けする

    [SerializeField] int currentWeapon = 0; //この数字は上の武器から０、１、２、３と変わっていく　「０」にすると一番上の武器が選択されるようになっている

    void Start()
    {
        SetWeaponActive(); //最初から「SetWeaponActive」を定義するって意味　
    }

    void Update()
    {
        int previousWeapon = currentWeapon;

        ProcessKeyInput(); //キーボードにより武器が変更される
        ProcessScrollWheel(); //マウスのスクロールによって武器が変わる　マックでは使わないかもね

        if (previousWeapon != currentWeapon)
        {
            SetWeaponActive();
        }
    }

    private void SetWeaponActive() //切り替えのシステムを構築「currentWeapon」上で数字を指定すると武器が変わる！「０」にすると一番上の武器が選択されるようになっている
    {
        int weaponIndex = 0;

        foreach (Transform weapon in transform)
        {
            if (weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }

    }

    private void ProcessKeyInput() //キーボードにより武器が変更される（ここでは数字の１２３を選択すると武器が変わる）
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
        }
    }

    private void ProcessScrollWheel() //マウスのスクロールによって武器が変わる　マックでは使わないかもね
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0) //マウススクロールを上に回転させると変わる
        {
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)　//マウススクロールを下に回転させると変わる
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
        }
    }

    
}
