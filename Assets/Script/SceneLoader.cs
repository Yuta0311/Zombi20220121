using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//ここで「SceneManagement」を使うという定義をする

public class SceneLoader : MonoBehaviour
{

    public void ReloadGame() //最初に戻る的な
    {
        //一番上で「SceneManagement」を定義しないといけない　　using UnityEngine.SceneManagement;
        //ゲームをリロードする（最初に戻る）
        SceneManager.LoadScene(0);
        Time.timeScale = 1; //リロードした時に、再度ゲームを初めに戻すためのコード
    }

    public void QuitGame()
    {
        //Unity内で「Play Again Button」内の「Button」メニューの「On Click」より＋を押し、「Game Session」を追加！その後、ここで定義した「Application.Quit();」を↓を押して選択して追加する！　同じ方法で「Quit Button」内にも設定する
        //ゲームを離れる的な
        Application.Quit();
    }

}
