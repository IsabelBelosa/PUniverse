using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    // Otro código de tu clase Game2048

    public void OnClickStartButton()
    {
        // Carga la escena "2048"
        SceneManager.LoadScene("2048");
    }

    // Otro código de tu clase Game2048
}