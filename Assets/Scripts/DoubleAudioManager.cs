using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class DoubleAudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] AudioClip activeAudio;


    void Start()
    {
        GetComponent<AudioSource>().loop = true;

        AudioSource.PlayClipAtPoint(activeAudio, Camera.main.transform.position);

        StartCoroutine(playEngineSound());

    }

    IEnumerator playEngineSound()
    {
        AudioSource.PlayClipAtPoint(activeAudio, Camera.main.transform.position);
        yield return new WaitForSeconds(activeAudio.length);
    }
    }
