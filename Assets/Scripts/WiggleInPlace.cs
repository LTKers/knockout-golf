using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleInPlace : MonoBehaviour
{
  public float wiggleDistance = -1f;
public float wiggleSpeed = 1.5f;
 public int sign;
 public float startPosx;
  public float startPosz;
 public  bool balltype=false;
 public bool line=false;
  public float Rand1=0.01f;
  public float Rand2=0.01f;
  public float linedistance;
  public float linespeed;
  public float height=27f;
  float  currentX;
  float currentZ;
  bool once=true;
  public bool race=false;
  public float offsetterX;
  public float offsetterZ;
  public float startPX;
  public float changeBall=1f;
  public float starty;
  private float startyball;
  public bool fourteeny=false;
  public bool knockout=false;

  
 void Start(){
    startPosx=transform.localPosition.x;
    startPosz=transform.localPosition.z;

    if(fourteeny==true){
      startyball=14.48f;
    }
    if(knockout==true){
      startyball=30f;
    }
    if (race==true){
      GameObject bigger=transform.parent.gameObject;

      startPX=bigger.transform.position.x;
      starty=0f;
    }

 }
void Update()
{
  gameObject.transform.position=new Vector3(transform.position.x, startyball, transform.position.z);
  if(race==true){
      float xPosition = Mathf.Sin(offsetterX*Time.time * wiggleSpeed*Rand1) * wiggleDistance*Rand2;
     float zPosition = Mathf.Sin(offsetterZ*Time.time * wiggleSpeed*Rand1) * wiggleDistance*Rand2;
    
         transform.localPosition = new Vector3(startPosx+xPosition,transform.position.y, zPosition+startPosz);

         GameObject bigger=transform.parent.gameObject;
           xPosition = changeBall*Mathf.Cos(Time.time * (wiggleSpeed)) * 6;

        bigger.transform.localPosition= new Vector3(startPX+xPosition,starty, bigger.transform.position.z);
  }
  else{

  
    if (balltype==false){
      
    float rad=0f;
    //Random.Range(-0.075f,0.075f);
   
    float xPosition = Mathf.Sin(0.5f*Time.time * wiggleSpeed) * wiggleDistance;
     float zPosition = Mathf.Sin(Time.time * wiggleSpeed) * wiggleDistance;
    transform.localPosition = new Vector3(startPosx+xPosition+rad,transform.position.y, zPosition+startPosz+rad);
    }
    else{
       //Rand1=Random.Range(-0.5f, 0.5f);
       //Rand2=Random.Range(0.5f, 0.75f);
       
      float xPosition = Mathf.Sin(0.5f*Time.time * wiggleSpeed*Rand1) * wiggleDistance*Rand2;
     float zPosition = Mathf.Sin(Time.time * wiggleSpeed*Rand1) * wiggleDistance*Rand2;
    
         transform.localPosition = new Vector3(startPosx+xPosition,transform.position.y, zPosition+startPosz);
    }
  }
    if (line==true){
      LineRenderer linerend=GetComponent<LineRenderer>();
      if (once==true){
currentX=linerend.GetPosition(1).x-linerend.GetPosition(0).x;
currentZ=linerend.GetPosition(1).z-linerend.GetPosition(0).z;
once=false;
      }
      float xPosition;
      float zPosition;
      if (race==false){
          xPosition = Mathf.Sin(0.5f*Time.time * linespeed) * linedistance ;
      zPosition = Mathf.Sin(Time.time * linespeed) * linedistance;
      }
      else{
          xPosition = Mathf.Sin(0.5f*Time.time * linespeed) * linedistance ;
      zPosition = Mathf.Sin(Time.time * linespeed) * 1;
      }
      Vector3[] positions = {
            new Vector3(transform.position.x, height, transform.position.z),
            new Vector3(xPosition+transform.position.x+currentX, height, zPosition+transform.position.z+currentZ)
            
        };
        
        linerend.SetPositions (positions);
        linerend.enabled=true;
    }
  
}
}
