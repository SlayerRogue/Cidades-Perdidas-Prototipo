using UnityEngine;
using TMPro;

public class GerenciadorUI : MonoBehaviour
{
    [Header("Referências")]
    public TextMeshProUGUI textoObjetivo;
    public InteracaoDresden dresdenScript;
    public ControleArtefato controleArtefato;

    private enum EstadoObjetivo
    {
        BuscarArtefato,
        UsarArtefato,
        EncontrarSaida
    }

    private EstadoObjetivo estadoAtual = EstadoObjetivo.BuscarArtefato;

    void Start()
    {
        AtualizarTexto();
    }

    void Update()
    {
        // Verifica se o Dresden foi coletado
        if (estadoAtual == EstadoObjetivo.BuscarArtefato && dresdenScript != null && dresdenScript.foiColetado)
        {
            estadoAtual = EstadoObjetivo.UsarArtefato;
            AtualizarTexto();
        }

        // Verifica se o artefato foi ativado (paredes reveladas)
        if (estadoAtual == EstadoObjetivo.UsarArtefato && controleArtefato != null)
        {
            // Como o ControleArtefato não tem uma variável pública fácil, 
            // vamos usar uma forma simples: se as paredes já sumiram
            // (vamos melhorar isso no próximo passo se necessário)
        }
    }

    public void MudarParaEncontrarSaida()
    {
        estadoAtual = EstadoObjetivo.EncontrarSaida;
        AtualizarTexto();
    }

    void AtualizarTexto()
    {
        if (textoObjetivo == null) return;

        switch (estadoAtual)
        {
            case EstadoObjetivo.BuscarArtefato:
                textoObjetivo.text = "Alcance o artefato Dresden sem ser detectado";
                break;

            case EstadoObjetivo.UsarArtefato:
                textoObjetivo.text = "Pressione F para usar o Dresden e revelar a passagem";
                break;

            case EstadoObjetivo.EncontrarSaida:
                textoObjetivo.text = "Encontre a saída das ruínas";
                break;
        }
    }
}