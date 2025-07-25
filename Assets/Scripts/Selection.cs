using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public GameObject LeftArrow;
    public GameObject RightArrow;

    public GameObject LeftButton;
    public GameObject RightButton;
    public GameObject LeftWhite;
    public GameObject RightWhite;
    public GameObject[] Options;
    public int currentOption=0;
        
    public Camera cam;
    public GameObject[] buttons;
    public float buttonOffset=0.0f;
    public  Color startColor, endColor;
      [SerializeField] float time=10f;

private int previous;
  public float dis, speed;
   private bool ready=true;
    // Start is called before the first frame update
    void Start()
    {
      for(int i=1; i<buttons.Length;i++){
          buttons[i].SetActive(false);
          Options[i].SetActive(false);
        }
      Options[currentOption].SetActive(true);
       Options[currentOption].transform.position=new Vector3(0f,   Options[currentOption].transform.position.y,   Options[currentOption].transform.position.z);
       buttons[currentOption].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        LeftArrow.transform.position=LeftArrow.transform.position+new Vector3(0.002f*Mathf.Sin(Time.time*1.75f+LeftArrow.transform.position.y*0.075f)*10f, 0,0);
        RightArrow.transform.position=RightArrow.transform.position+new Vector3(-0.002f*Mathf.Sin(Time.time*1.75f+LeftArrow.transform.position.y*0.075f)*10f, 0,0);
            buttons[currentOption].SetActive(true);
           Vector3 screenPosRight = cam.WorldToScreenPoint(RightArrow.transform.position);
           Vector3 screenPosLeft = cam.WorldToScreenPoint(LeftArrow.transform.position);
           
            LeftButton.transform.position=new Vector3(screenPosLeft.x+buttonOffset, LeftButton.transform.position.y, LeftButton.transform.position.z);
             RightButton.transform.position=new Vector3(screenPosRight.x-buttonOffset, RightButton.transform.position.y, RightButton.transform.position.z);
        Debug.Log(currentOption);

           Vector3 currentDisplay = cam.WorldToScreenPoint(Options[currentOption].transform.position);
            buttons[currentOption].transform.position=new Vector3(currentDisplay.x, currentDisplay.y, currentDisplay.z);
//Debug.Log(currentOption);
   }
    
    public void UIENABLE(){
        Debug.Log("UHGIUAD");
          LeftButton.SetActive(true);
        RightButton.SetActive(true);
        
      
    }
    public void MoveRight(){
      if(ready==true){
ready=false;
 Debug.Log("Riught");
        
         LeanTween.value(RightWhite, 0.1f, 1, time)
        .setEasePunch()
        .setOnUpdate((value)=>
        {
          MeshRenderer MR=RightWhite.GetComponent<MeshRenderer>();
        MR.material.color=Color.Lerp(startColor, endColor, value);
        });
         int temp=currentOption;
        currentOption=currentOption+1;
        if( currentOption==Options.Length){
        currentOption=0;
        }
     switchOption(temp, currentOption, -1);
      }
       

        
    }
    public void MoveLeft(){
      if(ready==true){
        ready=false;
         Debug.Log("Left");
 int temp=currentOption;
 currentOption=currentOption-1;
 if(currentOption==-1){
  currentOption=Options.Length-1;
 }
       LeanTween.value(LeftWhite, 0.1f, 1, time)
        .setEasePunch()
        .setOnUpdate((value)=>
        {
          MeshRenderer ML=LeftWhite.GetComponent<MeshRenderer>();
        ML.material.color=Color.Lerp(startColor, endColor, value);
        });


    switchOption(temp, currentOption, 1);
      }


    }

    public void switchOption(int before,int after, int direction){
       previous=before;
          LeanTween.moveX(Options[before], dis*direction, speed).setEase(LeanTweenType.easeOutElastic);
          Options[after].SetActive(true);
          
          Options[after].transform.position=new Vector3(30f*direction*-1,    Options[after].transform.position.y,   Options[after].transform.position.z);
             LeanTween.moveX(Options[after],0f, speed).setEase(LeanTweenType.easeOutElastic);
             buttons[currentOption].SetActive(false);
         StartCoroutine(WaitDisable(Options[before]));
         
    }

  

    IEnumerator WaitDisable(GameObject obj){
         yield return new WaitForSeconds(0.1f);
         obj.SetActive(false);
         yield return new WaitForSeconds(0.5f);
         ready=true;

        
    }


}
