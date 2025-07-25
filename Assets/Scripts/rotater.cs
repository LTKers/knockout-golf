using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotater : MonoBehaviour
{
    public float speed;

     public float sizeSpeed;
    public float maxSize;
    public float minSize;
    public float mag;
    int mode=1;

    public float startPosz;
    public float wiggleSpeed;
    public float wiggleDistance;
    public float startPosY;
     Vector3 initialSize;
     public bool blackLines=false;
    // Start is called before the first frame update
    void Start()
    {
          

        LeanTween.rotateAround(gameObject, Vector3.up, -360, speed).setLoopClamp();
        initialSize=gameObject.transform.localScale;
          startPosz=transform.localPosition.z;
          startPosY=transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        
         float zPosition = Mathf.Sin(Time.time * wiggleSpeed) * wiggleDistance;
         if (blackLines==false){
               transform.localPosition = new Vector3(0f,startPosY, zPosition+startPosz);
         }
         else{
               float sizeChange = Mathf.Sin(Time.time * sizeSpeed) * mag;
       transform.localScale = new Vector3(initialSize.x+sizeChange,initialSize.y+sizeChange,initialSize.z+sizeChange);
         }
      
   
        
    }
}
