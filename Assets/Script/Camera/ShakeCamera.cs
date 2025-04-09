using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] private Transform shakeTransform;
    [SerializeField] private float shakeMag;
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeSpeed;

    private Coroutine shakeCoroutine;
    private bool isShake;

    public void StartShake()
    {
        if(shakeCoroutine == null)
        {
            isShake = true;
            shakeCoroutine = StartCoroutine(Shake());
        }
    }
    IEnumerator Shake()
    {
        yield return new WaitForSeconds(shakeDuration);
        isShake = false;
        shakeCoroutine = null;
    }

    private void LateUpdate()
    {
        if (isShake)
        {
            Vector3 shakeAmt = new Vector3(Random.value, Random.value, Random.value) * shakeMag *
                (Random.value > 0.5f ? 1 : -1);
            shakeTransform.position += shakeAmt;
        }
    }
}
