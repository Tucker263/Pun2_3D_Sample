using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;

//アバターの情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class AvatorInfo
{
    public string name;
    public Vector3 position;
    public Quaternion rotation;
}


public static class AvatorFile_Manager
{

    public static void Save(string directoryPath)
    {
        //avator.jsonのような形式で保存
        Debug.Log("アバター情報のセーブ処理開始");
        string saveTag = "avator";
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            string objName = obj.name;
            objName = objName.Replace("(Clone)", "");
            //Tagがアバターだった時
            if (obj.CompareTag(saveTag))
            {
                //アバターの情報を取得
                AvatorInfo avator = new AvatorInfo();
                avator.name = objName;
                avator.position = obj.GetComponent<Transform>().position;
                avator.rotation = obj.GetComponent<Transform>().rotation;

                // JSONに変換
                string jsonData = JsonUtility.ToJson(avator);

                string fileName = saveTag + ".json";
                string filePath = Path.Combine(directoryPath, fileName);
                // ファイルに保存
                File.WriteAllText(filePath, jsonData);

                Debug.Log(jsonData);
            }

        }
        Debug.Log("アバター情報のセーブ処理終了");
    }

    public static void Load(string directoryPath)
    {
        //avator.jsonのような形式を読み込み
        Debug.Log("アバター情報のロード処理開始");
        string loadTag = "avator";
        string jsonData = ReadFileOfJSON(loadTag, directoryPath);

        //jsonからavatorオブジェクトに変換
        Debug.Log(jsonData);
        AvatorInfo avator = JsonUtility.FromJson<AvatorInfo>(jsonData);

        //ネットワークオブジェクト化
        PhotonNetwork.Instantiate(avator.name, avator.position, avator.rotation);
        Debug.Log("アバター情報のロード処理終了");
        
    }
    

    private static string ReadFileOfJSON(string loadTag, string directoryPath)
    {
        //フォルダ内の"{loadTag}.json"を読み込む
        //フォルダ内は"{loadTag}.json"という形式で順に保存されている
        string fileName = loadTag + ".json";
        string filePath = Path.Combine(directoryPath, fileName);
        string jsonData = "";
        //ファイルが存在するか確認
        if (File.Exists(filePath))
        {
            //ファイルからJSONデータを読み込む
            jsonData = File.ReadAllText(filePath);
        }
        else
        {
            Debug.Log("avatorファイルが見つかりませんでした。");
        }
           
        return jsonData;
    }
}
