using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;


[System.Serializable] // JSON化するにはSerializable属性が必要
public class InteriorObject
{
    public string name;
    public Vector3 position;
    public Quaternion rotation;

}

public static class StreamFile_Manager
{
    private static string dataPath = Application.persistentDataPath;


    public static void save()
    {
        /*InteriorObject interior1 = new InteriorObject();
        interior1.name = "chair1";
        interior1.potision = new Vector3(0, 0, 0);
        interior1.rotation = Quaternion.Euler(0, 90, 0);*/


        int i = 1;
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            Debug.Log("ネットワークオブジェクト: " + obj.name);
            Debug.Log(obj.name == "chair1(Clone)");
            if (obj.name == "chair1(Clone)")
            {
                InteriorObject interior = new InteriorObject();
                interior.name = "chair1";
                interior.position = obj.GetComponent<Transform>().position;
                interior.rotation = obj.GetComponent<Transform>().rotation;

                // JSONに変換
                string jsonData = JsonUtility.ToJson(interior);

                string fileName = "saveData" + i + ".json";
                string filePath = Path.Combine(dataPath, fileName);
                // ファイルに保存
                File.WriteAllText(filePath, jsonData);
                Debug.Log("データを保存しました: " + filePath);
                i++;

            }

        }

      



    }

    public static void Load()
    {
        int i = 1;
        while (true)
        {
            string fileName = "saveData" + i + ".json";
            string filePath = Path.Combine(dataPath, fileName);
            // ファイルが存在するか確認
            if (File.Exists(filePath))
            {
                // ファイルからJSONデータを読み込む
                string jsonData = File.ReadAllText(filePath);

                // JSONをC#のオブジェクトに変換
                InteriorObject interior = JsonUtility.FromJson<InteriorObject>(jsonData);

                Debug.Log($"ロードしたデータ: 名前: {interior.name}");

                PhotonNetwork.Instantiate(interior.name, interior.position, interior.rotation);



            }
            else
            {
                Debug.LogWarning("セーブデータが見つかりません");
                break;
            }
            i++;
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
