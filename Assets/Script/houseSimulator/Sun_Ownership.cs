using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Sun_Ownership : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //オーサーシップの譲渡、W
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Sunオブジェクトの所有権の譲渡");
            photonView.RequestOwnership();

        }

    }
}
