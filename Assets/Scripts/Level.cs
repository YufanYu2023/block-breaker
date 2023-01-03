using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    [SerializeField] int breakaleBlocks;//seralize for debugging

    //cached reference
    SceneLoader SceneLoader;


    private void Start()
    {
        SceneLoader = FindObjectOfType<SceneLoader>();
    }
    public void CountBlocks()
    {
        breakaleBlocks++;
    }

    public void BlockDestroyed()
    {
        breakaleBlocks--;
        if(breakaleBlocks<=0)
        {
            SceneLoader.LoadNextScene();
        }
    }
}
