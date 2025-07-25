using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Intromenu;
    public GameObject holeAnimation;
      Animator holeanimate;
      public Camera cam;
      public GameObject camera;
      public GameObject IntroHole;

      public GameObject SelectMenu;
      public Selection selectionScript;
    void Start()
    {
        Intromenu.SetActive(true);
        SelectMenu.SetActive(false);
      
    }

    public void CloseIntroMenu(){
          holeanimate=holeAnimation.GetComponent<Animator>();
           Vector3 pos= cam.WorldToScreenPoint(IntroHole.transform.position);
           holeAnimation.transform.position=pos;
            holeanimate.SetBool("Animate", true);
            StartCoroutine(WaitIntroClose());
    }

    public void OpenSelectMenu(){
        camera.transform.position=new Vector3(0,camera.transform.position.y,0);
          holeanimate=holeAnimation.GetComponent<Animator>();
          holeanimate.transform.position=new Vector3(Screen.width/2,Screen.height/2,0);
         SelectMenu.SetActive(true);
         holeanimate.SetBool("Animate", true);
         StartCoroutine(OpenSelectMenuWait());
    }

      IEnumerator WaitIntroClose()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.7f);
        holeanimate=holeAnimation.GetComponent<Animator>();
        holeanimate.SetBool("Animate", false);
        
        yield return new WaitForSeconds(0.3f);
        Intromenu.SetActive(false);
        OpenSelectMenu();
    }
    IEnumerator OpenSelectMenuWait(){
             yield return new WaitForSeconds(0.7f);
        holeanimate=holeAnimation.GetComponent<Animator>();
        holeanimate.SetBool("Animate", false);
         Debug.Log("gragd");
        selectionScript.UIENABLE();
        

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
