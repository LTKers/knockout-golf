using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour
{
    public bool transition;
    // Start is called before the first frame update
   public ParticleSystem ParticleSystem;


  
   public void OnTriggerEnter(Collider other){
    if (other.CompareTag("Draggable")){
        other.enabled=false;
        GameObject ball=other.gameObject;
      
        Rigidbody rigidbody=ball.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
   rigidbody.angularVelocity = Vector3.zero;  
   ParticleSystem.Play();
        LeanTween.scale(ball, new Vector3(0,0,0), 0.2f);
        //setEase(LeanTweenType.easeInBack)

        if (transition==true){
            GameObject MenuManager  = GameObject.Find("MenuManager");
           MenuManage MM=MenuManager.GetComponent<MenuManage>();
           MM.CloseIntroMenu();
        }
    }
   }


  
   
}
