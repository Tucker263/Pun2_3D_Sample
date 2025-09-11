using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class LatestState_Synchronize : MonoBehaviourPunCallbacks
{
    public void Start()
    {
    
    }
    public void Update()
    {
    
    }

    public void Synchronize()
    {
        PhotonView photonView = PhotonView.Get(this);

        //house_smallの名前の同期、これをしないとゲスト側の家の名前が被る
        photonView.RPC("NameHouseToSmall", RpcTarget.Others);

        //照明の同期
        List<GameObject> lightingList = NetworkObject_Search.GetListFromTag("lighting");
        foreach(GameObject obj in lightingList)
        {
            Light light = obj.GetComponent<Light>();
            //照明の情報を取得
            LightingInfo lighting = new LightingInfo();
            lighting.name = obj.name;
            lighting.enabled = light.enabled;
            lighting.intensity = light.intensity;
            //JSONに変換してRPC通信
            string jsonData = JsonUtility.ToJson(lighting);
            photonView.RPC("SynchronizeLighting", RpcTarget.Others, jsonData);

        }

        //家の外壁の同期
        List<GameObject> outerWallList = NetworkObject_Search.GetListFromTag("outerWall");
        foreach(GameObject obj in outerWallList)
        {
            //家の外壁の情報を取得
            OuterWallInfo outerWall = new OuterWallInfo();
            outerWall.name = obj.name;
            string materialName = obj.GetComponent<Renderer>().material.name;
            outerWall.materialName = materialName.Replace(" (Instance)", "");
            //JSONに変換してRPC通信
            string jsonData = JsonUtility.ToJson(outerWall);
            photonView.RPC("SynchronizeOutWall", RpcTarget.Others, jsonData);
        }

        //house_smallのアクティブ情報を同期
        GameObject house_small = NetworkObject_Search.GetObjectFromTag("house_small");
        bool isActive = house_small.activeSelf;
        photonView.RPC("SynchronizeHouseSmallActive", RpcTarget.Others, isActive);
    
    }

    [PunRPC]
    public void NameHouseToSmall()
    {
        //名前が同期できていないため、_smallをつけるように同期する
        Environment_Creator.NameHouseToSmall();
    }

    [PunRPC]
    public void SynchronizeLighting(string jsonData)
    {
        //JSONをC#のオブジェクトに変換
        LightingInfo lighting = JsonUtility.FromJson<LightingInfo>(jsonData);
        //ネットワークオブジェクトの中からlightingを手に入れて反映
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            //名前が被るとうまく反映できなくなるので注意
            if (obj.CompareTag("lighting") && obj.name == lighting.name)
            {
                Light light = obj.GetComponent<Light>();
                light.name = lighting.name;
                light.enabled = lighting.enabled;
                light.intensity = lighting.intensity;
            }
        }

    }

    [PunRPC]
    public void SynchronizeOutWall(string jsonData)
    {
        //JSONをC#のオブジェクトに変換
        OuterWallInfo outerWall = JsonUtility.FromJson<OuterWallInfo>(jsonData);
        //ネットワークオブジェクトの中からouterWallを手に入れて反映
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            //名前が被るとうまく反映ができなくなるので注意
            if (obj.CompareTag("outerWall") && obj.name == outerWall.name)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                obj.name = outerWall.name;
                // Resourcesフォルダ内のマテリアルをロード
                Material material = Resources.Load<Material>("Materials/" + outerWall.materialName);
                // ロードしたマテリアルをオブジェクトに適用
                renderer.material = material;
            }
        }

    }


    [PunRPC]
    public void SynchronizeHouseSmallActive(bool isActive)
    {
        //ネットワークオブジェクトの中からhouse_smallを手に入れて反映
        GameObject house_small = NetworkObject_Search.GetObjectFromTag("house_small");
        house_small.SetActive(isActive);
        
    }

}