using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void IniciarJogo()
    {
        SceneManager.LoadScene("Cena_Exploracao"); // nome da sua cena de jogo
    }

    public void SairDoJogo()
    {
        Application.Quit();
        Debug.Log("Saindo do jogo...");
    }
}