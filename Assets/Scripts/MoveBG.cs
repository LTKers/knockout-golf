using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBG : MonoBehaviour
{
    public float xmove=1f;
     public float ymove=1f;
    // Start is called before the first frame update
    private float xOffset;
    private float yOffset;
        private Material material;
        public int option=1;
        public float gearspeed;
        public float geardistance;
        public bool once=false;
        public float startPosz;
    void Start()
    {
        material = GetComponent<Renderer>().material;
        xOffset=material.mainTextureOffset.x;
        yOffset =material.mainTextureOffset.y;
        if (option==2){
            GameObject gear=GameObject.Find("gearSpin");
            GameObject Lines=GameObject.Find("Lines");
             LeanTween.rotateAround(gear, Vector3.up, -360, 5f).setLoopClamp();
             LeanTween.rotateAround(Lines, Vector3.up, -360, 5f).setLoopClamp();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (option==1){

        
        xOffset+=Time.deltaTime*xmove;
        yOffset+=Time.deltaTime*ymove;
        material.SetTextureOffset("_BaseMap", new Vector2(xOffset, yOffset));
        }
        if (option==2){
            GameObject gear= GameObject.Find("gearSpin");
            
           
              if (once==false){
                startPosz=gear.transform.position.z;
                once=true;
              }
               float zPosition = Mathf.Sin(Time.time * gearspeed) * geardistance;
         gear.transform.localPosition = new Vector3(gear.transform.position.x,gear.transform.position.y, startPosz+zPosition);
            yOffset+=Time.deltaTime*ymove;
            xOffset+=Time.deltaTime*xmove;
            material.SetTextureOffset("_BaseMap", new Vector2(xOffset, yOffset));
        }
       
    }
}
