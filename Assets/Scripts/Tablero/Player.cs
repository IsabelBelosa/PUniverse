using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    public Dado dado; // Variable para almacenar la instancia de Dado
    private int movimiento;
    private bool TuTurno = true;

    // Duración de la pausa en segundos
    public float duracionPausa = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Creamos una instancia de Dado y la asignamos a la variable dado
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TuTurno == true){
            Invoke("TurnoJugador", 0);
        }
    }

    void TurnoJugador(){
        dado.GetComponent<Transform>().localScale = new Vector3(2,2,2);
        dado.GetComponent<Transform>().SetPositionAndRotation(this.GetComponent<Transform>().position + new Vector3(0, 3, 0), Quaternion.Euler(0, 270, 0));
        if (Input.GetButtonDown("Jump"))
        {
            TuTurno = false;
            dado.GetComponent<AnimationScriptSkull>().isRotating = true;
            animator.SetTrigger("jump");
            // Llamamos a la función PausarYRealizarTirada() después de la duración de la pausa
            Invoke("PausarYRealizarTirada", duracionPausa);
        }
    }

    // Función para realizar la tirada después de la pausa
    void PausarYRealizarTirada()
    {
        // Llamamos al método Tirar() de la instancia dado
        movimiento = dado.Tirar();
        dado.GetComponent<AnimationScriptSkull>().isRotating = false;

        // Establecemos la posición y rotación del dado según el resultado de la tirada
        switch (movimiento)
        {
            case 1:
                dado.GetComponent<Transform>().SetPositionAndRotation(this.GetComponent<Transform>().position + new Vector3(0, 3, 0), Quaternion.Euler(0, 90, 0));
                break;
            case 2:
                dado.GetComponent<Transform>().SetPositionAndRotation(this.GetComponent<Transform>().position + new Vector3(0, 3, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 3:
                dado.GetComponent<Transform>().SetPositionAndRotation(this.GetComponent<Transform>().position + new Vector3(0, 3, 0), Quaternion.Euler(90, 0, 0));
                break;
            case 4:
                dado.GetComponent<Transform>().SetPositionAndRotation(this.GetComponent<Transform>().position + new Vector3(0, 3, 0), Quaternion.Euler(270, 0, 0));
                break;
            case 5:
                dado.GetComponent<Transform>().SetPositionAndRotation(this.GetComponent<Transform>().position + new Vector3(0, 3, 0), Quaternion.Euler(180, 0, 0));
                break;
            case 6:
                dado.GetComponent<Transform>().SetPositionAndRotation(this.GetComponent<Transform>().position + new Vector3(0, 3, 0), Quaternion.Euler(0, 270, 0));
                break;
        }
        Invoke("MoverJugador", duracionPausa);
    }
}