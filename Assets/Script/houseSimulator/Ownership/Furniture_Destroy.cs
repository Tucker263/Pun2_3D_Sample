using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Furniture_Destroy : MonoBehaviourPunCallbacks
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void Destroy()
    {
        //処理がうまくいかない
        Debug.Log(Input.GetMouseButton(1));
        //家具を右クリックした瞬間、オーナーシップが変わってオブジェクトを破棄
        if (Input.GetMouseButton(1))
        {
            photonView.RequestOwnership();
            Debug.Log("破棄");
        }
    }

}