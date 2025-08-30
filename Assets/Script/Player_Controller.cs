using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;



// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
public class Player_Controller : MonoBehaviourPunCallbacks
{
    private int createrID;
    //public GameObject object1;カメラ用
    void Start()
    {
        createrID = photonView.CreatorActorNr;
    }
    private void Update()
    {
        // 自身のオブジェクト
        if (photonView.IsMine)
        {
            //移動処理
            var input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
            transform.Translate(6f * Time.deltaTime * input.normalized);

            //オブジェクトの生成処理、スペースキー
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var localPlayer = PhotonNetwork.LocalPlayer;
                var position = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
                PhotonNetwork.Instantiate("Sphere", position, Quaternion.identity);
            }

            //ルームからの退出処理、エンターキー
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //マスタークライアントのみ
                if (PhotonNetwork.IsMasterClient)
                {
                    //データのセーブ処理
                    saveObjectData();
                    //プレイヤー全員をキック処理
                    kickOtherAllPlayers();
                }
                PhotonNetwork.LeaveRoom();
            }


        }
    }

    //他の人が退出した時に、このネットワークオブジェクトを破棄
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        //他の人が退出すると、所有権がマスタークライアントに移るため、マスタークライアントが削除できる
        //自分がマスタークライアントで、マスタークライアント以外の生産者だったら、プレイヤーオブジェクトを削除
        if (PhotonNetwork.IsMasterClient && this.createrID != 1)
        {
            Debug.Log(otherPlayer.ActorNumber);
            Debug.Log(photonView.ControllerActorNr);
            PhotonNetwork.Destroy(this.gameObject);
        }
    }


    //自分が退出した時の処理
    public override void OnLeftRoom()
    {
        Debug.Log("部屋から退出しました");
        PhotonNetwork.Disconnect();
        Debug.Log("TitleSceneへ戻ります");
        SceneManager.LoadScene("TitleScene");
    }

    private void kickOtherAllPlayers()
    {
        Debug.Log("マスタークライアントのみ、他のプレイヤーのキック処理開始");
        //自分以外のプレイヤーオブジェクトを取得し、キック処理
        var otherPlayers = PhotonNetwork.PlayerListOthers;
        for (int i = 0; i < otherPlayers.Length; i++)
        {
            PhotonNetwork.CloseConnection(otherPlayers[i]);
            Debug.Log(otherPlayers[i] + "をキックしました");
        }
        Debug.Log("他のプレイヤーのキック処理完了");
    }

    private void saveObjectData()
    {
        Debug.Log("マスタークライアントのみ、データの書き込み処理開始");

        //データの書き込み処理をここに書く

        Debug.Log("データの書き込み処理完了");
    }
}
