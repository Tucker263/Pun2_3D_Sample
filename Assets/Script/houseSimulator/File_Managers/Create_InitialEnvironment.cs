using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

//初期の環境を設定するクラス
public static class Create_InitialEnvironment
{
    //初期の環境を設定
    public static void Create()
    {
        //house_miniの生成
        var houseMiniPosition = new Vector3(15, 0, 0);
        Quaternion houseMiniRotation = Quaternion.Euler(0, 90, 0);
        PhotonNetwork.Instantiate("house_mini", houseMiniPosition, houseMiniRotation);
        //名前が被るとうまくセーブできないので、先に_miniという接尾語を足す
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            string objName = obj.name;
            objName = objName.Replace("(Clone)", "");
            if ((obj.CompareTag("lighting") || obj.CompareTag("exterior")) && !objName.Contains("_mini"))
            {
                obj.name += "_mini";
            }
        }

        //sunの生成
        var sunPosition = new Vector3(0, 100, 0);
        Quaternion sunRotation = Quaternion.Euler(90, 0, 0);
        PhotonNetwork.Instantiate("sun", sunPosition, sunRotation);

        //groundの生成
        var groundPosition = new Vector3(0, -50, 0);
        Quaternion groundRotation = Quaternion.Euler(0, 0, 0);
        PhotonNetwork.Instantiate("ground", groundPosition, groundRotation);

        //houseの生成
        var housePosition = new Vector3(0, 0, 0);
        Quaternion houseRotation = Quaternion.Euler(0, 90, 0);
        PhotonNetwork.Instantiate("house", housePosition, houseRotation);
    }
}
