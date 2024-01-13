using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
    public List<GameObject> enemies = new List<GameObject>();
    public Transform position;
    public GameObject spawnPrefab;
    public float spawnRate = 5f;
    public float spawnTimer;
    private UnityEvent morteEnemy;
    public Transform target;
    private float spawnDistance = 5f;
    public int pontos = 0;
    public TextMeshProUGUI pontosInfo;
    private Scene scene;
    void Start()
    {
        SpawnEnemy();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        pontosInfo.text = "Score: " + pontos;
    }
    void Update()
    {
        if (spawnTimer <= 0f)
        {
            spawnTimer = spawnRate;
            SpawnEnemy();
        } else 
        {
            spawnTimer -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Esc foi pressionado");
        }
    }
    public void SpawnEnemy()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject spawn = Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
        enemies.Add(spawn);
        spawn.GetComponent<Enemy>().morte.AddListener(Morto);
    }
    private Vector2 GetRandomSpawnPosition()
    {
        Vector2 randomSpawnPosition = Vector2.zero;
        float minSpawnDistance = 5.0f;

        Vector2 randomDirection = Random.insideUnitCircle.normalized * spawnDistance;
        do{
            randomDirection = Random.insideUnitCircle.normalized * spawnDistance;
        } while (Vector2.Distance(target.position, (Vector2) target.position + randomDirection) < minSpawnDistance);
        randomSpawnPosition = (Vector2) target.position + randomDirection;

        return randomSpawnPosition;
    }
    public void Morto()
    {
        Debug.Log("Inimigo morreu");
        pontos++;
        pontosInfo.text = "Score: " + pontos;
        if (pontos%5 == 0 && spawnRate >= 1.5f){
            spawnRate = spawnRate - 0.5f;
            spawnPrefab.GetComponent<Enemy>().rotateSpeed += 0.0025f;
            spawnPrefab.GetComponent<Enemy>().speed += 0.25f;
        }
    }
    public void Retry()
    {
        SceneManager.LoadScene("Scene1");
    }
}