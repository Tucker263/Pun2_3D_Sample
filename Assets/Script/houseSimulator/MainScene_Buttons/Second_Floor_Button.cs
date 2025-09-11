using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class Second_Floor_Button : MonoBehaviour
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
            if (avator_Controller.IsMine())
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
        
        //selected状態を解除,この処理がないとメニューバーの表示で二重で動く
        EventSystem.current.SetSelectedGameObject(null);

    }

}
