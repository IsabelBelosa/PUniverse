using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableroJuego : MonoBehaviour
{
    public Player jugador1;
    public Player jugador2;
    //public Player jugador3;
    //public Player jugador4;
    private int turno = 0;
    private int numJugadores = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IniciarJuego(){
        jugador1.ActivarTurno();
    }

    public void SiguienteTurno(){
        turno++;
        turno = turno % numJugadores;
        if (turno == 0){
            jugador1.ActivarTurno();
        }
        if (turno == 1){
            jugador2.ActivarTurno();
        }
        //if (turno == 2){
        //    jugador3.ActivarTurno();
        //}
        //if (turno == 3){
        //    jugador4.ActivarTurno();
        //}
    }
}
