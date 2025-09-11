using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

//環境を生成するクラス
public static class Environment_Creator
{
    //初期の環境を生成
    public static void CreateInitial()
    {
        //自身のアバターの生成
        var position = new Vector3(Random.Range(-8, 8), 2, Random.Range(-17, -10));
        PhotonNetwork.Instantiate("Avator", position, Quaternion.identity);

        //sunの生成
        var sunPosition = new Vector3(0, 100, 0);
        Quaternion sunRotation = Quaternion.Euler(90, 0, 0);
        PhotonNetwork.Instantiate("sun", sunPosition, sunRotation);

        //それ以外を生成
        Create();
    }

    //ロード後の環境を生成
    public static void CreateAfterLoad()
    {
        Create();
    }


    //環境を生成
    private static void Create()
    {
        //groundの生成
        var groundPosition = new Vector3(0, -50, 0);
        Quaternion groundRotation = Quaternion.Euler(0, 0, 0);
        PhotonNetwork.Instantiate("ground", groundPosition, groundRotation);

        //houseの生成
        var housePosition = new Vector3(0, 0, 0);
        Quaternion houseRotation = Quaternion.Euler(0, 90, 0);
        PhotonNetwork.Instantiate("house", housePosition, houseRotation);

        //house_miniの生成
        var houseMiniPosition = new Vector3(15, 0, 0);
        Quaternion houseMiniRotation = Quaternion.Euler(0, 90, 0);
        GameObject houseMini = PhotonNetwork.Instantiate("house_small", houseMiniPosition, houseMiniRotation);
        //houseと名前被りを避けるため、子孫のオブジェクト全部に_smallという接尾語を足す
        List<Transform> houseMini_children = GetAllChildObjects(houseMini.GetComponent<Transform>());
        foreach(Transform child in houseMini_children)
        {
            child.name += "_small";
        }

    }


    private static List<Transform> GetAllChildObjects(Transform parent)
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in parent)
        {
            children.Add(child);
            // 再帰的に孫オブジェクトも取得
            children.AddRange(GetAllChildObjects(child));
        }

        return children;
    }


    public static void NameHouseToSmall()
    {
        GameObject house_small = NetworkObject_Search.GetObjectFromTag("house_small");
        //houseと名前被りを避けるため、子孫のオブジェクト全部に_smallという接尾語を足す
        List<Transform> houseSmall_children = GetAllChildObjects(house_small.GetComponent<Transform>());
        foreach(Transform child in houseSmall_children)
        {
            string childName = child.name;
            if(!childName.Contains("_small"))
            {
                child.name += "_small";
            }
        }
    }
    
}
