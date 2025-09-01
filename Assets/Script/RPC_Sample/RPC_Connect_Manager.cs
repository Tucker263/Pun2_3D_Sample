using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class RPC_Connect_Manager : MonoBehaviourPunCallbacks
{
    private void Start()
    {

        Debug.Log("Connect_Managerのコンストラクタ処理");
        //プレイヤー自身の名前を"Player"に設定
        PhotonNetwork.NickName = "Player";
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        Debug.Log("マスターサーバーへの接続開始");
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Update()
    {

    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターサーバーの接続成功");
        Debug.Log("ルームへの接続開始");
        PhotonNetwork.JoinOrCreateRoom("roomName", new RoomOptions(), TypedLobby.Default);
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
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        var position1 = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
        PhotonNetwork.Instantiate("RPC_Player", position1, Quaternion.identity);

        //光を生成
        var position2 = new Vector3(0, 1, 13);
        Quaternion q = Quaternion.Euler(90, 0, 0);
        PhotonNetwork.Instantiate("SpotLight", position2, q);
    }  
}