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
        //ネットワークオブジェクトからSunを取得
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

    // Update is called once per frame
    void Update()
    {
        

    }

    public void Disappear()
    {
        if (house_small.activeSelf)
        {
            house_small.SetActive(false);
        }
    }
}
