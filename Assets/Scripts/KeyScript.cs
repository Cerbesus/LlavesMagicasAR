using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public bool isFound = false;
    public float rotationSpeed = 30f; // Velocidad de rotación


    public void SetFound(bool found)
    {
        isFound = found;
    }

    private void Update() {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
