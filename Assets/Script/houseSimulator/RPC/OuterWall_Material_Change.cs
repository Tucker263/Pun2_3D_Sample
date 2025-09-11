using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class OuterWall_Material_Change : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAll()
    {
        List<GameObject> outerWallList = NetworkObject_Search.GetListFromTag("outerWall");
        
        //OuterWall_Material_ChangeからChange関数を呼び出してマテリアルを変更
        foreach (GameObject obj in outerWallList)
        {
            OuterWall_Material_Change outerWall_Material_Change = obj.GetComponent<OuterWall_Material_Change>();
            if (outerWall_Material_Change != null)
            {
                outerWall_Material_Change.Change();
            }

        }
        
    }

    public void Change()
    {
        //staticクラス:Exterior_SelectedMaterialからマテリアル名を取得してRPC通信
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("ChangeMaterial", RpcTarget.All, Exterior_SelectedMaterial.materialName);

    }
    
    [PunRPC]
    public void ChangeMaterial(string materialName)
    {
        Renderer renderer = GetComponent<Renderer>();
        //Resourcesフォルダ内のマテリアルをロード
        Material material = Resources.Load<Material>("Materials/"+ materialName);
        //マテリアルを変更
        renderer.material = material;
    }
}
