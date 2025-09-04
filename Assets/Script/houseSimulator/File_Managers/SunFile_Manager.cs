using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;

//太陽の情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class SunInfo
{
    public string name;
    public Vector3 position;
    public Quaternion rotation;
}


public static class SunFile_Manager
{

    public static void Save(string directoryPath)
    {
        //sun.jsonのような形式で保存
        Debug.Log("太陽のセーブ処理開始");
        string saveTag = "sun";
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            string objName = obj.name;
            objName = objName.Replace("(Clone)", "");
            //Tagが太陽だった時
            if (obj.CompareTag(saveTag))
            {
                //太陽の情報を取得
                SunInfo sun = new SunInfo();
                sun.name = objName;
                sun.position = obj.GetComponent<Transform>().position;
                sun.rotation = obj.GetComponent<Transform>().rotation;

                // JSONに変換
                string jsonData = JsonUtility.ToJson(sun);

                string fileName = saveTag + ".json";
                string filePath = Path.Combine(directoryPath, fileName);
                // ファイルに保存
                File.WriteAllText(filePath, jsonData);

                Debug.Log(jsonData);
            }

        }
        Debug.Log("太陽のセーブ処理終了");
    }

    public static void Load(string directoryPath)
    {
        //sun.jsonのような形式を読み込み
        Debug.Log("太陽のロード処理開始");
        string loadTag = "sun";
        string jsonData = ReadFileOfJSON(loadTag, directoryPath);

        //jsonからsunオブジェクトに変換
        Debug.Log(jsonData);
        SunInfo sun = JsonUtility.FromJson<SunInfo>(jsonData);

        //ネットワークオブジェクト化
        PhotonNetwork.Instantiate(sun.name, sun.position, sun.rotation);
        Debug.Log("太陽のロード処理終了");
        
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
            Debug.Log("Sunファイルが見つかりませんでした。");
        }
           
        return jsonData;
    }
}
