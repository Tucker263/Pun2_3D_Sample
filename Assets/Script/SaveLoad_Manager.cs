using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable] // JSON化するにはSerializable属性が必要
public class PlayerData
{
    public string playerName;
    public int level;
    public float health;
}

public static class SaveLoad_Manager
{
    private static string filePath = Application.persistentDataPath;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("saveLoadのスタート処理");
        filePath = Path.Combine(Application.persistentDataPath, "saveData.json");

        // データを保存
        SaveGame();

        // データをロード
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void SaveGame()
    {
        PlayerData player = new PlayerData();
        player.playerName = "勇者";
        player.level = 10;
        player.health = 120.5f;

        // JSONに変換
        string jsonData = JsonUtility.ToJson(player);

        // ファイルに保存
        File.WriteAllText(filePath, jsonData);
        Debug.Log("データを保存しました: " + filePath);
    }

    void LoadGame()
    {
        // ファイルが存在するか確認
        if (File.Exists(filePath))
        {
            // ファイルからJSONデータを読み込む
            string jsonData = File.ReadAllText(filePath);

            // JSONをC#のオブジェクトに変換
            PlayerData loadedPlayer = JsonUtility.FromJson<PlayerData>(jsonData);

            Debug.Log($"ロードしたデータ: 名前: {loadedPlayer.playerName}, レベル: {loadedPlayer.level}, 体力: {loadedPlayer.health}");
        }
        else
        {
            Debug.LogWarning("セーブデータが見つかりません");
        }
    }
}
