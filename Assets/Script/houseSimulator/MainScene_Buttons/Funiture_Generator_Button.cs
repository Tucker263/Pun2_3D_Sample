using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Funiture_Generator_Button : MonoBehaviourPunCallbacks
{
    private string furnitureName;
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI buttonTMP = GetComponentInChildren<TextMeshProUGUI>();
        furnitureName = buttonTMP.text;
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void Generate()
    {
        var position = new Vector3(Random.Range(-8, 13), 2, Random.Range(-17, -10));
        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        PhotonNetwork.Instantiate(furnitureName, position, rotation);

        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

    }
}
