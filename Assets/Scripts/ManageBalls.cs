using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBalls : MonoBehaviour
{
    public Dictionary<string, bool> BallsInShot = new Dictionary<string, bool>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addToTable(string nameOfBall){
        BallsInShot.Add(nameOfBall, false);
        
    //Now you can access the key and value both separately from this attachStat as:
   

    }

    public void InMotion(string nameOfBall){
        
        
        BallsInShot[nameOfBall]=true;
        
 
    }
     public void DoneMotion(string nameOfBall){
        BallsInShot[nameOfBall]=false;
    }

}
