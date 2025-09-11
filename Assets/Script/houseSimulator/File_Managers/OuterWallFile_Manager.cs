using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;


//家の外壁の情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class OuterWallInfo
{
    public string name; //ここの名前が被るとうまくロードができなくなるので注意
    public string materialName; //マテリアルの名前から、マテリアルを取得してセット
}


public static class OuterWallFile_Manager
{

    public static void Save(string directoryPath)
    {
        //outerWall1.jsonのような形式で保存
        Debug.Log("家の外壁のセーブ処理開始");
        string saveTag = "outerWall";
        int index = 1;
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            if (obj.CompareTag(saveTag))
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                //家の外壁の情報を取得
                OuterWallInfo outerWall = new OuterWallInfo();
                outerWall.name = obj.name;
                string materialName = renderer.material.name;
                outerWall.materialName = materialName.Replace(" (Instance)", "");

                // JSONに変換
                string jsonData = JsonUtility.ToJson(outerWall);

                string fileName = saveTag + index + ".json";
                string filePath = Path.Combine(directoryPath, fileName);
                // ファイルに保存
                File.WriteAllText(filePath, jsonData);
                index++;

            }

        }

        Debug.Log("家の外壁のセーブ処理終了");
    }


    public static void Load(string directoryPath)
    {
        Debug.Log("家の外壁のロード処理開始");
        string loadTag = "outerWall";
        List<string> jsonList = ReadAllFilesOfJSON(loadTag, directoryPath);
        foreach (string jsonData in jsonList)
        {
            //JSONをC#のオブジェクトに変換
            OuterWallInfo outerWall = JsonUtility.FromJson<OuterWallInfo>(jsonData);
            //ネットワークオブジェクト化したhouseからouterWallを手に入れる
            foreach (PhotonView view in PhotonNetwork.PhotonViews)
            {
                GameObject obj = view.gameObject;
                if (obj.CompareTag(loadTag) && obj.name == outerWall.name)
                {
                    Renderer renderer = obj.GetComponent<Renderer>();
                    obj.name = outerWall.name;
                    // Resourcesフォルダ内のマテリアルをロード
                    Material material = Resources.Load<Material>("Materials/"+ outerWall.materialName);
                    // ロードしたマテリアルをオブジェクトに適用
                    renderer.material = material;
                }
            }
        }

        Debug.Log("家の外壁のロード処理終了");
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
