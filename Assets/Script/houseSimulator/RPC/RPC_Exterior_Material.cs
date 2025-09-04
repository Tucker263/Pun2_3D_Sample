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
            photonView.RPC("ChangeMaterial", RpcTarget.All, "Red");

        }

        //マテリアルの変更処理、K
        if (Input.GetKeyDown(KeyCode.K))
        {

            Debug.Log("マテリアルの変更処理： ");
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("ChangeMaterial", RpcTarget.All, "Blue");

        }
    }
    
    [PunRPC]
    public void ChangeMaterial(string materialName)
    {
        Debug.Log("マテリアルを変更しました: " + materialName);

        Renderer renderer = GetComponent<Renderer>();
        //Resourcesフォルダ内のマテリアルをロード
        Material material = Resources.Load<Material>("Materials/"+ materialName);
        //マテリアルを変更
        renderer.material = material;
    }
}
