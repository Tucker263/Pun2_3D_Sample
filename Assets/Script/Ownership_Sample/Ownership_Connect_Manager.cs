using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Ownership_Connect_Manager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        Debug.Log("コンストラクタ処理");
        //プレイヤー自身の名前を"Player"に設定
        PhotonNetwork.NickName = "Player";
        Debug.Log("マスターサーバーに接続します");
        //マスターサーバーに接続
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
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
        Debug.Log("roomName" + "への接続開始");
        PhotonNetwork.JoinOrCreateRoom("roomName", roomOptions, TypedLobby.Default);
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
        var position = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
        PhotonNetwork.Instantiate("Ownership_Player", position, Quaternion.identity);
    }    
}