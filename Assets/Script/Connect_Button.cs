using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Connect_Button : MonoBehaviour
{
    public TMP_InputField inputField_roomName;
    public TMP_InputField inputField_directoryName;

    // Start is called before the first frame update
    void Start()
    {
        //初期のルーム名をSampleRoomに設定
        inputField_roomName.text = "SampleRoom";
        //初期のセーブデータ名をSampleDirectoryに設定;
        inputField_directoryName.text = "SampleDirectory";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Connect()
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
        Debug.Log("入力されたルーム名:" + roomName);
        Debug.Log("入力されたセーブデータ名:" + directoryName);
        //mainsceneの読み込み
        Debug.Log("MainSceneへの移行");
        SceneManager.LoadScene("MainScene");
    }
}
