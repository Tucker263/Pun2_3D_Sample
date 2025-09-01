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


//照明の情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class LightingInfo
{
    public string name; //照明の名前、白熱灯、常夜灯、などアセットから色々追加する、照明の種類の切り替えはコントローラーでdistacegrabみたいに
    public bool isOn; //コントローラーで、オンオフの切り替えが可能、distacegrabみたいに
    public int brightness; //光の明るさ、UIのスライダーでセットをして、コントローラでクリックして変更、distancegrabみたいに

}


//エクステリアの情報
[System.Serializable] // JSON化するにはSerializable属性が必要
public class ExteriorInfo
{
    public string name;
    public string materialName;
}



public static class StreamFile_Manager
{
    private static string dataPath = Application.persistentDataPath;
    private static string directoryPath = Path.Combine(dataPath, Config.directoryName);

    public static void Save()
    {
        if (Directory.Exists(directoryPath))
        {
            //家具のセーブ処理
            SaveFurniture();
            //照明のセーブ処理
            SaveLighting();
            //エクステリアのセーブ処理
            SaveExterior();
        }
    }

    private static void SaveFurniture()
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
                Debug.Log($"セーブするデータ: {jsonData}");
                index++;

            }

        }
        Debug.Log("家具のセーブ処理終了");
    }


    private static void SaveLighting()
    {
        //lighting1.jsonのような形式で保存
        Debug.Log("照明のセーブ処理開始");
        string saveTag = "lighting";
        int index = 1;
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            string objName = obj.name;
            objName = objName.Replace("(Clone)", "");
            //Tagが照明だった時
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
                Debug.Log($"セーブするデータ: {jsonData}");
                index++;

            }

        }

        Debug.Log("照明のセーブ処理終了");
    }


    private static void SaveExterior()
    {
        Debug.Log("エクステリアのセーブ処理開始");

        Debug.Log("エクステリアのセーブ処理終了");
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
                Debug.Log("ディレクトリを中身ごと削除しました");
            }
            Directory.CreateDirectory(directoryPath);
            InitialEnnvironment();
            return;
        }

        //ディレクトリがない場合
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            InitialEnnvironment();
            return;
        }
        
        //家具のロード処理
        LoadFurniture();
        //照明のロード処理
        LoadLighting();
        //エクステリアのロード処理
        LoadExterior();
    }

    private static void LoadFurniture()
    {
        //future1.jsonのような形式を読み込み
        Debug.Log("家具のロード処理開始");
        string loadTag = "furniture";
        List<string> jsonList = ReadAllFilesOfJSON(loadTag);

        //jsonからfurnitureオブジェクトに変換
        foreach(string jsonData in jsonList){
            Debug.Log($"ロードするデータ: {jsonData}");
            //JSONをC#のオブジェクトに変換
            FurnitureInfo funiture = JsonUtility.FromJson<FurnitureInfo>(jsonData);
            //ネットワークオブジェクト化
            PhotonNetwork.Instantiate(funiture.name, funiture.position, funiture.rotation);

        }
        
        Debug.Log("家具のロード処理終了");
    }

    private static void LoadLighting()
    {
        //light1.jsonのような形式を読み込み
        Debug.Log("照明のロード処理開始");
        string loadTag = "lighting";
        List<string> jsonList = ReadAllFilesOfJSON(loadTag);

        //jsonからfurnitureオブジェクトに変換
        foreach(string jsonData in jsonList){
            Debug.Log($"ロードするデータ: {jsonData}");
            //JSONをC#のオブジェクトに変換
            FurnitureInfo funiture = JsonUtility.FromJson<FurnitureInfo>(jsonData);
            //ネットワークオブジェクト化
            PhotonNetwork.Instantiate(funiture.name, funiture.position, funiture.rotation);

        }


        //PhotonNetwork.Instantiate()で生成されたゲームオブジェクトも取得できる

        Debug.Log("照明のロード処理終了");
    }

    private static void LoadExterior()
    {
        Debug.Log("エクステリアのロード処理開始");

        //マテリアルをセットする処理も書く
        //マテリアルの情報を同期させる処理も必要かも
        //GameObject obj = PhotonNetwork.Instantiate()で生成されたゲームオブジェクトも取得できる
        InitialEnnvironment();


        Debug.Log("エクステリアのロード処理終了");
    }

    private static List<string> ReadAllFilesOfJSON(string loadTag)
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


    //初期の環境を設定
    private static void InitialEnnvironment()
    {
        //sunの生成
        var sunPosition = new Vector3(0, 100, 0);
        Quaternion sunRotation = Quaternion.Euler(90, 0, 0);
        PhotonNetwork.Instantiate("sun", sunPosition, sunRotation);

        //groundの生成
        var groundPosition = new Vector3(0, -50, 0);
        Quaternion groundRotation = Quaternion.Euler(0, 0, 0);
        PhotonNetwork.Instantiate("ground", groundPosition, groundRotation);

        //houseの生成
        var housePosition = new Vector3(0, 0, 0);
        Quaternion houseRotation = Quaternion.Euler(0, 90, 0);
        PhotonNetwork.Instantiate("house", housePosition, houseRotation);

        //house_miniの生成
        var houseMiniPosition = new Vector3(15, 0, 0);
        Quaternion houseMiniRotation = Quaternion.Euler(0, 90, 0);
        PhotonNetwork.Instantiate("house_mini", houseMiniPosition, houseMiniRotation);
    }
}
