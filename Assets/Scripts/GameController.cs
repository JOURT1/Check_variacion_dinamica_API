using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private Vector3 lastCheckpointPosition;
    public GameObject player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
    }

    public Vector3 GetCheckpointPosition()
    {
        return lastCheckpointPosition;
    }
}
