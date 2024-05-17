using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TableroJuego : MonoBehaviour
{
    public Player jugador1;
    public Player jugador2;
    public Player jugador3;
    public Player jugador4;
    static public int turno = 0;
    private int numJugadores = 4;
    static public Text puntos;
    public Text numJugador;
    private string minijuego;
    static public bool juegoTerminado = false; // Variable para indicar si el juego ha terminado
    static public int jugador_gana = 1;

    // Start is called before the first frame update
    void Start()
    {
        puntos = GameObject.FindWithTag("puntuacion").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IniciarJuego(){
        numJugadores = Menu.Jugadores;
        Alien.mensajeInicio.enabled = false;
        puntos.enabled = true;
        numJugador.enabled = true;
        jugador1.ActivarTurno();
    }

    public void SiguienteTurno(){
        turno++;
        turno = turno % numJugadores;
        if (turno == 0){
            numJugador.text = "Jugador " + (turno+1);
            numJugador.color = Color.blue;
            jugador1.ActivarTurno();
        }
        if (turno == 1){
            numJugador.text = "Jugador " + (turno+1);
            numJugador.color = Color.magenta;
            jugador2.ActivarTurno();
        }
        if (turno == 2){
            numJugador.text = "Jugador " + (turno+1);
            numJugador.color = Color.green;
            jugador3.ActivarTurno();
        }
        if (turno == 3){
            numJugador.text = "Jugador " + (turno+1);
            numJugador.color = Color.red;
            jugador4.ActivarTurno();
        }
    }

    public void ActivarMinijuego(){
        int juego = Random.Range(1, 6); //cambiar el rango cuando esten los minijuegos
        switch(juego){
            case 1:
                minijuego = "Juego_Parejas";
                SceneManager.LoadSceneAsync(minijuego, LoadSceneMode.Additive);
                break;
            case 2:
                minijuego = "Tetris";
                SceneManager.LoadSceneAsync(minijuego, LoadSceneMode.Additive);
                break;

            case 3:
                minijuego = "2048";
                SceneManager.LoadSceneAsync(minijuego, LoadSceneMode.Additive);
                break;
            case 4:
                minijuego = "Puzzle";
                SceneManager.LoadSceneAsync(minijuego, LoadSceneMode.Additive);
                break;
            case 5:
                minijuego = "ZigZag";
                SceneManager.LoadSceneAsync(minijuego, LoadSceneMode.Additive);
                break;
        }   
    }

    public void DesactivarMinijuego(){
        SceneManager.UnloadSceneAsync(minijuego);
    }


}
