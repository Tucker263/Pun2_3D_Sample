using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


//staticクラス、ネットワークオブジェクトを探すクラス
public static class NetworkObject_Search
{
    public static GameObject GetObjectFromName(string targetName)
    {
        //targetNameが単体の時
        GameObject target = null;
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            if (obj.name == targetName)
            {
                target = obj;
                break;
            }
        }

        return target;
    }


    public static List<GameObject> GetListFromName(string targetName)
    {
        //targetNameが複数個の時
        List<GameObject> targetList = new List<GameObject>();
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            if (obj.name == targetName)
            {
                targetList.Add(obj);
            }
        }

        return targetList;
    }


    public static GameObject GetObjectFromTag(string targetTag)
    {
        //targetTagが単体の時
        GameObject target = null;
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            if (obj.CompareTag(targetTag))
            {
                target = obj;
                break;
            }
        }

        return target;
    }


    public static List<GameObject> GetListFromTag(string targetTag)
    {
        //targetTagが複数個の時
        List<GameObject> targetList = new List<GameObject>();
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            if (obj.CompareTag(targetTag))
            {
                targetList.Add(obj);
            }
        }

        return targetList;
    }
}   
        
