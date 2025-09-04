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
        //オーナーシップの譲渡,E
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Furnitureの所有権の譲渡");
            photonView.RequestOwnership();

        }

        float speed = 5.0f;
        if (Input.GetKey (KeyCode.G)) 
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey (KeyCode.H)) 
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

    }

}