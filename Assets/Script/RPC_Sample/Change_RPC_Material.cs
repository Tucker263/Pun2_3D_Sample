using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Change_RPC_Material : MonoBehaviourPunCallbacks
{
    void Start()
    {

        
    }

    void Update()
    {
        //マテリアルの変更処理、P
        if (Input.GetKeyDown(KeyCode.P))
        {
                
            Debug.Log("マテリアルの変更処理： ");
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("ChangeMaterial", RpcTarget.All, "Red");

        }

        //マテリアルの変更処理、O
        if (Input.GetKeyDown(KeyCode.O))
        {
                
            Debug.Log("マテリアルの変更処理： ");
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("ChangeMaterial", RpcTarget.All, "Blue");

        }
    }

    [PunRPC]
    public void ChangeMaterial(string color)
    {
        Debug.Log("マテリアルを変更しました: " + color);

        Renderer renderer = GetComponent<Renderer>();

        // マテリアルの色を変更
        if (renderer != null)
        {
            if(color == "Red")
            {
                renderer.material.color = Color.red; // 赤色に変更
            }
            if(color == "Blue")
            {
                renderer.material.color = Color.blue; // 青色に変更
            }
        }

    }
    
}