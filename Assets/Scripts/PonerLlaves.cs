using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

[HelpURL("https://youtu.be/HkNVp04GOEI")]
[RequireComponent(typeof(ARRaycastManager))]
public class PonerLlaves : MonoBehaviour
{
    [SerializeField]
    GameObject keyPrefab1; // Prefab de la llave 1
    [SerializeField]
    GameObject keyPrefab2; // Prefab de la llave 2
    [SerializeField]
    GameObject botonJugar; // Prefab de la llave 2
    [SerializeField]
    GameObject botonPlayAgain; // Prefab de la llave 2
    List<GameObject> spawnedKeys = new List<GameObject>(); // Lista de llaves instanciadas

    public static int keysPlaced = 0;
    public static int keysFound = 0;

    public static bool isPlaying = false;



    /// <summary>
    /// The input touch control.
    /// </summary>
    TouchControls controls;

    // public ARSceneManager arSceneManager;
    public static ARSceneManager Instance;

    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Update() {
        if (keysPlaced == 2) botonJugar.SetActive(true);
        else botonJugar.SetActive(false);

        if (keysFound < 2) botonPlayAgain.SetActive(false);
    }
    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();

        controls = new TouchControls();
        // If there is touch input being performed. call the OnPress function.
        controls.control.touch.performed += ctx =>
        {
            if (ctx.control.device is Pointer device)
            {
                OnPress(device.position.ReadValue());
            }
        };
    }

    private void OnEnable()
    {
        controls.control.Enable();
    }

    private void OnDisable()
    {
        controls.control.Disable();
    }

    public void OnPress(Vector3 position)
    {
        if (keysPlaced == 2 && isPlaying)
        {
            
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Verificar si el objeto clicado es una llave y aún no se ha encontrado
                if (hit.collider.CompareTag("Key") && !hit.collider.GetComponent<KeyScript>().isFound)
                {
                    // Marcar la llave como encontrada
                    // hit.collider.GetComponent<KeyScript>().SetFound(true);

                    // Incrementar el contador de llaves encontradas
                    keysFound++;


                                
                    hit.collider.GetComponent<KeyScript>().SetFound(true);
                    hit.collider.GetComponentInChildren<Renderer>().material.color = Color.red;


                    // Mostrar el contador en la consola (puedes adaptarlo para mostrar en la interfaz de usuario)
                    Debug.Log("Llaves encontradas: " + keysFound);

                    // Si se encuentran las dos llaves, reiniciar el juego
                    if (keysFound == 2)
                    {
                        Debug.Log("¡Has encontrado todas las llaves! Has ganado");
                        botonPlayAgain.SetActive(true);
                    }
                }
            }
        }   
        else if (aRRaycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon) && keysPlaced < 2)
        {
            // Raycast hits are sorted by distance, so the first hit means the closest.
            var hitPose = hits[0].pose;

           // Instantiated the prefab.
            GameObject spawnedKey;

            if (keysPlaced == 0)
            {
                spawnedKey = Instantiate(keyPrefab1, hitPose.position, hitPose.rotation);
                spawnedKey.GetComponentInChildren<Renderer>().material.color = Color.green;

            }
            else
            {
                spawnedKey = Instantiate(keyPrefab2, hitPose.position, hitPose.rotation);
                spawnedKey.GetComponentInChildren<Renderer>().material.color = Color.green;

            }

            spawnedKeys.Add(spawnedKey);

            keysPlaced++;
            Debug.Log("Se han colocado " + keysPlaced + " llaves.");            


            spawnedKey.GetComponent<KeyScript>().SetFound(false);
        }

    }

    public void RestartConfig()
    {
        foreach (GameObject key in spawnedKeys)
        {
            Destroy(key);
        }

        // Limpiar la lista de llaves instanciadas
        spawnedKeys.Clear();

        // Reiniciar el contador de llaves
        keysFound = 0;
        keysPlaced = 0;
    }

    public void GuardarCambios()
    {
        if (keysPlaced == 2)
        {
            isPlaying = true;
            SCManager.instance.LoadScene("Game");
        }
    }

    public void PlayAgain()
    {
        keysFound = 0;
        keysPlaced = 0;
        spawnedKeys.Clear();
        isPlaying = false;
        SCManager.instance.LoadScene("Configuration");

    }
}
