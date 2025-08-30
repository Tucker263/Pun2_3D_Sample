using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Connect_Manager : MonoBehaviourPunCallbacks
{

    private void Start()
    {

        Debug.Log("Connect_Managerのstart処理");
        //プレイヤー自身の名前を"Player"に設定
        PhotonNetwork.NickName = "Player";
        //どのクライアントも、キックされる処理ができるように設定
        PhotonNetwork.EnableCloseConnection = true;
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        Debug.Log("マスターサーバへの接続開始");
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Update()
    {

    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターサーバーの接続成功");
        //参加可能人数を3人に設定
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 3;
        //プレイヤーが退出しても、ネットワークオブジェクトを消えないように設定
        roomOptions.CleanupCacheOnLeave = false;

        // roomNameというルームに参加する（ルームが存在しなければ作成して参加する）
        string roomName = Connect_Button.roomName;
        Debug.Log(roomName + "への接続開始");
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    //ルームが作成された時に呼ばれるコールバック
    public override void OnCreatedRoom()
    {
        Debug.Log("ルームの作成成功");
    }

    //ルームへの参加が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        Debug.Log("ルームへの参加成功");
        //マスタークライアントのみ、セーブデータを読み込み処理
        if (PhotonNetwork.IsMasterClient)
        {
            loadObjectData();
        }

        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        var position = new Vector3(Random.Range(1, 3), 1, Random.Range(1, 3));
        PhotonNetwork.Instantiate("Player", position, Quaternion.identity);
    }

    //ルームへの参加が失敗した時に呼ばれるコールバック
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("ルームへの参加が失敗しました");
        PhotonNetwork.Disconnect();
        Debug.Log("TitleSceneへ戻ります");
        SceneManager.LoadScene("TitleScene");
    }

    private void loadObjectData()
    {
        Debug.Log("マスタークライアントのみ、セーブデータの読み込み処理開始");
        //セーブデータのロード処理

        //マスタークライアントのみ、ルームオブジェクトを作成可能
        //地面などの変わらないものは、ルームオブジェクトにし、家などの変更するものはネットワークオブジェクトにする予定

        var position = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        PhotonNetwork.InstantiateRoomObject("SaveObject", position, Quaternion.identity);


        Debug.Log("セーブデータの読み込み処理完了");

    }
}