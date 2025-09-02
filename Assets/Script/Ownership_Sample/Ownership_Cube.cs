using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Ownership_Cube : MonoBehaviourPunCallbacks
{
    void Start()
    {

    }

    void Update()
    {
        //エンターキー、オーサーシップの譲渡
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Cubeオブジェクトの所有権の譲渡");
            photonView.RequestOwnership();

        }
    }

}