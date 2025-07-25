using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchtry : MonoBehaviour
{

    private Vector3 pos;
    public int fingerIndex;
    public bool beingDragged;
    public LineRenderer linerend;

    public GameObject[] grabbed = new GameObject[2];


    private Dictionary<int, GameObject> balls = new Dictionary<int, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      int touchcounts=Input.touchCount;
     

      
          if(touchcounts > 0)
          {
             for(int i=0; i<touchcounts; i++){
               if(Input.GetTouch(i).phase == TouchPhase.Began){

               
                     // create ray from the camera and passing through the touch position:
                     Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                     // create a logical plane at this object's position
                     // and perpendicular to world Y:
                     Plane plane = new Plane(Vector3.up, transform.position);
                     float distance = 0; // this will return the distance from the camera

                        RaycastHit hit;
                           

                     if (plane.Raycast(ray, out distance)&& Physics.Raycast(ray, out hit) && hit.collider.tag == "Draggable"){ // if plane hit...
                           pos = ray.GetPoint(distance);
                        Debug.Log(Vector2.Distance(transform.position, pos));
                        linerend=hit.transform.gameObject.GetComponent<LineRenderer>();
                        DrawLine(pos, linerend);
                        // get the point
                        // pos has the position in the plane you've touched
                     }
               }
               if(Input.GetTouch(i).phase == TouchPhase.Moved){
                  Debug.Log("moving");
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                     // create a logical plane at this object's position
                     // and perpendicular to world Y:
                     Plane plane = new Plane(Vector3.up, transform.position);
                     float distance = 0; // this will return the distance from the camera

                        RaycastHit hit;
                           

                     if (plane.Raycast(ray, out distance)){ // if plane hit...
                           pos = ray.GetPoint(distance);
                        Debug.Log(Vector2.Distance(transform.position, pos));
                       
                        DrawLine(pos, linerend);
                        // get the point
                        // pos has the position in the plane you've touched
                     }
               }

     

             }
   }
   
}
private void DrawLine(Vector3 worldPoint, LineRenderer linerenderer){
        Vector3[] positions = {
            transform.position,
            new Vector3(worldPoint.x, transform.position.y, worldPoint.z)
            
        };
        
        linerenderer.SetPositions (positions);
        linerenderer.enabled=true;
       }
}