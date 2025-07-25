using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject ball;

    public Camera cam;
    private float offset=0.02f;
    // Start is called before the first frame update
    void Start()
    {
        cam=gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
         Vector3 ballPosScreen = cam.WorldToScreenPoint(ball.transform.position);
      //   Debug.Log(ballPosScreen+" "+Screen.height);
         // Debug.Log(ballPosScreen.y);
         Rigidbody rb = ball.GetComponent<Rigidbody>();
         Vector3 VelocityBall = rb. velocity;
         
         if(ballPosScreen.y<0+150f && VelocityBall.z<0){
        
    
            

            
            transform.position = new Vector3(transform.position.x, transform.position.y , transform.position.z-offset*(Mathf.Abs(VelocityBall.z)));
          
 
         }
         if(ballPosScreen.y>Screen.height-150f && VelocityBall.z>0){
         
    
         
            transform.position = new Vector3(transform.position.x, transform.position.y , transform.position.z+offset*(Mathf.Abs(VelocityBall.z)));
          
 
         }
    }
}
