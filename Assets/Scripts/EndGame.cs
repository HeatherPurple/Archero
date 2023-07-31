using UnityEngine;
using UnityEngine.Events;

public class EndGame : MonoBehaviour
{
    public static readonly UnityEvent EndGameEvent = new UnityEvent();

    private int _enemyNumber;

    private void Awake()
    {
        Enemy.EnemyDeath.AddListener(DecreaseEnemyNumber);
        Spawner.EnemySpawn.AddListener(IncreaseEnemyNumber);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Player component)) return;
        Debug.Log("Win!");
        Time.timeScale = 0f;
    }
    
    private void DecreaseEnemyNumber()
    {
        _enemyNumber -= 1;
        if (_enemyNumber <= 0)
        {
            EndGameEvent.Invoke();
        }
    }

    private void IncreaseEnemyNumber()
    {
        _enemyNumber += 1;
    }
    
    private void OnDestroy()
    {
        Enemy.EnemyDeath.RemoveListener(DecreaseEnemyNumber);
        Spawner.EnemySpawn.RemoveListener(IncreaseEnemyNumber);
    }
}
