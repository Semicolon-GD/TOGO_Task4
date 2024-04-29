using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum PropType
{
    ResetObstacle,
    MinusObstacle,
    JumpObstacle,
    Collectible,
}
public class ObstacleController : MonoBehaviour
{
    public PropType propType;
    
    public delegate void Action3(GameObject collectible);
    public delegate void Action4(int point);
    
    public static event Action Jump ;
    public static event Action3 CollectibleCollected ;
    public static event Action4 MinusPoint;
    private void OnTriggerEnter(Collider other)
    {
        var player = other.CompareTag("Player");
        if (player == false)
            return;
        switch (propType)
        {
            case PropType.ResetObstacle:
                MinusPoint?.Invoke(ScoreSystem.Score);
                break;
            case PropType.MinusObstacle:
                MinusPoint?.Invoke(1);
                break;
            case PropType.JumpObstacle:
                Jump?.Invoke();
                break;
            case PropType.Collectible:
                CollectibleCollected?.Invoke(gameObject);
                break;
        }
       
    }

    private void Update()
    {
        if (propType == PropType.Collectible)
        {
            transform.Rotate(Vector3.up, 100 * Time.deltaTime);
        }
        
    }
}
