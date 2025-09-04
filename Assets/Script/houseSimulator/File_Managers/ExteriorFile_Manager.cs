using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;


//エクステリアの情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class ExteriorInfo
{
    public string name; //ここの名前が被るとうまくロードができなくなるので注意
    public string materialName; //マテリアルの名前から、マテリアルを取得してセット
}


public static class ExteriorFile_Manager
{

    public static void Save(string directoryPath)
    {
        //exterior1.jsonのような形式で保存
        Debug.Log("エクステリアのセーブ処理開始");
        string saveTag = "exterior";
        int index = 1;
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            if (obj.CompareTag(saveTag))
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                //エクステリアの情報を取得
                ExteriorInfo exterior = new ExteriorInfo();
                exterior.name = obj.name;
                string materialName = renderer.material.name;
                exterior.materialName = materialName.Replace(" (Instance)", "");

                // JSONに変換
                string jsonData = JsonUtility.ToJson(exterior);

                string fileName = saveTag + index + ".json";
                string filePath = Path.Combine(directoryPath, fileName);
                // ファイルに保存
                File.WriteAllText(filePath, jsonData);
                index++;

            }

        }

        Debug.Log("エクステリアのセーブ処理終了");
    }


    public static void Load(string directoryPath)
    {
        Debug.Log("エクステリアのロード処理開始");
        string loadTag = "exterior";
        List<string> jsonList = ReadAllFilesOfJSON(loadTag, directoryPath);
        foreach (string jsonData in jsonList)
        {
            //JSONをC#のオブジェクトに変換
            ExteriorInfo exterior = JsonUtility.FromJson<ExteriorInfo>(jsonData);
            //ネットワークオブジェクト化したhouseからexteriorを手に入れる
            foreach (PhotonView view in PhotonNetwork.PhotonViews)
            {
                GameObject obj = view.gameObject;
                if (obj.CompareTag(loadTag) && obj.name == exterior.name)
                {
                    Renderer renderer = obj.GetComponent<Renderer>();
                    obj.name = exterior.name;
                    // Resourcesフォルダ内のマテリアルをロード
                    Material material = Resources.Load<Material>("Materials/"+ exterior.materialName);
                    // ロードしたマテリアルをオブジェクトに適用
                    renderer.material = material;
                }
            }
        }

        Debug.Log("エクステリアのロード処理終了");
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
