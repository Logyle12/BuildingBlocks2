using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{

    public void buttonClicked() 
    {

        GetComponent<Animation>().Play("ButtonAnimation");

    }

}
