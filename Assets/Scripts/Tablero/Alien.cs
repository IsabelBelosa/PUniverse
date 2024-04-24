using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{

    private Animator animator;
    public Camera camara;
    public AudioClip mareo;
    public AudioClip cohete;
    private AlienVuela alienVuela;
    public TableroJuego tablero;

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el componente Animator del objeto
        animator = GetComponent<Animator>();
        alienVuela = GetComponent<AlienVuela>();
        Invoke("MareoAlien",5.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MareoAlien(){
        CambiarAnimacion("rig|SpaceGuyPose_ZeroGravity_1");
        alienVuela.isAnimated = true;
        transform.position = new Vector3(-7.103f, 6.060875f, -30.312f);
        camara.transform.position = new Vector3(-7.343821f, 6.620454f, -27.85598f);
        GetComponent<AudioSource>().clip = mareo; 
        GetComponent<AudioSource>().Play();
        Invoke("LlegarCohete", 6.5f);
    }

    void LlegarCohete(){
        alienVuela.isAnimated=false;
        CambiarAnimacion("rig|SpaceGuyPose_Standing");
        transform.position = new Vector3(-9.543f, 7.37f, -33.427f);
        transform.rotation = new Quaternion(0f,0f,0f,0f);
        GetComponent<AudioSource>().clip = cohete; 
        GetComponent<AudioSource>().Play();
        camara.transform.position = new Vector3(-9.465719f, 8.143775f, -28.78481f);
        camara.transform.rotation = new Quaternion(18.3588505f,180.281967f,-18.3588505f,0f);
        Invoke("comenzarPartida", 8.5f);
    }

    void comenzarPartida(){
        tablero.IniciarJuego();
    }

    void CambiarAnimacion(string nombreAnimacion)
    {
        // Si el componente Animator no está asignado, salir del método
        if (animator == null)
        {
            Debug.LogWarning("El componente Animator no está asignado.");
            return;
        }

        // Cambiar a la animación especificada por nombre
        animator.Play(nombreAnimacion);
    }
}
