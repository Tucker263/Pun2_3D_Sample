using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;


[System.Serializable] // JSON化するにはSerializable属性が必要
public class InteriorObject
{
    public string name;
    public Vector3 potision;
    public Quaternion rotation;

}

public static class StreamFile_Manager
{
    private static string dataPath = Application.persistentDataPath;


    public static void save()
    {
        InteriorObject interior = new InteriorObject();
        interior.name = "chair1";
        interior.potision = new Vector3(0, 0, 0);
        interior.rotation = Quaternion.Euler(0, 90, 0);

        string filePath = Path.Combine(dataPath, "saveData.json");

        // JSONに変換
        string jsonData = JsonUtility.ToJson(interior);

        // ファイルに保存
        File.WriteAllText(filePath, jsonData);
        Debug.Log("データを保存しました: " + filePath);
    }

    public static void Load()
    {
        string filePath = Path.Combine(dataPath, "saveData.json");
        // ファイルが存在するか確認
        if (File.Exists(filePath))
        {
            // ファイルからJSONデータを読み込む
            string jsonData = File.ReadAllText(filePath);

            // JSONをC#のオブジェクトに変換
            InteriorObject interior = JsonUtility.FromJson<InteriorObject>(jsonData);

            Debug.Log($"ロードしたデータ: 名前: {interior.potision}");


            var position = new Vector3(Random.Range(-8, 13), 2, Random.Range(-17, -10));
            Quaternion rotate12 = Quaternion.Euler(0, 90, 0);
            PhotonNetwork.Instantiate(interior.name, position, rotate12);



        }
        else
        {
            Debug.LogWarning("セーブデータが見つかりません");
        }


        //マスタークライアントのみ、ルームオブジェクトを作成可能
        //地面などの変わらないものは、ルームオブジェクトにし、家などの変更するものはネットワークオブジェクトにする予定
        var position1 = new Vector3(0, 0, 0);
        Quaternion rotate = Quaternion.Euler(0, 90, 0);
        PhotonNetwork.InstantiateRoomObject("house", position1, rotate);
        var position2 = new Vector3(15, 0, 0);
        PhotonNetwork.InstantiateRoomObject("house_mini", position2, rotate);
    }

    public static void InitialEnnvironment()
    {
        //マスタークライアントのみ、ルームオブジェクトを作成可能
        //地面などの変わらないものは、ルームオブジェクトにし、家などの変更するものはネットワークオブジェクトにする予定
        var position1 = new Vector3(0, 0, 0);
        Quaternion rotate = Quaternion.Euler(0, 90, 0);
        PhotonNetwork.InstantiateRoomObject("house", position1, rotate);
        var position2 = new Vector3(15, 0, 0);
        PhotonNetwork.InstantiateRoomObject("house_mini", position2, rotate);
    }
}
