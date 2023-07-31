using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int enemyNumber = 4;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private List<GameObject> enemyPrefabList = new List<GameObject>();

    private Transform _floor;
    private float _minX;
    private float _minZ;
    private Vector3 _playerSpawnPos;

    public static readonly UnityEvent EnemySpawn = new UnityEvent();

    private void Awake()
    {
        GetSpawnZone();
        Spawn(playerPrefab, _playerSpawnPos);
    }

    private void Start()
    {
        for (var i = 0; i < enemyNumber; i++)
        {
            var randomEnemy = enemyPrefabList[Random.Range(0, enemyPrefabList.Count)];
            Spawn(randomEnemy, GetRandomEnemyPos() + randomEnemy.transform.position);
            EnemySpawn.Invoke();
        }
    }

    private void GetSpawnZone()
    {
        _floor = transform.parent;
        var bounds = _floor.GetComponent<MeshFilter>().mesh.bounds;

        var position = _floor.position;
        var localScale = _floor.localScale;
        _minX = position.x - localScale.x * bounds.size.x * 0.5f;
        _minZ = position.z - localScale.z * bounds.size.z * 0.5f;

        _playerSpawnPos = new Vector3(0f, 1f, _minZ);
    }
    
    private static void Spawn(GameObject prefab, Vector3 position)
    {
        Instantiate(prefab, position, new Quaternion());
    }

    private Vector3 GetRandomEnemyPos()
    {
        return new Vector3(Random.Range(_minX, -_minX), 0f, Random.Range((1f / 3f) * _minZ, -_minZ));
    }
    
}
