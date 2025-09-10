using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
public class AvatorUI_Display : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = this.gameObject;
        // 自身のUIオブジェクトの時はアクティブ化
        bool isActive = photonView.IsMine ? true : false;
        obj.SetActive(isActive);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
