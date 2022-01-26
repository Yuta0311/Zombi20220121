using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson; //UnityStandardAssetsを使うために

//ここでは武器に紐付けしている

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;

    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = .5f;

    //武器に紐付けすることでこれだけ増やすことになった
    [SerializeField] RigidbodyFirstPersonController fpsController; //これで「Player」を選択し、UnityStandardAssetsの「RigidbodyFirstPersonController」を使えるようにしている

    /*
     * RigidbodyFirstPersonController fpsController;  //UnityStandardAssetsの「RigidbodyFirstPersonController」を設定するために定義　ここではズームした際に、通常時よりも動きが小さくなるようにしている
    */

    bool zoomedInToggle = false;

    private void OnDisable() //ズームアウトしたまま武器を変えてもズームアウトしたままなのでそのバグを直す
    {
        ZoomOut();
    }

    /* これは「Player」に紐づいていた時に使っていたが、武器それぞれの能力をつけるために、各武器に紐づけるようになったため使えなくなった
     * その代わり、武器の親カテ？には「Player」があり、「RigidbodyFirstPersonController」と連携するようにしないといけない
    private void Start()
    {
        fpsController = GetComponent<RigidbodyFirstPersonController>(); //「RigidbodyFirstPersonController」が常に変更できるように定義
    }
    */

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) //右クリック？二本指でクリックした時
        {
            if (zoomedInToggle == false)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }
    }

    private void ZoomIn()
    {
        zoomedInToggle = true; //zoomedInToggleがtrueの時はズームさせる
        fpsCamera.fieldOfView = zoomedInFOV; //fieldOfViewは公式（ズームさせたりなど）

        fpsController.mouseLook.XSensitivity = zoomInSensitivity; //ズームした時に動きが小さくなるようにしている
        fpsController.mouseLook.YSensitivity = zoomInSensitivity; //ズームした時に動きが小さくなるようにしている
    }

    private void ZoomOut()
    {
        zoomedInToggle = false; //zoomedInToggleがfalseの時はfpsCameraで代入した（ここではMainCamera）が通常時（zoomedOutFOV）のまま
        fpsCamera.fieldOfView = zoomedOutFOV;

        fpsController.mouseLook.XSensitivity = zoomOutSensitivity; //ズームではない時、通常のスピードで動くようにしている
        fpsController.mouseLook.YSensitivity = zoomOutSensitivity; //ズームではない時、通常のスピードで動くようにしている
    }
}
