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
        //house_miniの名前の同期処理、これをしないとゲスト側の家の名前が被る
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("NameHouseToMini", RpcTarget.Others);
        }

        //マスタークライアントのみ、今の状況を他のプレイヤー全員に送る
        //この処理がないと、今の状況を途中参加者に反映できない
        if (PhotonNetwork.IsMasterClient)
        {
            //家の外壁の情報、照明の情報を送信して他のプレイヤーに反映
            Debug.Log("他のプレイヤーが参加しました。今の状況を反映させます。");
            PhotonView photonView = PhotonView.Get(this);
            foreach (PhotonView view in PhotonNetwork.PhotonViews)
            {
                GameObject obj = view.gameObject;
                //照明の情報を送信
                if(obj.CompareTag("lighting"))
                {
                    Light light = obj.GetComponent<Light>();
                    //照明の情報を取得
                    LightingInfo lighting = new LightingInfo();
                    lighting.name = obj.name;
                    lighting.enabled = light.enabled;
                    lighting.intensity = light.intensity;
                    //JSONに変換
                    string jsonData = JsonUtility.ToJson(lighting);               
                    photonView.RPC("MakeCurrentLighting", RpcTarget.Others, jsonData);
                }
                //家の外壁の情報を送信
                if(obj.CompareTag("outerWall"))
                {
                    //家の外壁の情報を取得
                    OuterWallInfo outerWall = new OuterWallInfo();
                    outerWall.name = obj.name;
                    string materialName = obj.GetComponent<Renderer>().material.name;
                    outerWall.materialName = materialName.Replace(" (Instance)", "");
                    //JSONに変換
                    string jsonData = JsonUtility.ToJson(outerWall);
                    photonView.RPC("MakeCurrentOutWall", RpcTarget.Others, jsonData);
                }
            }
        }
    }

    [PunRPC]
    public void NameHouseToMini()
    {
        //名前が同期できていないため、_miniをつけるように同期する
        Environment_Creator.NameHouseToMini();
    }
    
    [PunRPC]
    public void MakeCurrentLighting(string jsonData)
    {
        //JSONをC#のオブジェクトに変換
        LightingInfo lighting = JsonUtility.FromJson<LightingInfo>(jsonData);
        //ネットワークオブジェクトの中からlightingを手に入れて反映
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            //名前が被るとうまく反映できなくなるので注意
            if (obj.CompareTag("lighting") && obj.name == lighting.name)
            {
                Light light = obj.GetComponent<Light>();
                light.name = lighting.name;
                light.enabled = lighting.enabled;
                light.intensity = lighting.intensity;
            }
        }

    }

    [PunRPC]
    public void MakeCurrentOutWall(string jsonData)
    {
        //JSONをC#のオブジェクトに変換
        OuterWallInfo outerWall = JsonUtility.FromJson<OuterWallInfo>(jsonData);
        //ネットワークオブジェクトの中からexteriorを手に入れて反映
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            //名前が被るとうまく反映ができなくなるので注意
            if (obj.CompareTag("outerWall") && obj.name == outerWall.name)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                obj.name = outerWall.name;
                // Resourcesフォルダ内のマテリアルをロード
                Material material = Resources.Load<Material>("Materials/"+ outerWall.materialName);
                // ロードしたマテリアルをオブジェクトに適用
                renderer.material = material;
            }
        }

    }
}