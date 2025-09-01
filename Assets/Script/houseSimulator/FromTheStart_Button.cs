using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromTheStart_Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void switchFromTheStart_Button()
    {
        Config.isInitialStart = true;
    }
}
