using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MoverJugador : MonoBehaviour
{
    public float velocidad = 0.5f;
    public CharacterController cc;
    public Text ContadorAliens;
    private Interfaz Interfaz;

    // Variables privadas
    private int aliens = 0;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        //offSet = camara.transform.position - transform.position;
        //Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor para evitar que se salga de la ventana del juego
    }

    void FixedUpdate()
    {
        float Vertical = Input.GetAxis("Vertical");
        float Horizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(Horizontal, 0.0f, Vertical);
        cc.Move(movement * velocidad);
    }
    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag =="Alien")
        {
            aliens++;
            ContadorAliens.text = "Aliens = " + aliens + "/2";
            Destroy(other.gameObject);
        }
        if(aliens == 2 && other.gameObject.tag == "Cohete")
        {
            Cursor.lockState = CursorLockMode.None;
            Interfaz.FinalizarJuego();
        }
    }

    void Awake()
    {
        Interfaz = GameObject.FindObjectOfType<Interfaz>();
    }
}
