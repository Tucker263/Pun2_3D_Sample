using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Exterior_Position_Button : MonoBehaviour
{
    private GameObject avator;
    private Transform avator_tf;

    // Start is called before the first frame update
    void Start()
    {
        //ネットワークオブジェクトからavatorを取得
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            string objName = obj.name;
            objName = objName.Replace("(Clone)", "");
            //Tagがavatorだった時
            if (obj.CompareTag("avator"))
            {
                avator = obj;
                break;
            }
        }

        avator_tf = avator.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void Change_Position()
    {
        avator_tf.position = new Vector3(-10, 1, -10);

    }

}
