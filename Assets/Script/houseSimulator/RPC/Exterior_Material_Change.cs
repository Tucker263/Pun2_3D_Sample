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

    public void ChangeAll()
    {
        List<GameObject> exteriorList = new List<GameObject>();
        //ネットワークオブジェクトからexteriorのリストを取得
        string targetTag = "exterior";
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            if (obj.CompareTag(targetTag))
            {
                exteriorList.Add(obj);
            }
        }
        //Exterior_Material_ChangeからChange関数を呼び出してマテリアルを変更
        foreach (GameObject obj in exteriorList)
        {
            Exterior_Material_Change exterior_Material_Change = obj.GetComponent<Exterior_Material_Change>();
            if (exterior_Material_Change != null)
            {
                exterior_Material_Change.Change();
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
