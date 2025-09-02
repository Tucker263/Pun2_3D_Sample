using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class Ownership_PlayerName_Display : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private void Start()
    {
        var nameLabel = GetComponent<TextMeshPro>();
        // プレイヤー名とプレイヤーIDを表示する
        nameLabel.text = $"{photonView.Owner.NickName}({photonView.OwnerActorNr})";
        nameLabel.color = Color.black;
        nameLabel.transform.Rotate(30, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
