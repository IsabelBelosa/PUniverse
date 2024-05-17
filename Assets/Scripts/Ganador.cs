using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ganador : MonoBehaviour
{
    public Material jugador1;
    public Material jugador2;
    public Material jugador3;
    public Material jugador4;
    public GameObject ganador;
    // Start is called before the first frame update
    void Start()
    {
        if(TableroJuego.jugador_gana == 1){
            ganador.GetComponent<Renderer>().material = jugador1;
        }
        if(TableroJuego.jugador_gana == 2){
            ganador.GetComponent<Renderer>().material = jugador2;
        }
        if(TableroJuego.jugador_gana == 3){
            ganador.GetComponent<Renderer>().material = jugador3;
        }
        if(TableroJuego.jugador_gana == 4){
            ganador.GetComponent<Renderer>().material = jugador4;
        }
        Invoke("Entrar_ganador", 12f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Entrar_ganador(){

    }
}
