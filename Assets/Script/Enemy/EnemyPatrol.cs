using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Points pointsPos;
    [SerializeField] private List<Transform> points = new List<Transform>();
    [SerializeField] private string pointKey ="";
    [SerializeField] private Transform point;
    [SerializeField] private int preRand;

    [SerializeField] private BehaviorTree tree;
    public string PointKey => pointKey;
    public Transform Point => point;

    private void Awake()
    {
        tree = GetComponent<BehaviorTree>();
        pointsPos = Transform.FindObjectOfType<Points>();
        points = pointsPos.points;
    }
    public void GetRandomPoint()
    {
        int rand = Random.Range(0, points.Count);
        if(rand == preRand)
        {
            rand += 1;
            rand %= points.Count;
        }
        preRand = rand;

        if(pointKey != "")
        {
            tree.Blackboard.RemoveData(pointKey);
        }

        pointKey = "Point" + (rand+1);
        point = points[rand]; 

        tree.Blackboard.SetOrAddData(pointKey, point.gameObject);
    }
    
}
