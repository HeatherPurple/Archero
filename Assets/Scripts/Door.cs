using UnityEngine;

public class Door : MonoBehaviour
{
    private void Awake()
    {
        EndGame.EndGameEvent.AddListener(OpenDoor);
    }

    private void OpenDoor()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EndGame.EndGameEvent.RemoveListener(OpenDoor);
    }
}
