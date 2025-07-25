using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTouchFinalVersion : MonoBehaviour
{
    public GameObject line;
     private Vector3 pos;
    public List<TouchLocation> touches=new List<TouchLocation>();
public Material whiteOutline;
private MaterialPropertyBlock propertyBlock;
public Touch t;

 public float maxdistance=30f;
 public float maxpower=100f;
 public float distance=0f;
 public Vector3 direction;
 public GameObject placeholder;
 public bool activeLine=false;
 public bool readyshoot=true;    // Update is called once per frame
 public bool shotHappening=false;
public bool shot=false;
  ManageBalls ballmanager;
 public GameObject ballmanagerObject;
 public float power=10f;
 private Transform childBall;
//WHY DOES IZT NOT WORK????
 float length;
 void Start(){
  ballmanager=ballmanagerObject.GetComponent<ManageBalls>();
    ballmanager.addToTable(gameObject.name);
    shot=false;
    childBall=gameObject.transform.GetChild(0);
 }
    void FixedUpdate(){
        
    }
    private IEnumerator resetShot() {
    
        yield return new WaitForSeconds(1.5f); //wait 2 seconds
        //do thing
        
        LeanTween.value(gameObject, 0.21f, 0.35f, 0.2f)
        .setOnUpdate((value)=>
        {
            if (propertyBlock == null){
            propertyBlock = new MaterialPropertyBlock();
                   }

         //Get a renderer component either of the own gameobject or of a child
         Renderer renderer = GetComponentInChildren<Renderer>();
        //set the color property
        propertyBlock.SetFloat("_OutlineThicknessWhite", value);
        //apply propertyBlock to renderer
        renderer.SetPropertyBlock(propertyBlock);

        });
        readyshoot=true;
    }
    void Update()
    {
        int i=0;
        while (i<Input.touchCount){
            t=Input.GetTouch(i);
            if (t.phase==TouchPhase.Began && ballmanager.BallsInShot[gameObject.name]==false){
              if (readyshoot==true){
                  
                  
            //    Debug.Log("touch began");
                 Ray ray = Camera.main.ScreenPointToRay(t.position);
                // create a logical plane at this object's position
                // and perpendicular to world Y:
                Plane plane = new Plane(Vector3.up, transform.position);
        

                RaycastHit hit;
                if (plane.Raycast(ray, out distance)&& Physics.Raycast(ray, out hit) && hit.collider.tag == "Draggable"){ // if plane hit...
              //  Debug.Log("hit");
              MultiTouchFinalVersion multitouch=hit.transform.gameObject.GetComponent<MultiTouchFinalVersion>();
          
             // if (multitouch.shotHappening==false){
                 
             if(GameObject.ReferenceEquals(hit.transform.gameObject, gameObject)){
               
                   if (propertyBlock == null){
            propertyBlock = new MaterialPropertyBlock();
                   }
        //Get a renderer component either of the own gameobject or of a child
        Renderer renderer = GetComponentInChildren<Renderer>();
        //set the color property
        propertyBlock.SetFloat("_OutlineThicknessWhite", 0.35f);
        //apply propertyBlock to renderer
        renderer.SetPropertyBlock(propertyBlock);
       ballmanager.InMotion(gameObject.name);
             }
                    shotHappening=true;
                  activeLine=true;
                    pos = ray.GetPoint(distance);
                     Vector3 positionOfBall=hit.collider.transform.position;
                   
                  
                    touches.Add(new TouchLocation(t.fingerId, createLine(positionOfBall, t,  hit.transform.gameObject),hit.transform.gameObject));
                  
                      
                      TouchLocation thisTouch = touches.Find(TouchLocation => TouchLocation.touchId==t.fingerId);
                     //  Debug.Log(thisTouch.ball.name);
                       thisTouch.line.transform.parent=hit.collider.transform;
                    // get the point
                    // pos has the position in the plane you've touched
            
                     
            //     }
            }
              }
                
            }
            else if(t.phase==TouchPhase.Ended){
                 activeLine=false;
                 shotHappening=false;
                 shot=true;
               TouchLocation thisTouch = touches.Find(TouchLocation => TouchLocation.touchId==t.fingerId);
               if(GameObject.ReferenceEquals(thisTouch.ball, gameObject)){
                
                   if (propertyBlock == null){
            propertyBlock = new MaterialPropertyBlock();
                   }
        LeanTween.value(gameObject, 0.35f, 0.21f, 0.2f)
        .setOnUpdate((value)=>
        {
            if (propertyBlock == null){
            propertyBlock = new MaterialPropertyBlock();
                   }

         //Get a renderer component either of the own gameobject or of a child
         Renderer renderer = GetComponentInChildren<Renderer>();
         Debug.Log(renderer);
        //set the color property
        propertyBlock.SetFloat("_OutlineThicknessWhite", value);
        //apply propertyBlock to renderer
        renderer.SetPropertyBlock(propertyBlock);

        });
          
         ballmanager.DoneMotion(gameObject.name);
                Rigidbody rb=thisTouch.ball.GetComponent<Rigidbody>();
              
                  rb.velocity= -1*direction*length*Mathf.Pow(power, 2.3f);
                  
                  readyshoot=false;
                   StartCoroutine(resetShot());
              



             }
                Ray ray = Camera.main.ScreenPointToRay(t.position);
                Plane plane = new Plane(Vector3.up, transform.position);
              
                RaycastHit hit;
                Vector3 pointTouch;
                if (plane.Raycast(ray, out distance)&& Physics.Raycast(ray, out hit)){ // if plane hit...
                //Debug.Log("hit");
                     pointTouch = hit.point;
                     //Debug.Log(pointTouch);
                     Vector2 dir=new Vector2(transform.position.x, transform.position.z)-new Vector2(pointTouch.x, pointTouch.z);
                 }
               

                 
               Destroy(thisTouch.line);
               
               touches.RemoveAt(touches.IndexOf(thisTouch));
              
            }
            else if(t.phase==TouchPhase.Moved){
             
                
                Ray ray = Camera.main.ScreenPointToRay(t.position);
                Plane plane = new Plane(Vector3.up, transform.position);
                float distance = 0; // this will return the distance from the camera

                RaycastHit hit;
                if (plane.Raycast(ray, out distance)&& Physics.Raycast(ray, out hit)){ // if plane hit...
            //    Debug.Log("hit");
                     pos = ray.GetPoint(distance);
                      TouchLocation thisTouch = touches.Find(TouchLocation => TouchLocation.touchId==t.fingerId);
                //thisTouch.line.transform.position=pos;
              //  Debug.Log(thisTouch.ball.name);
                if(GameObject.ReferenceEquals(thisTouch.ball, gameObject)){
                    distance=Vector2.Distance(new Vector2(thisTouch.ball.transform.position.x,thisTouch.ball.transform.position.z), new Vector2(hit.point.x, hit.point.z));

                  
                   if (distance<=maxdistance){
                      direction=(new Vector3(hit.point.x, 0, hit.point.z)-new Vector3(thisTouch.ball.transform.position.x, 0, thisTouch.ball.transform.position.z)).normalized;
                     DrawLine(thisTouch.line.transform.position, pos, thisTouch.line.GetComponent<LineRenderer>());
                }
                if(distance>maxdistance){
                   // Debug.Log(thisTouch.ball.name);
                   distance=maxdistance;
               direction=(new Vector3(hit.point.x, 0, hit.point.z)-new Vector3(thisTouch.ball.transform.position.x, 0, thisTouch.ball.transform.position.z)).normalized;
                //direction=direction*-1;
                   DrawLine(thisTouch.ball.transform.position, thisTouch.ball.transform.position+direction*maxdistance, thisTouch.line.GetComponent<LineRenderer>());

                }
                length=distance;
                 Debug.Log(distance);
                }
                
              
                 }
              
            }
            if (activeLine=true){
                 Ray ray = Camera.main.ScreenPointToRay(t.position);
                Plane plane = new Plane(Vector3.up, transform.position);
                float distance = 0; // this will return the distance from the camera

                RaycastHit hit;
                if (plane.Raycast(ray, out distance)&& Physics.Raycast(ray, out hit)){ // if plane hit...
            //    Debug.Log("hit");
                     pos = ray.GetPoint(distance);
                      TouchLocation thisTouch = touches.Find(TouchLocation => TouchLocation.touchId==t.fingerId);
                //thisTouch.line.transform.position=pos;
              //  Debug.Log(thisTouch.ball.name);
                if(GameObject.ReferenceEquals(thisTouch.ball, gameObject)){
                    distance=Vector2.Distance(new Vector2(thisTouch.ball.transform.position.x,thisTouch.ball.transform.position.z), new Vector2(hit.point.x, hit.point.z));
                  

                   if (distance<=maxdistance){
                      direction=(new Vector3(hit.point.x, 0, hit.point.z)-new Vector3(thisTouch.ball.transform.position.x, 0, thisTouch.ball.transform.position.z)).normalized;
                     DrawLine(thisTouch.line.transform.position, pos, thisTouch.line.GetComponent<LineRenderer>());
                }
                else{
                   // Debug.Log(thisTouch.ball.name);
                      
               direction=(new Vector3(hit.point.x, 0, hit.point.z)-new Vector3(thisTouch.ball.transform.position.x, 0, thisTouch.ball.transform.position.z)).normalized;
                //direction=direction*-1;
                   DrawLine(thisTouch.ball.transform.position, thisTouch.ball.transform.position+direction*maxdistance, thisTouch.line.GetComponent<LineRenderer>());

                }
                }
            }
            ++i;
        }

        
        GameObject createLine(Vector3 positionclick, Touch t, GameObject hitball){
        //    Debug.Log("make");
        if (GameObject.ReferenceEquals(hitball, gameObject)){
            GameObject l=Instantiate(line) as GameObject;
            l.name = "Touch"+t.fingerId;
            l.transform.position=positionclick;
            LineRenderer linerender=l.GetComponent<LineRenderer>();
            DrawLine(positionclick, positionclick, linerender);
            return l;
        }
        else{
          return placeholder;
        }
        }

        void DrawLine(Vector3 origin, Vector3 worldPoint, LineRenderer linerenderer){
        Vector3[] positions = {
            new Vector3(origin.x, childBall.position.y-(childBall.localScale.y/2)-2f, origin.z),
            new Vector3(worldPoint.x, childBall.position.y-(childBall.localScale.y/2), worldPoint.z)
            
        };
        
        linerenderer.SetPositions (positions);
        linerenderer.enabled=true;
       }
}
}
}

