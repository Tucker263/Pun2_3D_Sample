using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;



// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
public class Avator_Controller : MonoBehaviourPunCallbacks
{
    public int createrID;
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
            var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            transform.Translate(6f * Time.deltaTime * input.normalized);

        }
    }
}