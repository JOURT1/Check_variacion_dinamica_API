using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.SaveCheckpoint(transform.position);
            gameObject.SetActive(false);
        }
    }
}
