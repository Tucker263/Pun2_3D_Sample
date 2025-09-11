using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
public class Exit_Button : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void Exit()
    {
        //マスタークライアントのみ
        if (PhotonNetwork.IsMasterClient)
        {
            //データのセーブ処理
            Debug.Log("マスタークライアントのみ、データのセーブ処理開始");
            StreamFile_Manager.Save();
            Debug.Log("データのセーブ処理完了");
            //プレイヤー全員をキック処理
            KickOtherAllPlayers();
        }
        PhotonNetwork.LeaveRoom();
    }


    //他の人が退出した時に、このavator(ネットワークオブジェクト)を破棄
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        List<GameObject> avatorList = NetworkObject_Search.GetListFromTag("avator");
        //avatorの配列から退出するプレイヤーのavatorを削除
        foreach (GameObject avator in avatorList)
        {
            Avator_Controller avator_Controller = avator.GetComponent<Avator_Controller>();
            //他の人が退出すると、所有権がマスタークライアントに移るため、マスタークライアントが削除できる
            //自分がマスタークライアントで、マスタークライアント以外の生産者だったら、プレイヤーオブジェクトを削除
            if (PhotonNetwork.IsMasterClient && avator_Controller.createrID != 1)
            {
                Debug.Log(otherPlayer.ActorNumber);
                Debug.Log(photonView.ControllerActorNr);
                PhotonNetwork.Destroy(avator);
            }

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

    private void KickOtherAllPlayers()
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
}
