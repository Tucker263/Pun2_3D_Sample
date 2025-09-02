using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RPC_Lighting_OnOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //照明のオンオフ処理、スペース
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Debug.Log("照明のオンオフ処理");
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("OnOffLight", RpcTarget.All);

        }
    }

    [PunRPC]
    public void OnOffLight()
    {
        Light light = GetComponent<Light>();
        light.enabled = !light.enabled;
    }
}
