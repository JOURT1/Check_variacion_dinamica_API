using UnityEngine;
using System.IO;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private Vector3 lastCheckpointPosition;
    public GameObject player;

    private string saveFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            saveFilePath = Application.persistentDataPath + "/checkpoint.txt";
            Debug.Log("Ruta de guardado checkpoint: " + saveFilePath);

            LoadCheckpoint();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
        SaveCheckpointToFile();
    }

    public Vector3 GetCheckpointPosition()
    {
        return lastCheckpointPosition;
    }

    private void SaveCheckpointToFile()
    {
        // Guarda la posición en el archivo como texto "x,y,z"
        string posString = $"{lastCheckpointPosition.x},{lastCheckpointPosition.y},{lastCheckpointPosition.z}";
        try
        {
            File.WriteAllText(saveFilePath, posString);
            Debug.Log($"Checkpoint guardado en archivo: {posString}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error guardando checkpoint: {e.Message}");
        }
    }

    private void LoadCheckpoint()
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                string posString = File.ReadAllText(saveFilePath);
                string[] coords = posString.Split(',');

                if (coords.Length == 3)
                {
                    float x = float.Parse(coords[0]);
                    float y = float.Parse(coords[1]);
                    float z = float.Parse(coords[2]);

                    lastCheckpointPosition = new Vector3(x, y, z);
                    Debug.Log($"Checkpoint cargado desde archivo: {lastCheckpointPosition}");
                }
                else
                {
                    Debug.LogWarning("Archivo checkpoint.txt no tiene el formato correcto.");
                    lastCheckpointPosition = player.transform.position; // fallback
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error cargando checkpoint: {e.Message}");
                lastCheckpointPosition = player.transform.position; // fallback
            }
        }
        else
        {
            lastCheckpointPosition = player.transform.position;
            Debug.Log("No se encontró checkpoint guardado, usando posición inicial.");
        }

        // Mueve el jugador a la posición cargada (checkpoint o inicial)
        if (player != null)
        {
            player.transform.position = lastCheckpointPosition;
        }
    }

    public void ResetCheckpoint()
    {
        // Borrar el archivo si existe
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Archivo checkpoint borrado.");
        }

        // Resetear la posición a la posición inicial del jugador en la escena (puedes guardarla en Awake)
        if (player != null)
        {
            lastCheckpointPosition = player.transform.position;
            player.transform.position = lastCheckpointPosition;
        }
        else
        {
            Debug.LogWarning("Player no asignado en GameController.");
        }
    }

}
