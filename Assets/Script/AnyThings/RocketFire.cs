using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFire : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Vector3 rotateAngle = new Vector3(0, 45, 0);
    [SerializeField] private Vector3 flyAngle;
    [SerializeField] private GameObject effectImpact;

    [SerializeField] private AudioClip rocketSound;
    private void Awake()
    {
        flyAngle = PlayerCtrl.Instance.PlayerSkill.rocketPoint.forward;
    }
    void Update()
    {
        FireObject();
    }

    public void FireObject()
    {
        transform.Rotate(rotateAngle * rotateSpeed * Time.deltaTime, Space.Self);
  
        transform.Translate(flyAngle * moveSpeed * Time.deltaTime , Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(effectImpact , transform.position - new Vector3(0 , 1 ,0) , Quaternion.identity);
        AudioManager.Instance.PlayClipOneShot(rocketSound);

        PlayerCtrl.Instance.PlayerSkill.isExplode = true;
        PlayerCtrl.Instance.PlayerSkill.collisionPoint = transform.position - new Vector3(0, 1, 0);
        Destroy(gameObject);
        
    }
}
