using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RPC_Lighting_Itensity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //照明の明るく処理、Y
        if (Input.GetKeyDown(KeyCode.Y))
        {

            Debug.Log("照明の明るくする処理");
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("BrightLighting", RpcTarget.All);

        }

        //照明の暗く処理、T
        if (Input.GetKeyDown(KeyCode.T))
        {

            Debug.Log("照明の暗くする処理");
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("DarkLighting", RpcTarget.All);

        }

    }


    [PunRPC]
    public void BrightLighting()
    {
        Light light = GetComponent<Light>();

        //明るさの範囲を制限 0～6
        light.intensity = Mathf.Clamp(light.intensity, 0f, 6f);

        float speed = 0.4f;
        light.intensity += speed;
    }


    [PunRPC]
    public void DarkLighting()
    {
        Light light = GetComponent<Light>();
        //明るさの範囲を制限 0～6
        light.intensity = Mathf.Clamp(light.intensity, 0f, 6f);

        float speed = 0.4f;
        light.intensity -= speed;
    }
}
