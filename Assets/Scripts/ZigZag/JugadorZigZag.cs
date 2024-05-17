using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JugadorZigZag : MonoBehaviour
{
    public Camera camara;
    public GameObject suelo;
    public GameObject bola;
    public float velocidad=5;
    public GameObject Tierra;
    public GameObject Jupiter;
    public GameObject Marte;
    public GameObject Mercurio;
    public GameObject Neptuno;
    public GameObject Asteroide;
    public GameObject Saturno;
    public GameObject Urano;
    public GameObject Venus;
    public Text Contador;
    public Animator jugador;
    public Renderer material;
    public Material azul;
    public Material rojo;
    public Material verde;
    public Material rosa;

    private InterfazZigZag Interfaz;
    private Vector3 offset;
    private float ValX, ValZ;
    private Vector3 DireccionActual;
    private int TotalPlanetas = 0;
    private int turno = 0;

    void Start()
    {
        if(turno == 0)
        {
            material.material = azul;
        }
        else if (TableroJuego.turno == 1)
        {
            material.material = rosa;
        }
        else if (TableroJuego.turno == 2)
        {
            material.material = verde;
        }
        else if (TableroJuego.turno == 3)
        {
            material.material = rojo;
        }
    }

    void CreateSueloInicial()
    {
        for(int i = 0 ; i < 3; i++)
        {
            ValZ +=6.0f;
            Instantiate(suelo,new Vector3(ValX,0,ValZ), Quaternion.identity);
        }
    }


    //Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            CambiarDireccion();
        }
    }
    
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(transform.position + DireccionActual * velocidad * Time.fixedDeltaTime);
        if(Input.GetKeyUp(KeyCode.Space))
        {
            CambiarDireccion();
        }
    }
    
    private void OnCollisionExit(Collision other){
        if(other.gameObject.tag =="Suelo")
        {
            Debug.Log("Rutina");
            StartCoroutine(BorrarSuelo(other.gameObject));
        }
    }

    IEnumerator BorrarSuelo(GameObject suelo)
    {
        float aleatorio = Random.Range(0.0f,1.0f);
        if(aleatorio > 0.5f)
        {
            ValX += 6.0f;
        }
        else
        {
            ValZ += 6.0f;
        }
        Instantiate(suelo,new Vector3(ValX,0.0f,ValZ),Quaternion.identity);
        yield return new WaitForSeconds(2);
        suelo.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        suelo.gameObject.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(2);
        Destroy(suelo);

        float ran = Random.Range(0f,1f);
        int rango = (int)(ran * 10); // Multiplicar por 10 y convertir a entero
        switch (rango)
        {
            case 0:
                ran = Random.Range(-2f,2f);
                Instantiate(Tierra,new Vector3(ValX + ran,1.5f,ValZ + aleatorio), Quaternion.identity);
                break;
            case 1:
                ran = Random.Range(-2f,2f);
                Instantiate(Jupiter,new Vector3(ValX + ran,1.5f,ValZ + aleatorio), Quaternion.identity);
                break;
            case 2:
                ran = Random.Range(-2f,2f);
                Instantiate(Marte,new Vector3(ValX + ran,1.5f,ValZ + aleatorio), Quaternion.identity);
                break;
            case 3:
                ran = Random.Range(-2f,2f);
                Instantiate(Mercurio,new Vector3(ValX + ran,1.5f,ValZ + aleatorio), Quaternion.identity);
                break;
            case 4:
                ran = Random.Range(-2f,2f);
                Instantiate(Neptuno,new Vector3(ValX + ran,1.5f,ValZ + aleatorio), Quaternion.identity);
                break;
            case 5:
                ran = Random.Range(-2f,2f);
                Instantiate(Asteroide,new Vector3(ValX + ran,1.5f,ValZ + aleatorio), Quaternion.identity);
                break;
            case 6:
                ran = Random.Range(-2f,2f);
                Instantiate(Saturno,new Vector3(ValX + ran,1.5f,ValZ + aleatorio), Quaternion.identity);
                break;
            case 7:
                ran = Random.Range(-2f,2f);
                Instantiate(Urano,new Vector3(ValX + ran,1.5f,ValZ + aleatorio), Quaternion.identity);
                break;
            case 8:
                ran = Random.Range(-2f,2f);
                Instantiate(Venus,new Vector3(ValX + ran,1.5f,ValZ + aleatorio), Quaternion.identity);
                break;
            case 9:
                ran = Random.Range(-2f,2f);
                Instantiate(Asteroide,new Vector3(ValX + ran,1.5f,ValZ + aleatorio), Quaternion.identity);
                break;
        }
    }

    void CambiarDireccion()
    {
        Rigidbody rb = bola.GetComponent<Rigidbody>();
        if (DireccionActual == Vector3.forward)
        {
            // Define la rotación deseada utilizando un Vector3 de ángulos de Euler
            Vector3 targetEulerAngles = new Vector3(0f, 90f, 0f);
            
            // Convierte los ángulos de Euler a un Quaternion
            Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);
            
            // Aplica la rotación instantáneamente en el Rigidbody
            rb.MoveRotation(targetRotation);
            DireccionActual =  Vector3.right;
        }
        else
        {
            // Define la rotación deseada utilizando un Vector3 de ángulos de Euler
            Vector3 targetEulerAngles = new Vector3(0f, 0f, 0f);
            
            // Convierte los ángulos de Euler a un Quaternion
            Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);
            
            // Aplica la rotación instantáneamente en el Rigidbody
            rb.MoveRotation(targetRotation);
            DireccionActual =  Vector3.forward;
        }
    }

    void Awake()
    {
        Interfaz = GameObject.FindObjectOfType<InterfazZigZag>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Planeta"))
        {
            TotalPlanetas++;
            Contador.text = "Planetas:" + TotalPlanetas + "/10";
            Destroy(other.gameObject);
            if (TotalPlanetas == 10)
            {
                velocidad = 0f;
                Interfaz.MostrarMenuGanador();
            }
        }
        if(other.gameObject.CompareTag("Asteroide"))
        {
            TotalPlanetas--;
            Contador.text = "Planetas:" + TotalPlanetas + "/10";
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Pared"))
        {
            Interfaz.MostrarMenuPerder();
        }
    }

    public void inicio()
    {
        ValX=0.0F;
        ValZ=0.0f;
        offset = camara.transform.position - transform.position;
        
        DireccionActual = Vector3.forward;
        // Activamos la animación de vuelo
        jugador.SetBool("move", true);
        jugador.SetTrigger("jump");
        Rigidbody rb = bola.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = bola.AddComponent<Rigidbody>();
            rb.useGravity = false; 
        }
        CreateSueloInicial();
    }
}