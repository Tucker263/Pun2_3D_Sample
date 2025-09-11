using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Exterior_Material_Button : MonoBehaviourPunCallbacks
{
    private TextMeshProUGUI buttonTMP;
    private string materialName;
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI buttonTMP = GetComponentInChildren<TextMeshProUGUI>();
        materialName = buttonTMP.text;
  
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void Select()
    {
        //staticクラスのExterior_SelectedMaterialに選んだマテリアル名を入力
        Exterior_SelectedMaterial.materialName = materialName;

        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);
    }
}
