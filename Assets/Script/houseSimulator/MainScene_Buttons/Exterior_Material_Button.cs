using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Exterior_Material_Button : MonoBehaviourPunCallbacks
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
            GameObject myObject = PhotonNetwork.Instantiate(furnitureName, position, rotation);
            Debug.Log("自分が生成したオブジェクト: " + myObject.name);
    }
}
