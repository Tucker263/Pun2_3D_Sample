using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Outside_Position_Button : MonoBehaviour
{
    private GameObject avator;
    private Transform avator_tf;

    // Start is called before the first frame update
    void Start()
    {
        //ネットワークオブジェクトからavatorListを取得
        List<GameObject> avatorList = NetworkObject_Search.GetListFromTag("avator");
        foreach (GameObject obj in avatorList)
        {
            Avator_Controller avator_Controller = obj.GetComponent<Avator_Controller>();
            if (avator_Controller.IsMine)
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
