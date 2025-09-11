using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SmallHouse_Appear_Button : MonoBehaviour
{
    private GameObject house_small;

    // Start is called before the first frame update
    void Start()
    {
        Set_House_Small();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void Appear()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("Appear_RPC", RpcTarget.All);

    }

    [PunRPC]
    public void Appear_RPC()
    {
        if (house_small == null)
        {
            Set_House_Small();
        }
        if (!house_small.activeSelf)
        {
            house_small.SetActive(true);
        }
    }


    private void Set_House_Small()
    {
        house_small = NetworkObject_Search.GetObjectFromTag("house_small");  

        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);
    }
}
