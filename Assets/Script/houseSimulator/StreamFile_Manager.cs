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
            Create_InitialEnvironment.Create();
            return;
        }

        //ディレクトリがない場合
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            Create_InitialEnvironment.Create();
            return;
        }

        //初期環境をロード
        Create_InitialEnvironment.Create();
        //家具のロード処理
        FurnitureFile_Manager.Load();
        //照明のロード処理
        LightingFile_Manager.Load();
        //エクステリアのロード処理
        ExteriorFile_Manager.Load();
    }
}   
        
