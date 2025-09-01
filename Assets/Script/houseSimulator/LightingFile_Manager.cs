using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;


//照明の情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class LightingInfo
{
    public string name; //照明の名前、白熱灯、常夜灯、などアセットから色々追加する、照明の種類の切り替えはコントローラーでdistacegrabみたいに
    public bool enabled; //コントローラーで、オンオフの切り替えが可能、distacegrabみたいに
    public float intensity; //光の明るさ、UIのスライダーでセットをして、コントローラでクリックして変更、distancegrabみたいに

}

public static class LightingFile_Manager
{

    public static void Save(string directoryPath)
    {
        //lighting1.jsonのような形式で保存
        Debug.Log("照明のセーブ処理開始");
        string saveTag = "lighting";
        int index = 1;
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            //名前が被るとうまくセーブができなくなるので注意
            GameObject obj = view.gameObject;
            string objName = obj.name;
            objName = objName.Replace("(Clone)", "");
            if (obj.CompareTag(saveTag))
            {
                Light light = obj.GetComponent<Light>();
                //照明の情報を取得
                LightingInfo lighting = new LightingInfo();
                lighting.name = objName;
                lighting.enabled = light.enabled;
                lighting.intensity = light.intensity;

                // JSONに変換
                string jsonData = JsonUtility.ToJson(lighting);

                string fileName = saveTag + index + ".json";
                string filePath = Path.Combine(directoryPath, fileName);
                // ファイルに保存
                File.WriteAllText(filePath, jsonData);
                index++;

            }

        }

        Debug.Log("照明のセーブ処理終了");
    }


    public static void Load(string directoryPath)
    {
        //light1.jsonのような形式を読み込み
        Debug.Log("照明のロード処理開始");
        string loadTag = "lighting";
        List<string> jsonList = ReadAllFilesOfJSON(loadTag, directoryPath);

        //jsonからlightingオブジェクトに変換
        foreach (string jsonData in jsonList)
        {
            //JSONをC#のオブジェクトに変換
            LightingInfo lighting = JsonUtility.FromJson<LightingInfo>(jsonData);
            //ネットワークオブジェクトしたhouseからlightingを手に入れる
            foreach (PhotonView view in PhotonNetwork.PhotonViews)
            {
                GameObject obj = view.gameObject;
                string objName = obj.name;
                objName = objName.Replace("(Clone)", "");

                //名前が被るとうまくロードができなくなるので注意
                if (obj.CompareTag(loadTag) && objName == lighting.name)
                {
                    Light light = obj.GetComponent<Light>();
                    light.name = lighting.name;
                    light.enabled = lighting.enabled;
                    light.intensity = lighting.intensity;
                }

            }

        }

        Debug.Log("照明のロード処理終了");
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
