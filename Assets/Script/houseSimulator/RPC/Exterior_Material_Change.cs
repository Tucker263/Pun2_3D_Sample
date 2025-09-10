using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Exterior_Material_Change : MonoBehaviour
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
        //staticクラス:Exterior_SelectedMaterialからマテリアル名を取得してRPC通信
        Debug.Log("マテリアルの変更処理： ");
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("ChangeMaterial", RpcTarget.All, Exterior_SelectedMaterial.materialName);

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
