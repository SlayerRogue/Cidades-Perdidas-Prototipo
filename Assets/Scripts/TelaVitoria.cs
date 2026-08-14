using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaVitoria : MonoBehaviour
{
    void Start()
    {
        // Libera o cursor para poder clicar nos botões
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void VoltarAoMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}