using System;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandleLevelComplete();
        }
    }

    private void HandleLevelComplete()
    {
        GameManager.Instance.LevelFinished();
    }
}
