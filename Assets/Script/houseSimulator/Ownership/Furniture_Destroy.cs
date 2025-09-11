using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Furniture_Destroy : MonoBehaviourPunCallbacks
{
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f;
    void Start()
    {

    }

    void Update()
    {

    }

    public void Destroy()
    {

        //家具をダブルクリックした時、オーナーシップが変わってオブジェクトを破棄
        float timeSinceLastClick = Time.time - lastClickTime;
        if (timeSinceLastClick <= doubleClickThreshold)
        {
            photonView.RequestOwnership();
            PhotonNetwork.Destroy(this.gameObject);
        }

        lastClickTime = Time.time;
    }

}