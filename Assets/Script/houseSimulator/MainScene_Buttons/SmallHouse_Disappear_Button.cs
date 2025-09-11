using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class SmallHouse_Disapear_Button : MonoBehaviour
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

    public void Disappear()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("Disappear_RPC", RpcTarget.All);
    }

    [PunRPC]
    public void Disappear_RPC()
    {
        if (house_small == null)
        {
            Set_House_Small();
        }
        if (house_small.activeSelf)
        {
            house_small.SetActive(false);
        }
    }

    private void Set_House_Small()
    {
        //ネットワークオブジェクトからhouse_smallを取得
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            string objName = obj.name;
            objName = objName.Replace("(Clone)", "");
            //Tagがhouse_smallだった時
            if (obj.CompareTag("house_small"))
            {
                house_small = obj;
                break;
            }
        }
        
    }
}
