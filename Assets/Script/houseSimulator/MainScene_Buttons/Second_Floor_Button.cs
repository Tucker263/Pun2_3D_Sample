using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Second_Floor_Button : MonoBehaviour
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

    public void Move_Position()
    {
        avator_tf.position = new Vector3(0, 7, 0);

    }

}
