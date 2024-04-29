using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWonPanel;

    [SerializeField] private List<GameObject> tutorialObjects;
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        tutorialPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(false);
        gamePanel.SetActive(false);
    }

    private void Update()
    {
        foreach (var item in tutorialObjects)
        {
            item.transform.Rotate(0.2f,1f,0.4f );
        }
    }

    private void OnEnable()
    {
        InputController.OnFirstClick += OnFirstClick;
        ScoreSystem.GameOver += GameOver;
        ScoreSystem.GameWon += GameWon;
    }

    private void OnDisable()
    {
        InputController.OnFirstClick -= OnFirstClick;
        ScoreSystem.GameOver -= GameOver;
        ScoreSystem.GameWon -= GameWon;
    }
    
    private void OnFirstClick()
    {
        tutorialPanel.SetActive(false);
        gamePanel.SetActive(true);
    }
    
    private void GameOver()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
    
    private void GameWon()
    {
        gamePanel.SetActive(false);
        gameWonPanel.SetActive(true);
    }
}
