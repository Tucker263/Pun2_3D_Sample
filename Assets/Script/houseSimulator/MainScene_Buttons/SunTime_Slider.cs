using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class SunTime_Slider : MonoBehaviourPunCallbacks
{
    private GameObject sun;
    private Slider slider;
    private Transform sun_tf;
    private Sun_Ownership sun_Ownership;
    // Start is called before the first frame update
    void Start()
    {
        //ネットワークオブジェクトからSunを取得
        foreach (PhotonView view in PhotonNetwork.PhotonViews)
        {
            GameObject obj = view.gameObject;
            string objName = obj.name;
            objName = objName.Replace("(Clone)", "");
            Debug.Log(obj);
            //Tagがsunだった時
            if (obj.CompareTag("sun"))
            {
                sun = obj;
                break;
            }
        }

        slider = GetComponent<Slider>();
        sun_tf = sun.GetComponent<Transform>();
        sun_Ownership = sun.GetComponent<Sun_Ownership>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void Change_SunTime()
    {
        //所有権の譲渡がないと、同期できない
        //他のオブジェクトからスクリプトを実行
        sun_Ownership.Change();
        float sunRotateX = slider.value;
        sun_tf.eulerAngles = new Vector3(sunRotateX, 0, 0);
    }
}
