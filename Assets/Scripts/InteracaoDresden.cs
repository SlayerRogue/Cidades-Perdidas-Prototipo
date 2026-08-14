using UnityEngine;

public class InteracaoDresden : MonoBehaviour
{
    [Header("Configurações")]
    public float distanciaInteracao = 2.2f;
    public KeyCode teclaInteracao = KeyCode.E;
    public Transform jogador;

    [Header("Estado")]
    public bool foiColetado = false;

    private bool jogadorPerto = false;

    void Update()
    {
        if (foiColetado || jogador == null) return;

        float distancia = Vector3.Distance(transform.position, jogador.position);
        jogadorPerto = distancia <= distanciaInteracao;

        if (jogadorPerto && Input.GetKeyDown(teclaInteracao))
        {
            ColetarDresden();
        }
    }

    void ColetarDresden()
    {
        foiColetado = true;
        Debug.Log("Dresden coletado!");

        // Desativa o objeto do artefato
        gameObject.SetActive(false);

        // Desliga a luz do Dresden
        GameObject luz = GameObject.Find("Luz_Dresden");
        if (luz != null)
        {
            luz.SetActive(false);
        }
    }

    // Desenha uma esfera no Scene para ver a área de interação
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, distanciaInteracao);
    }
}