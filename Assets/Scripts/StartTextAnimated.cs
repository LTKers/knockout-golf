using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTextAnimated : MonoBehaviour
{
    public GameObject ball;
    public MultiTouchFinalVersion script;
    bool flash=true;
    Animator animate;

    // Start is called before the first frame update
    void Start()
    {
        animate=gameObject.GetComponent<Animator>();
      animate.SetBool("start", true);
      animate.SetBool("stop", false);
      StartCoroutine(start());
        script=ball.GetComponent<MultiTouchFinalVersion>();
    }

    // Update is called once per frame
    void Update()
    {
        if (script.shot==true){
            flash=false;
        }
        if (flash==false){
            Debug.Log("ya");
            animate.SetBool("stop", true);
        }
    }

      private IEnumerator start() {
    
        yield return new WaitForSeconds(0.5f); 
         animate.SetBool("start", false);
      }

      private IEnumerator stop() {
    
        yield return new WaitForSeconds(0.5f); 
         animate.SetBool("stop", false);
      }
}
