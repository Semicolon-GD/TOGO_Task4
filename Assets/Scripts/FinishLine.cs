using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.CompareTag("Player");
        if (player == false)
            return;
        ScoreSystem.CalculateResult();
    }
}
