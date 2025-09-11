using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Connect_Manager : MonoBehaviourPunCallbacks
{
    public Light directionalLight;
    public void Start()
    {
        Debug.Log("Connect_Managerのコンストラクタ処理");
        //どのクライアントも、キックされる処理ができるように設定
        PhotonNetwork.EnableCloseConnection = true;
        //オフラインモードかオンラインモードか、どちらか設定
        PhotonNetwork.OfflineMode = Config.isOfflineMode;
        //オンラインモードの場合
        if (!PhotonNetwork.OfflineMode)
        {
            // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
            Debug.Log("オンラインモード");
            Debug.Log("マスターサーバーへの接続開始");
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("オフラインモード");
            //元からあるライトをオフに
            directionalLight.enabled = false;
        }
    }

    public void Update()
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
        string roomName = Config.roomName;
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
        //ニックネームを、ホスト、ゲストに分ける
        PhotonNetwork.NickName = PhotonNetwork.IsMasterClient ? "Host" : "Guest";

        //マスタークライアントのみ、セーブデータを読み込み処理
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("マスタークライアントのみ、データのロード処理開始");
            StreamFile_Manager.Load();
            Debug.Log("データのロード処理完了");

        }
        else
        {
            //ゲストは、ランダムな座標に自身のアバターを生成
            var position = new Vector3(Random.Range(-8, 8), 2, Random.Range(-17, -10));
            PhotonNetwork.Instantiate("avator", position, Quaternion.identity);
        }
        //元からあるライトをオフに
        directionalLight.enabled = false;

    }

    //ルームへの参加が失敗した時に呼ばれるコールバック
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("ルームへの参加が失敗しました");
        PhotonNetwork.Disconnect();
        Debug.Log("TitleSceneへ戻ります");
        SceneManager.LoadScene("TitleScene");
    }


    //自分以外のプレイヤーがルームに入室した時に呼ばれるコールバック
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //自分がマスタークライアントの時、全クライアントに向けて最新の状態を同期する処理を行う
        //この処理がないと、今の状況を途中参加者に反映できない
        GameObject obj = NetworkObject_Search.GetObjectFromName("LatestState_Synchronize");
        LatestState_Synchronize latestState_Synchronize = obj.GetComponent<LatestState_Synchronize>();
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("他プレイヤーが参加しました。最新の状況を反映させます");
            latestState_Synchronize.Synchronize();
            Debug.Log("他プレイヤーの同期完了");
        }

    }

}