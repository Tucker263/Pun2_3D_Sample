using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;

//家具の情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class FurnitureInfo
{
    public string name;
    public Vector3 position;
    public Quaternion rotation;
}


public static class FurnitureFile_Manager
{

    public static void Save(string directoryPath)
    {
        //future1.jsonのような形式で保存
        Debug.Log("家具のセーブ処理開始");
        string saveTag = "furniture";
        int index = 1;
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            string objName = obj.name;
            objName = objName.Replace("(Clone)", "");
            //Tagが家具だった時
            if (obj.CompareTag(saveTag))
            {
                //家具の情報を取得
                FurnitureInfo funiture = new FurnitureInfo();
                funiture.name = objName;
                funiture.position = obj.GetComponent<Transform>().position;
                funiture.rotation = obj.GetComponent<Transform>().rotation;

                // JSONに変換
                string jsonData = JsonUtility.ToJson(funiture);

                string fileName = saveTag + index + ".json";
                string filePath = Path.Combine(directoryPath, fileName);
                // ファイルに保存
                File.WriteAllText(filePath, jsonData);
                index++;

            }

        }
        Debug.Log("家具のセーブ処理終了");
    }

    public static void Load(string directoryPath)
    {
        //future1.jsonのような形式を読み込み
        Debug.Log("家具のロード処理開始");
        string loadTag = "furniture";
        List<string> jsonList = ReadAllFilesOfJSON(loadTag, directoryPath);

        //jsonからfurnitureオブジェクトに変換
        foreach (string jsonData in jsonList)
        {
            //JSONをC#のオブジェクトに変換
            FurnitureInfo funiture = JsonUtility.FromJson<FurnitureInfo>(jsonData);
            //ネットワークオブジェクト化
            PhotonNetwork.Instantiate(funiture.name, funiture.position, funiture.rotation);

        }

        Debug.Log("家具のロード処理終了");
    }
    

    private static List<string> ReadAllFilesOfJSON(string loadTag, string directoryPath)
    {
        //フォルダ内の全ての"{loadTag}.json"を読み込む
        //フォルダ内は"{loadTag}.json"という形式で順に保存されている
        List<string> jsonList = new List<string>();
        int index = 1;
        while (true)
        {
            string fileName = loadTag + index + ".json";
            string filePath = Path.Combine(directoryPath, fileName);
            // ファイルが存在するか確認
            if (File.Exists(filePath))
            {
                // ファイルからJSONデータを読み込む
                string jsonData = File.ReadAllText(filePath);
                jsonList.Add(jsonData);
            }
            else
            {
                break;
            }
            index++;
        }

        return jsonList;
    }
}
