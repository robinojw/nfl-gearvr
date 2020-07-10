using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Remove : MonoBehaviour { 

// Use this for initialization
void Start()
    {

        var components = GetComponents<Movement>();
        foreach (var t in components)
        {
            if (t is Transform)
                continue;
            Destroy(t);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
