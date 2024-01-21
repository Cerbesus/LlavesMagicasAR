using UnityEngine;

public class KeyInstantiator : MonoBehaviour
{
    public GameObject keyPrefab; // Prefab del objeto que quieres instanciar

    // public static PonerLlaves ponerLlaves;

    void Start()
    {
        // Accede al ARSceneManager para obtener las posiciones guardadas
        ARSceneManager arSceneManager = ARSceneManager.Instance;

        // Itera sobre las posiciones guardadas y instancia los objetos
        foreach (SerializableVector3 position in arSceneManager.arData.keyPositions)
        {
            Instantiate(keyPrefab, position.ToVector3(), Quaternion.identity);
        }

        foreach (GameObject llave in GameObject.FindGameObjectsWithTag("Key"))
        {
            llave.GetComponentInChildren<Renderer>().material.color = Color.green;
        }
    }
}
