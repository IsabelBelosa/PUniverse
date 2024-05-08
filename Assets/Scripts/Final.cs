using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Final : MonoBehaviour
{
    public GameObject cristal;
    public Camera camara;
    private float velocidadMovimiento = 8;
    public Text has_ganado;
    public Text ganador;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("VuelaCohete",7.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void VuelaCohete(){
        StartCoroutine(VueloCohete());
        has_ganado.enabled = true;
        ganador.text="Jugador " + TableroJuego.jugador_gana;
        switch(TableroJuego.jugador_gana){
            case 1:
                ganador.color= Color.blue;
            break;
            case 2:
                ganador.color= Color.magenta;
            break;
            case 3:
                ganador.color= Color.green;
            break;
            case 4:
                ganador.color= Color.red;
            break;
        }
        
        ganador.enabled = true;
    }

    IEnumerator VueloCohete(){
        cristal.transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Euler(90, 0, 0);
        camara.transform.position = new Vector3(56.3850136f,62.945385f,-29.4771271f);
        camara.transform.rotation = Quaternion.Euler(30.1846447f,326.198151f,359.99173f);

         // Obtenemos la posición inicial y final del jugador
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(0, 0, 3000);
        
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
    }
}
