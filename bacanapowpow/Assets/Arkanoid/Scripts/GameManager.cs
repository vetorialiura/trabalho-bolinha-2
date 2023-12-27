using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int vidas = 3;

    public int tijolosrestantes;

    public GameObject playerPrefab;
    public GameObject ballPrefab;
    public Transform playerSpawnPoint;
    public Transform ballSpawnPoint;

    public PlayerB playerAtual;
    public BallB ballAtual;

    public TextMeshProUGUI Contador;
    public TextMeshProUGUI MsgFinal;

    public bool Segurando;
    private Vector3 offset;


    
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        SpawnarNovoJogador();
        AtualizarContador();
        tijolosrestantes = GameObject.FindGameObjectsWithTag("Tijolo").Length;

    }

    public void AtualizarContador()
    {

        Contador.text = $"Vidas: {vidas}";
    }

    public void SpawnarNovoJogador()
    {
    
     GameObject playerObj = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
     GameObject ballObj = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);

        playerAtual = playerObj.GetComponent<PlayerB>();
        ballAtual = ballObj.GetComponent<BallB>();

        Segurando = true;
        offset = playerAtual.transform.position - ballAtual.transform.position;
    } 
    
    public void subtrairtijolo()
    {
        tijolosrestantes--;

        if(tijolosrestantes <= 0)
        {
            Vitoria();
        }

    }

    public void subtrairvida()
    {
        vidas--;
        AtualizarContador();
        Destroy(playerAtual.gameObject);
        Destroy(ballAtual.gameObject);
        if(vidas <= 0)
        {

            GameOver();

        }
        else
        {
            Invoke(nameof(SpawnarNovoJogador), 2);
        }
    }

    public void Vitoria()
    {
        MsgFinal.text = "PARABÉNS!";
        Destroy(ballAtual.gameObject);
        Invoke(nameof(reiniciarcena),2);

    }
    public void GameOver()
    {

        MsgFinal.text = "Gamer Over :(";
        Invoke(nameof(reiniciarcena), 2);
    }

    public void reiniciarcena()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Update()
    {
        if(Segurando)
        {

            ballAtual.transform.position = playerAtual.transform.position - offset;

            if (Input.GetKeyDown(KeyCode.Space))
            {

                ballAtual.DispararBolinha(playerAtual.inputX);
                Segurando = false;
            }
        }
        
    }
}
