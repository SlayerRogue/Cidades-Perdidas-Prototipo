using UnityEngine;

public class SentinelaPatrulha : MonoBehaviour
{
    [Header("Patrulha")]
    public float velocidadePatrulha = 1.8f;
    public float velocidadePerseguicao = 3.4f;
    public float distanciaPatrulha = 3.5f;
    public float tempoEsperaNasPontas = 1.2f;

    [Header("Detecção")]
    public Transform jogador;
    public float distanciaVisao = 9f;
    public float anguloVisao = 55f;
    public LayerMask camadasObstaculos;

    // Variáveis internas
    private Vector3 pontoInicial;
    private Vector3 pontoEsquerda;
    private Vector3 pontoDireita;
    private Vector3 destinoAtual;
    private bool indoParaDireita = true;
    private bool estaEsperando = false;
    private float tempoEsperaAtual = 0f;
    private bool viuOJogador = false;

    void Start()
    {
        pontoInicial = transform.position;
        pontoEsquerda = pontoInicial + Vector3.left * distanciaPatrulha;
        pontoDireita = pontoInicial + Vector3.right * distanciaPatrulha;
        destinoAtual = pontoDireita;
    }

    void Update()
    {
        DetectarJogador();

        if (viuOJogador)
            Perseguir();
        else
            Patrulhar();
    }

    void DetectarJogador()
    {
        if (jogador == null) return;

        Vector3 origemVisao = transform.position + Vector3.up * 1.5f;
        Vector3 alvo = jogador.position + Vector3.up * 1.0f;

        Vector3 direcao = alvo - origemVisao;
        float distancia = direcao.magnitude;

        if (distancia > distanciaVisao)
        {
            viuOJogador = false;
            return;
        }

        float angulo = Vector3.Angle(transform.forward, direcao);
        if (angulo > anguloVisao)
        {
            viuOJogador = false;
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(origemVisao, direcao.normalized, out hit, distancia, camadasObstaculos))
        {
            if (hit.collider.gameObject != jogador.gameObject && !hit.collider.transform.IsChildOf(jogador))
            {
                viuOJogador = false;
                return;
            }
        }

        viuOJogador = true;
    }

    void Perseguir()
    {
        Vector3 direcao = (jogador.position - transform.position).normalized;
        direcao.y = 0;

        transform.position += direcao * velocidadePerseguicao * Time.deltaTime;

        if (direcao != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direcao), Time.deltaTime * 8f);
        }
    }

    void Patrulhar()
    {
        if (estaEsperando)
        {
            tempoEsperaAtual -= Time.deltaTime;
            if (tempoEsperaAtual <= 0f)
            {
                estaEsperando = false;
                indoParaDireita = !indoParaDireita;
                destinoAtual = indoParaDireita ? pontoDireita : pontoEsquerda;
            }
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, destinoAtual, velocidadePatrulha * Time.deltaTime);

        Vector3 direcao = (destinoAtual - transform.position).normalized;
        if (direcao != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direcao), Time.deltaTime * 5f);
        }

        if (Vector3.Distance(transform.position, destinoAtual) < 0.15f)
        {
            estaEsperando = true;
            tempoEsperaAtual = tempoEsperaNasPontas;
        }
    }
}