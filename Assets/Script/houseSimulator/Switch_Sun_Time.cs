using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Sun_Time : MonoBehaviour
{
    private bool isSunset = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform tf = GetComponent<Transform>();
        //昼夜の切り替え
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isSunset)
            {
                tf.eulerAngles = new Vector3(90, 0, 0);
            }
            else
            {
                tf.eulerAngles = new Vector3(-90, 0, 0);
            }
            isSunset = !isSunset;
        }

        
    }
}
