using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextMove : MonoBehaviour
{

    public TMP_Text textComp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textComp.ForceMeshUpdate();
        var textInfo=textComp.textInfo;

        for(int i=0; i<textInfo.characterCount;i++){
            var charInfo=textInfo.characterInfo[i];

            if (!charInfo.isVisible){
                continue;
            }
            var vertices=textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
            for (int j=0; j<4; j++){
                var posOriginal=vertices[charInfo.vertexIndex+j];
                vertices[charInfo.vertexIndex+j] = posOriginal+new Vector3(0, 0.075f*Mathf.Sin(Time.time*1.75f+vertices[charInfo.vertexIndex].x*0.025f)*10f,0);
            }

           
        }
         for (int i=0; i<textInfo.meshInfo.Length;i++){
                var meshInfo=textInfo.meshInfo[i];
                meshInfo.mesh.vertices=meshInfo.vertices;
                textComp.UpdateGeometry(meshInfo.mesh,i);
            }
    }
}
