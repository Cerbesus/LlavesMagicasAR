using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

[Serializable]
public class ARData
{
    public List<SerializableVector3> keyPositions = new List<SerializableVector3>();
    // Agrega más datos que necesites serializar
}

public class ARSceneManager : MonoBehaviour
{
    public static ARSceneManager Instance;

    public ARData arData = new ARData();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Destroy(gameObject);
        }
    }

    public void SaveKeyPositions()
    {
        //guardar la posicion de cada llave instanciada
        GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");

        arData.keyPositions.Clear();

        foreach (GameObject key in keys)
        {
            SerializableVector3 serializableVector = new SerializableVector3(key.transform.position);
            arData.keyPositions.Add(serializableVector);
        }

        foreach (SerializableVector3 position in arData.keyPositions)
        {
            Debug.Log("Key position: " + position.ToVector3());
        }
    }
}

[Serializable]
public struct SerializableVector3
{
    public float x, y, z;

    public SerializableVector3(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}
