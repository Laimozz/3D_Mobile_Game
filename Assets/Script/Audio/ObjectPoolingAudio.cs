using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingAudio : MonoBehaviour
{
    [SerializeField] private Queue<GameObject> objectPool = new Queue<GameObject>();
    [SerializeField] private GameObject audioPrefap;
    [SerializeField] private int poolSize;

    private void Start()
    {
        CreatePool();
    }

    public void CreatePool()
    {
        for(int i =  0; i < poolSize; i++)
        {
            GameObject audio = Instantiate(audioPrefap);
            audio.SetActive(false);
            objectPool.Enqueue(audio);
        } 
    }

    public GameObject GetObject()
    {
        if(objectPool.Count > 0)
        {
            GameObject obj =  objectPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(audioPrefap);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}
