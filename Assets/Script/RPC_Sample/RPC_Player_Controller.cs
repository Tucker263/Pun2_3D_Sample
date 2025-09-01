using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;



// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
public class RPC_Player_Controller : MonoBehaviourPunCallbacks
{
    void Start()
    {
        
    }
    private void Update()
    {
        // 自身のオブジェクト
        if (photonView.IsMine)
        {
            //移動処理
            var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            transform.Translate(6f * Time.deltaTime * input.normalized);

            //オブジェクトの生成処理、スペースキー
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var position = new Vector3(Random.Range(-3, 3), 2, Random.Range(-3, 3));
                Quaternion rotation = Quaternion.Euler(0, 90, 0);
                GameObject myObject = PhotonNetwork.Instantiate("sphere", position, rotation);
                Debug.Log("自分が生成したオブジェクト: " + myObject.name);
  

            }


             //所有権の譲渡、M
            if (Input.GetKeyDown(KeyCode.M))
            {
                Debug.Log("所有権の譲渡");
  

            }

        }
    }
}
