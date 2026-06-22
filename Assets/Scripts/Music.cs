using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
 
    private void Awake()
    {
        musicSingleton();
        
    }

    private void musicSingleton() 
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);

        }

        else 
        {

            DontDestroyOnLoad(gameObject);

        }

    
    }

}
