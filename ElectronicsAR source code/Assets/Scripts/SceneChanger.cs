using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Obsolete]
public class SceneChanger : MonoBehaviour
{
    public elemtPositions elemtPos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Obsolete]
    public void changeSceneToAnalyser()
    {
        SceneManager.LoadScene("Analyser");
    }

    [Obsolete]
    public void changeSceneToRec()
    {
        elemtPos.listOfFoundElements.Clear();
        elemtPos.foundElements.Clear();
        SceneManager.LoadScene("Recognition");
    }


}
