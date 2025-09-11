using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Furniture_Ownership : MonoBehaviourPunCallbacks
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void TransOwnership()
    {
        //家具をクリックした瞬間、オーナーシップが変わる
        photonView.RequestOwnership();
    }

}