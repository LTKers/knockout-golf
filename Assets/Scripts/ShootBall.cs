using System.Collections;
using UnityEngine;

public class ShootBall : MonoBehaviour {

    public string draggingTag;
    public Camera cam;

    private Vector3 dis;
    private float posX;
    private float posY;

    private bool touched = false;
    private bool dragging = false;

    private Transform toDrag;
    private Rigidbody toDragRigidbody;
    private Vector3 previousPosition;

    private bool drawing=false;

    [SerializeField] private LineRenderer linerenderer;
    void FixedUpdate () {
       
        if (Input.touchCount != 1) {
            dragging = false;
            touched = false;
         
        }

        Touch touch = Input.touches[0];
        Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);

        if (touch.phase == TouchPhase.Began) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(pos);

            if (Physics.Raycast(ray, out hit) && hit.collider.tag == draggingTag) {
                Debug.Log("hitball");
                toDrag = hit.transform;
                previousPosition = toDrag.position;
                toDragRigidbody = toDrag.GetComponent<Rigidbody>();

                dis = cam.WorldToScreenPoint(previousPosition);
              posX = Input.GetTouch(0).position.x - dis.x;
                posY = Input.GetTouch(0).position.y - dis.y;
                drawing=true;
                DrawLine(new Vector3(toDrag.position.x, transform.position.y, toDrag.position.z));
                touched = true;
            }
        }
         if (drawing==true){
               DrawLine(new Vector3(toDrag.position.x, transform.position.y, toDrag.position.z));
        }
        if (touched && touch.phase == TouchPhase.Moved) {
            dragging = true;

            //float posXNow = Input.GetTouch(0).position.x - posX;
        //    float posYNow = Input.GetTouch(0).position.y - posY;
           // Vector3 curPos = new Vector3(posXNow, posYNow, dis.z);

        //    Vector3 worldPos = cam.ScreenToWorldPoint(curPos) - previousPosition;
       //     worldPos = new Vector3(worldPos.x, worldPos.y, 0.0f);

           // toDragRigidbody.velocity = worldPos / (Time.deltaTime * 10);

      //      previousPosition = toDrag.position;
      Ray ray = cam.ScreenPointToRay(pos);
       RaycastHit hit;
                 Physics.Raycast(ray, out hit);
                toDrag = hit.transform;
             

                dis = cam.WorldToScreenPoint(previousPosition);
             
                
                DrawLine(new Vector3(toDrag.position.x, transform.position.y, toDrag.position.z));
                
            }
            Debug.Log(Input.GetTouch(0).position.x+" "+Input.GetTouch(0).position.y);
        

        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)) {
            dragging = false;
            touched = false;
            previousPosition = new Vector3(0.0f, 0.0f, 0.0f);
            drawing=false;
        }

        
        
    }
    private void DrawLine(Vector3 worldPoint){
        Vector3[] positions = {
            transform.position,
            new Vector3(worldPoint.x, transform.position.y, worldPoint.z)
            
        };
        
        linerenderer.SetPositions (positions);
        linerenderer.enabled=true;
       }

 
}