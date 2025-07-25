using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMask : MonoBehaviour
{
    public GameObject[] maskObj;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<maskObj.Length; i++){
            maskObj[i].GetComponent<MeshRenderer>().material.renderQueue=3002;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
