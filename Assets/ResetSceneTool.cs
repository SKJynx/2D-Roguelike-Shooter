using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSceneTool : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ResetSceneFunction();
        }
    }

    void ResetSceneFunction()
    {

        SceneManager.LoadScene("SampleScene");
    }
}
