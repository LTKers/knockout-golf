using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchLocation{
    public int touchId;
    public GameObject line;
public GameObject ball;
    public TouchLocation(int newTouchID, GameObject newLine, GameObject newball){
        touchId=newTouchID;
        line=newLine;
        ball=newball;
    }
}
