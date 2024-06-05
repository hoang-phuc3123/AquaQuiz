using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialValue : MonoBehaviour
{

    public VectorValue startingPosition;

    void Start()
    {
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
