using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    public Dado dado; // Variable para almacenar la instancia de Dado
    private int movimiento;
    private bool TuTurno = true;
    private int casilla = 0;
    private int newCasilla;
    private GameObject Casilla_Act;
    private GameObject Casilla_Next; 
    public float velocidadMovimiento = 3;

    // Start is called before the first frame update
    void Start()
    {
        // Creamos una instancia de Dado y la asignamos a la variable dado
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TuTurno){
            Invoke("TurnoJugador", 0);
        }

        if(dado.GetComponent<Transform>().localScale.x <= 0.02f){
            dado.GetComponent<AnimationScriptSkull>().isScaling = false;
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
            Invoke("PausarYRealizarTirada", 2.0f);
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

        Invoke("DesaparecerDado", 3.0f);

    }

    void DesaparecerDado(){
        // Escalamos el dado para que desaparezca
        dado.GetComponent<AnimationScriptSkull>().isScaling = true;

        // Calculamos la nueva casilla
        newCasilla = (casilla + movimiento) % 10;

        // Obtenemos las casillas actual y siguiente
        Casilla_Act = GameObject.FindWithTag("casilla" + casilla);
        Casilla_Next = GameObject.FindWithTag("casilla" + newCasilla);

        // Calculamos la rotación hacia la siguiente casilla
        float rotacion = CalcularRotacion(Casilla_Act.transform.position, Casilla_Next.transform.position);

        // Actualizamos la rotación del jugador
        Quaternion nuevaRotacion = Quaternion.Euler(0f, rotacion, 0f);
        transform.rotation = nuevaRotacion;

        // Activamos la animación de vuelo
        animator.SetBool("move", true);
        animator.SetTrigger("jump");

        Invoke("MoverJugador", 2.0f);
    }

    void MoverJugador()
    {
        // Iniciamos la corrutina para mover al jugador
        StartCoroutine(MoverJugadorCoroutine());
    }

    float CalcularRotacion(Vector3 puntoA, Vector3 puntoB)
    {
        // Calculamos la diferencia en las posiciones de los ejes X y Z
        float deltaX = puntoB.x - puntoA.x;
        float deltaZ = puntoB.z - puntoA.z;

        // Usamos Atan2 para obtener el ángulo en radianes entre los dos puntos
        float radianes = Mathf.Atan2(deltaX, deltaZ);

        // Convertimos de radianes a grados y ajustamos el ángulo para que esté en el rango [0, 360]
        float grados = radianes * Mathf.Rad2Deg;
        grados = (grados + 360) % 360;

        // Devolvemos el ángulo de rotación resultante
        return grados;
    }

    IEnumerator MoverJugadorCoroutine()
    {
        // Obtenemos la posición inicial y final del jugador
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(Casilla_Next.transform.position.x, transform.position.y , Casilla_Next.transform.position.z);
        float distance = Vector3.Distance(startPos, endPos);

        // Mientras el jugador no haya llegado a la posición final
        while (transform.position != endPos)
        {
            // Calculamos el paso de movimiento
            float step = velocidadMovimiento * Time.deltaTime;

            // Movemos al jugador hacia la posición final de manera suave
            transform.position = Vector3.MoveTowards(transform.position, endPos, step);

            // Esperamos al próximo frame
            yield return null;
        }

        // Una vez que el jugador llega a la nueva casilla, lo rotamos hacia (0,0,0)
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        // Actualizamos la variable de casilla con la nueva casilla
        casilla = newCasilla;

        // Indicamos que el jugador ha terminado de moverse
        animator.SetBool("move", false);
    }
}

    