using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RPC_Exterior_Material : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //マテリアルの変更処理、L
        if (Input.GetKeyDown(KeyCode.L))
        {

            Debug.Log("マテリアルの変更処理： ");
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("ChangeMaterial", RpcTarget.AllBuffered, "Red");

        }

        //マテリアルの変更処理、K
        if (Input.GetKeyDown(KeyCode.K))
        {

            Debug.Log("マテリアルの変更処理： ");
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("ChangeMaterial", RpcTarget.AllBuffered, "Blue");

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
