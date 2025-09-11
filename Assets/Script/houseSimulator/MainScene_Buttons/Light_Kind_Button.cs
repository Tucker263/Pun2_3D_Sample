using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Light_Kind_Button : MonoBehaviourPunCallbacks
{
    private TextMeshProUGUI buttonTMP;
    private string lightKind;
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI buttonTMP = GetComponentInChildren<TextMeshProUGUI>();
        lightKind = buttonTMP.text;
  
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void Select()
    {
        //staticクラスのLight_SelectedStateに選んだ照明の種類を入力
        Light_SelectedState.lightKind = lightKind;

        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);
    }
}
