using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HidePositionManager : MonoBehaviour
{
    public static HidePositionManager Instance { private set; get; } = null;
    
    private GameObject[] _hideObjects;
    private Dictionary<GameObject,HidePosition> _hidePositions = new Dictionary<GameObject, HidePosition>();

    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _hideObjects = GameObject.FindGameObjectsWithTag("HidePosition");
        _hidePositions.Clear();
        for (int i = 0; i < _hideObjects.Length; i++)
        {
            _hidePositions.Add(_hideObjects[i],_hideObjects[i].GetComponent<HidePosition>());
        }
    }

    public HidePosition GetNearestHidePosition(Vector3 pos)
    {
        _hidePositions = _hidePositions.OrderBy(obj => Vector3.Distance(pos, obj.Key.transform.position)).ToDictionary(x => x.Key, x=> x.Value);
        foreach (var pair in _hidePositions)
        {
            if (pair.Value.IsValidPosition(pos)) return pair.Value;
        }

        return null;
    }
    
}
