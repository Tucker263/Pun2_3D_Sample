using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Light_Itensity_Change : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void Change()
    {
        Debug.Log("クリックされました。");
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("ChangeIntensity", RpcTarget.All);
        
    }


    [PunRPC]
    public void ChangeIntensity()
    {
        Light light = GetComponent<Light>();
        light.intensity = Light_SelectedState.intensity;

    }

}
