using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;
using UnityEngine;
using System.IO;



//照明の情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class LightingInfo
{
    public string name; //照明の場所の名前,ここの名前が被るとうまくロードができなくなるので注意
    public bool enabled; //コントローラーで、オンオフの切り替えが可能、distacegrabみたいに
    public float intensity; //光の明るさ、UIのスライダーでセットをして、コントローラでクリックして変更、distancegrabみたいに
    public string lightKind;//照明の種類、distancegrabみたいに変える、常夜灯、白熱灯、LEDなど

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
            GameObject obj = view.gameObject;
            if (obj.CompareTag(saveTag))
            {
                Light light = obj.GetComponent<Light>();
                TextMeshProUGUI objTMP = obj.GetComponent<TextMeshProUGUI>();
                //照明の情報を取得
                LightingInfo lighting = new LightingInfo();
                lighting.name = obj.name;
                lighting.enabled = light.enabled;
                lighting.intensity = light.intensity;
                lighting.lightKind = objTMP.text;

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

        foreach (string jsonData in jsonList)
        {
            //jsonからlightingオブジェクトに変換
            LightingInfo lighting = JsonUtility.FromJson<LightingInfo>(jsonData);
            //ネットワークオブジェクトしたhouseからlightingを手に入れる
            foreach (PhotonView view in PhotonNetwork.PhotonViews)
            {
                GameObject obj = view.gameObject;
                if (obj.CompareTag(loadTag) && obj.name == lighting.name)
                {
                    Light light = obj.GetComponent<Light>();
                    TextMeshProUGUI objTMP = obj.GetComponent<TextMeshProUGUI>();

                    //Resourcesフォルダ内の照明の種類をロードして、アタッチ
                    light = FetchLightFromKind(lighting.lightKind);

                    //lightの情報を書き換え
                    objTMP.text = lighting.lightKind;
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


    private static Light FetchLightFromKind(string lightKind)
    {
        //Resourcesフォルダ内のlightをロード
        GameObject objLight = Resources.Load<GameObject>("Lights/" + lightKind);
        Light light = objLight.GetComponent<Light>();
        return light;
    }
}
