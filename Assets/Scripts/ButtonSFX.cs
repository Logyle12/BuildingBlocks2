using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip buttonClickedSFX;

    public void buttonClickedSound()
    {

        AudioSource.PlayClipAtPoint(buttonClickedSFX, Camera.main.transform.position);

    }
}
