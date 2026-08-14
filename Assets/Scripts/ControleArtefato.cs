using UnityEngine;

public class ControleArtefato : MonoBehaviour
{
    [Header("Referências")]
    public InteracaoDresden dresdenScript;
    public GameObject[] paredesSecretas;

    [Header("Configuração")]
    public KeyCode teclaRevelar = KeyCode.F;

    private bool artefatoAtivo = false;

    void Update()
    {
        if (dresdenScript != null && dresdenScript.foiColetado && !artefatoAtivo)
        {
            if (Input.GetKeyDown(teclaRevelar))
            {
                RevelarPassagem();
            }
        }
    }

    void RevelarPassagem()
    {
        artefatoAtivo = true;
        Debug.Log("Artefato ativado! Passagens reveladas.");

        foreach (GameObject parede in paredesSecretas)
        {
            if (parede != null)
                parede.SetActive(false);
        }

        // Muda o objetivo na UI
        GerenciadorUI ui = FindObjectOfType<GerenciadorUI>();
        if (ui != null)
        {
            ui.MudarParaEncontrarSaida();
        }
    }
}