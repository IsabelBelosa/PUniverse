using System;
using UnityEngine;
using UnityEngine.UI;

public class InterfazUsuario : MonoBehaviour
{
    public GameObject menuGanador;
    public bool menuMostradoGanador;

    public GameObject menuPerdedor;
    public bool menuMostradoPerdedor;

    public int segundosCronometro;
    public Text cronometro;

    public bool juegoTerminado; // Variable para indicar si el juego ha terminado

    public void MostrarMenuGanador()
    {
        menuGanador.SetActive(true);
        menuMostradoGanador = true;
        juegoTerminado = true; // El juego ha terminado
    }

    public void EsconderMenuGanador()
    {
        menuGanador.SetActive(false);
        menuMostradoGanador = false;
    }

    public void MostrarMenuPerdedor()
    {
        menuPerdedor.SetActive(true);
        menuMostradoPerdedor = true;
        // juegoTerminado = true; // El juego ha terminado
    }

    public void EsconderMenuPerdedor()
    {
        menuPerdedor.SetActive(false);
        menuMostradoPerdedor = false;
    }

    public void ActivarCronometro()
    {
        ActualizarCronometro();
    }

    public void ReiniciarCronometro()
    {
        segundosCronometro = 0;
    }

    public void PausarCronometro()
    {
        CancelInvoke("ActualizarCronometro");
        MostrarMenuPerdedor();
    }

    public void ActualizarCronometro()
    {
        if (!juegoTerminado) // Verifica si el juego aún no ha terminado
        {
            segundosCronometro++;
            TimeSpan tiempo = new TimeSpan(0, 0, segundosCronometro);
            cronometro.text = tiempo.ToString(@"mm\:ss");

            if (segundosCronometro >= 60)
            {
                juegoTerminado = true;
                PausarCronometro();
            }

            Invoke("ActualizarCronometro", 1.0f);
        }
    }

    //Cuando se pulse el boton de continuar -> Siguiente escena
    // Start is called before the first frame update
    public void Start()
    {
        EsconderMenuGanador();
        EsconderMenuPerdedor();
        ActivarCronometro();
    }
}
