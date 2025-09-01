using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Change_RPC_Light : MonoBehaviourPunCallbacks
{
    void Start()
    {

        
    }

    void Update()
    {
        //ライトの変更処理、L
        if (Input.GetKeyDown(KeyCode.L))
        {
                
            Debug.Log("ライトの変更処理： ");
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("ChangeLight", RpcTarget.All, "Red");

        }

        //ライトの変更処理、K
        if (Input.GetKeyDown(KeyCode.K))
        {
                
            Debug.Log("ライトの変更処理： ");
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("ChangeLight", RpcTarget.All, "Blue");

        }

        //ライトのオンオフ、J
        if (Input.GetKeyDown(KeyCode.J))
        {            
            photonView.RPC("OnOffLight", RpcTarget.All, "Blue");
        }

    }

    [PunRPC]
    public void ChangeLight(string type)
    {
        Debug.Log("ライトを変更しました: " + type);

        Light light = GetComponent<Light>();

        //ライトを変更
        if (light != null)
        {
            if(type == "Red")
            {
                light.color = Color.red; // 赤色に変更
                light.type = LightType.Spot; //種類を変更
                light.intensity = Mathf.PingPong(Time.time, 2.0f); // 時間に応じて強度を変化
            }
            if(type == "Blue")
            {
                light.color = Color.blue; // 赤色に変更
                light.type = LightType.Point; //種類を変更
            }
        }

    }


    [PunRPC]
    public void OnOffLight(string type)
    {
        Light light = GetComponent<Light>();           
        light.enabled = !light.enabled;
    }
    
}