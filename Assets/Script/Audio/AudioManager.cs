using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private ObjectPoolingAudio objectPoolingAudio;

    private void Start()
    {
        objectPoolingAudio = GetComponent<ObjectPoolingAudio>();
    }

    public void PlayClipOneShot(AudioClip clip)
    {
        AudioSource audioSource = objectPoolingAudio.GetObject().transform.GetComponent<AudioSource>();

        //audioSource.transform.position = Camera.main.transform.position; 

        audioSource.PlayOneShot(clip);

        StartCoroutine(BackToPool(audioSource.gameObject, clip.length));

    }

    IEnumerator BackToPool(GameObject audio , float length)
    {
        yield return new WaitForSeconds(length);
        objectPoolingAudio.ReturnObject(audio);
    }
}
