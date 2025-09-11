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
