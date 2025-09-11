using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Furniture_Controller : MonoBehaviourPunCallbacks
{
    Transform furniture_tf;
    void Start()
    {
        furniture_tf = GetComponent<Transform>();
    }

    void Update()
    {

    }

    //家具をドラッグ中、家具が移動する
    public void Move()
    {
        //家具をクリックした瞬間、オーナーシップが変わる
        photonView.RequestOwnership();

        Vector3 mousePos = Input.mousePosition;
        //奥行指定、カメラから20ユニット先
        mousePos.z = 20.0f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        furniture_tf.position = worldPos;

    }

}