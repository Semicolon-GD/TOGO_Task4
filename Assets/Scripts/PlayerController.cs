using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float verticalMovementSensitivity;
    [SerializeField] private Transform stackPoint;
    [SerializeField] private Animator playerAnimator;
    
    private List<GameObject> _stackList = new List<GameObject>();
    private float _givenSpeed;
    readonly float  _horizontalRange = 4.2f;
    private float _horizontalOffset;
    private Rigidbody _rigidbody;
    

    private void Start()
    {
        _rigidbody=gameObject.GetComponent<Rigidbody>();
        _givenSpeed = forwardSpeed;
        forwardSpeed = 0;
    }

    private void Update()
    {
        transform.position += Vector3.forward * (forwardSpeed * Time.deltaTime);
    }
    
    #region Event Subscription
    
    private void OnEnable()
    {
        InputController.OnFirstClick += StartMovement;
        InputController.Dragging += HorizontalMovement;
        ObstacleController.Jump += VerticalMovement;
        ObstacleController.CollectibleCollected+= StackPoints;
        ObstacleController.MinusPoint += MinusPoints;
        ScoreSystem.GameOver += GameOver;
        ScoreSystem.GameWon += GameWon;
    }

    private void OnDisable()
    {
        InputController.OnFirstClick -= StartMovement;
        InputController.Dragging -= HorizontalMovement;
        ObstacleController.Jump -= VerticalMovement;
        ObstacleController.CollectibleCollected-= StackPoints;
        ObstacleController.MinusPoint -= MinusPoints;
        ScoreSystem.GameOver -= GameOver;
        ScoreSystem.GameWon -= GameWon;
    }

    #endregion
    
    #region Event Methods
    
    private void StartMovement()
    {
        forwardSpeed= _givenSpeed;
        playerAnimator.SetBool("isRunning",true);
    }
    
    private void HorizontalMovement(float horizontal)
    {
        transform.position += Vector3.right * (horizontal * verticalMovementSensitivity * Time.deltaTime);
        var playerPosition = transform.position;
        playerPosition.x = Mathf.Clamp(transform.position.x,-_horizontalRange,_horizontalRange);
        transform.position = playerPosition;
    }
    
    private void VerticalMovement()
    {
        _rigidbody.AddForce(0,10,0,ForceMode.Impulse);
        playerAnimator.SetTrigger("Jump");
    }
    
    void StackPoints(GameObject collectible)
    {
        _stackList.Add(collectible);
        collectible.transform.SetParent(transform);
        collectible.transform.localPosition = stackPoint.localPosition;
        stackPoint.localPosition += Vector3.up;

    }

    void MinusPoints(int point)
    {
        
        while (point > 0 && _stackList.Count > 0)
        {
            point--;
            Destroy(_stackList[^1].gameObject);
            stackPoint.localPosition -= Vector3.up;
            _stackList.Remove(_stackList[^1].gameObject);
        }

    }
    void GameOver()
    {
        InputController.Dragging -= HorizontalMovement;
        playerAnimator.SetTrigger("Fail");
        forwardSpeed = 0;
    }
    
    void GameWon()
    {
        transform.Rotate(0,180,0);
        playerAnimator.SetTrigger("Win");
        forwardSpeed = 0;
    }
    
    #endregion


    
}
