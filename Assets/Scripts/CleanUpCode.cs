using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CleanUpCode : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    void OnSceneUnloaded(Scene scene)
    {
        CleanupAllObjects();
    }

    void CleanupAllObjects()
    {
        GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectable");
        foreach (var collectable in collectables)
        {
            Destroy(collectable);
        }
    }
}
