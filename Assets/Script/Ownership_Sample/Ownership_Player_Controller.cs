using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;



// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
public class Ownership_Player_Controller : MonoBehaviourPunCallbacks
{

    void Start()
    {

    }
    void Update()
    {
        // 自身のオブジェクト
        if (photonView.IsMine)
        {
            //移動処理
            //var input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
            var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            transform.Translate(6f * Time.deltaTime * input.normalized);

            //スペースキー、オブジェクトの生成
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var position = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
                PhotonNetwork.Instantiate("Ownership_Cube", position, Quaternion.identity);

            }

        }

        //所有権の譲渡,P
        if (Input.GetKeyDown(KeyCode.P))
        {
            photonView.RequestOwnership();
            Debug.Log("オブジェクトの所有権の譲渡");
        }
    }


}
