using UnityEngine;
using UnityEngine.SceneManagement;

public class ZonaVitoria : MonoBehaviour
{
    public Transform jogador;
    public float distanciaParaVencer = 2.5f;
    private bool jaVenceu = false;

    void Update()
    {
        if (jaVenceu || jogador == null) return;

        float distancia = Vector3.Distance(transform.position, jogador.position);

        if (distancia <= distanciaParaVencer)
        {
            Venceu();
        }
    }

    void Venceu()
    {
        jaVenceu = true;
        Debug.Log("Você chegou na saída! Vitória!");

        SceneManager.LoadScene("TelaVitoria");
    }
}