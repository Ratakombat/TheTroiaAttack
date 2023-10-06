using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnState {SPAWNING, WAITING, COUTING};

    //VARIABLES
    [SerializeField] Wave[] waves;
    [SerializeField] private float timeBetweenWaves = 3f;
    [SerializeField] private float wavesCountdown = 0;
    private SpawnState state = SpawnState.COUTING;
    


    //REFERENCES
    [SerializeField] private Transform[] spawners;
    [SerializeField] private GameObject enemy;

    private void Start() {
        wavesCountdown = timeBetweenWaves;
    }

    private void Update() {
        if (wavesCountdown <= 0)
        {
            //Spawn Enemies
            
        }
        else
        {
            wavesCountdown -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnEnemy();
        }
    }
    private void SpawnEnemy(){
        int randomInt = Random.Range(1, spawners.Length);
        Transform randomSpawner = spawners[randomInt]; 
        Instantiate(enemy, randomSpawner.position, spawners[randomInt].rotation);


        Debug.Log(randomInt);
    }

}
