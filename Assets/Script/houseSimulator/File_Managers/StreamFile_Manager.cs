using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class StreamFile_Manager
{
    private static string dataPath = Application.persistentDataPath;
    private static string directoryPath = Path.Combine(dataPath, Config.directoryName);

    public static void Save()
    {
        //常に最新の状態にしたいので、データを初期化(中身ごと削除)してセーブ
        Directory.Delete(directoryPath, true);
        Directory.CreateDirectory(directoryPath);

        //アバター情報のセーブ処理
        AvatorFile_Manager.Save(directoryPath);
        //太陽のセーブ処理
        SunFile_Manager.Save(directoryPath);
        //家具のセーブ処理
        FurnitureFile_Manager.Save(directoryPath);
        //照明のセーブ処理
        LightingFile_Manager.Save(directoryPath);
        //家の外壁のセーブ処理
        OuterWallFile_Manager.Save(directoryPath);
        //家の屋根のセーブ処理
        
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
            Environment_Creator.CreateInitial();
            return;
        }

        //ディレクトリがない場合
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            Environment_Creator.CreateInitial();
            return;
        }

        //環境を生成
        Environment_Creator.CreateAfterLoad();
        //アバター情報のロード処理
        AvatorFile_Manager.Load(directoryPath);
        //太陽のロード処理
        SunFile_Manager.Load(directoryPath);
        //家具のロード処理
        FurnitureFile_Manager.Load(directoryPath);
        //照明のロード処理
        LightingFile_Manager.Load(directoryPath);
        //家の外壁のロード処理
        OuterWallFile_Manager.Load(directoryPath);
        //家の屋根のロード処理
    }
}   
        
