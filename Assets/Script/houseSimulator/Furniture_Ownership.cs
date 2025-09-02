using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Furniture_Ownership: MonoBehaviourPunCallbacks
{
    void Start()
    {

    }

    void Update()
    {
        //オーサーシップの譲渡,E
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Furnitureの所有権の譲渡");
            photonView.RequestOwnership();

        }
    }

}