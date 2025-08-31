using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Launch_Button : MonoBehaviour
{
    public TMP_InputField inputField_roomName;
    public TMP_InputField inputField_directoryName;

    // Start is called before the first frame update
    void Start()
    {
        //初期のルーム名をSampleRoomに設定
        Config.roomName = "SampleRoom";
        inputField_roomName.text = Config.roomName;
        //初期のセーブデータ名をSampleDirectoryに設定;
        Config.directoryName = "SampleDirectory";
        inputField_directoryName.text = Config.directoryName;
        //初期のセーブデータを初めからにする
        Config.isInitialStart = true;
        //初期のモードをオンラインモードにする
        Config.isOfflineMode = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Launch()
    {
        //inputbuttonからルーム名を取得
        Config.roomName = inputField_roomName.text;
        string roomName = Config.roomName;
        //inputbuttonからセーブデータ名を取得
        Config.directoryName = inputField_directoryName.text;
        string directoryName = Config.directoryName;

        //入力が空欄だと動作しない処理
        if (roomName == "" || directoryName == "")
        {
            return;
        }

        //入力された値
        Debug.Log("---------------------------");
        Debug.Log("入力されたルーム名:" + roomName);
        Debug.Log("入力されたセーブデータ名:" + directoryName);
        Debug.Log("---------------------------");
        //mainsceneの読み込み
        Debug.Log("MainSceneへの移行します");
        SceneManager.LoadScene("MainScene");
    }
}
