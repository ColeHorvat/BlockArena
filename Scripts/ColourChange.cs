using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChange : MonoBehaviour
{

    Camera cam;
    float changeTimeTotal = 0.4196f * 2;
    float changeTime;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;
        
        if(changeTime <= 0)
        {
            cam.backgroundColor =  Random.ColorHSV();
            changeTime = changeTimeTotal;
        }
        
    }
}
