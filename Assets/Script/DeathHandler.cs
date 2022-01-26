using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;

    private void Start()
    {
        gameOverCanvas.enabled = false; //ゲームオーバーキャンバスをデフォルトでは無効
    }

    public void HandleDeath()
    {
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        FindObjectOfType<WeaponSwitcher>().enabled = false; //死んだ時に「WeaponSwitcher.cs」を無効にして、武器の変更ができないようにしている
        FindObjectOfType<Weapon>().enabled = false; //死んだ時に「WeaponSwitcher.cs」を無効にして、武器の変更ができないようにしている
    }
}

