using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumKeys : MonoBehaviour
{
    // public PonerLlaves scriptAR;

    private TMP_Text textMesh;

    private void Start() {
        textMesh = GetComponent<TMP_Text>();
    }

    private void Update() {
        UpdateText();
    }
    private void UpdateText()
    {
        textMesh.text = $"LLAVE ABIERTA({PonerLlaves.keysFound}/2)";
    }
}
