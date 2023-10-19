using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnState {SPAWNING, WAITING, COUTING};

    public int currentWave;

    //VARIABLES
    [SerializeField] Wave[] waves;
    [SerializeField] private float timeBetweenWaves = 3f;
    [SerializeField] private float wavesCountdown = 0;
    [SerializeField] private TextMeshProUGUI currentWaveCanvas;
    [SerializeField] private TextMeshProUGUI timeToNextWaveCanvas;
    [SerializeField] private TextMeshProUGUI enemiesAliveNum;
    [SerializeField] private GameObject timeToNextWaveText;
    [SerializeField] private GameObject timeToNextWaveNum;
    [SerializeField] private GameObject congratulations;
    
    private SpawnState state = SpawnState.COUTING;
    


    //REFERENCES
    [SerializeField] private Transform[] spawners;
    [SerializeField] private List<CharacterStats> enemyList;

    private void Start() {
        timeToNextWaveText.SetActive(false);
        timeToNextWaveNum.SetActive(false);
        congratulations.SetActive(false);
        wavesCountdown = timeBetweenWaves;
        currentWave = 0;
    }

    private void Update() {
        EnemiesAlive();

        if(state == SpawnState.WAITING)
        {
            if(!EnemiesAreDead())
                return;
            else
                CompleteWave();
        }

        if (wavesCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING){
                StartCoroutine(SpawnWave(waves[currentWave]));

                timeToNextWaveText.SetActive(false);
                timeToNextWaveNum.SetActive(false);
            }
            
        }
        else
        {
            wavesCountdown -= Time.deltaTime;
                timeToNextWaveText.SetActive(true);
                timeToNextWaveNum.SetActive(true);
                int textWavesCountDown = (int)wavesCountdown + 1;
                timeToNextWaveCanvas.text = textWavesCountDown.ToString(); 
            
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.enemiesAmount; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(wave.delay);
        }

        state = SpawnState.WAITING;

        yield break;
            
    }


    private void SpawnEnemy(GameObject enemy){
        int randomInt = Random.Range(1, spawners.Length);
        Transform randomSpawner = spawners[randomInt]; 
        GameObject newEnemy = Instantiate(enemy, randomSpawner.position, spawners[randomInt].rotation);
        CharacterStats newEnemyStats = newEnemy.GetComponent<CharacterStats>();
        
        enemyList.Add(newEnemyStats);

   
    }

    private bool EnemiesAreDead(){

        int i = 0;

        foreach(CharacterStats enemy in enemyList){
            if(enemy.IsDead())
                i++;
            else
                return false;
        }
        return true;
    }

    private void CompleteWave(){
      
        Debug.Log("Wave Completede");

        state = SpawnState.COUTING;
        wavesCountdown = timeBetweenWaves;
        

        if(currentWave + 1 > waves.Length - 1){
            currentWave = 0;

            congratulations.SetActive(true);
            Debug.Log("Complete all the waves");
        }
        else{
            currentWave++;
            
            currentWaveCanvas.text = currentWave.ToString();
        }
    }


    private void EnemiesAlive(){
        List<CharacterStats> filteredList = new List<CharacterStats>();
        foreach (CharacterStats enemy in enemyList)
        {
            if(enemy != null){
                filteredList.Add(enemy);
            }
        }


        int aliveEnemies = filteredList.Count;
        
        enemiesAliveNum.text = aliveEnemies.ToString();
    }
}
