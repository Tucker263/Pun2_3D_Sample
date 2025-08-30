using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Connect_Button : MonoBehaviour
{
    public static string roomName;
    public TMP_InputField inputField_roomName;

    // Start is called before the first frame update
    void Start()
    {
        //初期のルーム名をSampleRoomに設定
        inputField_roomName.text = "SampleRoom";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Connect()
    {
        //inputbuttonから値を取得
        roomName = inputField_roomName.text;
        Debug.Log("入力されたroom名:" + roomName);
        //mainsceneの読み込み
        Debug.Log("MainSceneへの移行");
        SceneManager.LoadScene("MainScene");
    }
}
