using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;


public static class StreamFile_Manager
{
    private static string dataPath = Application.persistentDataPath;
    private static string directoryPath = Path.Combine(dataPath, Config.directoryName);

    public static void Save()
    {
        if (Directory.Exists(directoryPath))
        {
            //家具のセーブ処理
            FurnitureFile_Manager.Save();
            //照明のセーブ処理
            LightingFile_Manager.Save();
            //エクステリアのセーブ処理
            ExteriorFile_Manager.Save();
        }
    }



    public static void Load()
    {
        //ディレクトリのパスを更新
        directoryPath = Path.Combine(dataPath, Config.directoryName);
        //「初めから」の場合
        if (Config.isInitialStart)
        {
            if (Directory.Exists(directoryPath))
            {
                //ディレクトリを中身ごと削除して、新たに生成
                Directory.Delete(directoryPath, true);
                //Debug.Log("ディレクトリを中身ごと削除しました");
            }
            Directory.CreateDirectory(directoryPath);
            InitialEnnvironment();
            return;
        }

        //ディレクトリがない場合
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            InitialEnnvironment();
            return;
        }

        //初期環境をロード
        InitialEnnvironment();
        //家具のロード処理
        FurnitureFile_Manager.Load();
        //照明のロード処理
        LightingFile_Manager.Load();
        //エクステリアのロード処理
        ExteriorFile_Manager.Load();
    }


    //初期の環境を設定
    private static void InitialEnnvironment()
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
        
