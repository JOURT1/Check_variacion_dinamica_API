using UnityEngine;
using UnityEngine.UI;

public class ResetButtonHandler : MonoBehaviour
{
    public Button resetButton;

    private void Start()
    {
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(OnResetButtonClicked);
        }
        else
        {
            Debug.LogWarning("Reset Button no asignado en ResetButtonHandler.");
        }
    }

    private void OnResetButtonClicked()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.ResetCheckpoint();
        }
    }
}
