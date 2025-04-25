using UnityEngine;
using System.Collections.Generic;

public class ProceduralSpawner : MonoBehaviour
{
    [Header("Configurações do Cilindro")]
    public float raioCilindro = 5f;
    public float alturaCilindro = 10f;
    public float distanciaMinima = 2f;
    public int quantidadeAsteroides = 30;

    [Header("Referências")]
    public GameObject terraPrefab;
    public GameObject navePrefab;
    public GameObject asteroidePrefab;

    private List<GameObject> objetos = new List<GameObject>();
    private Vector3 centro;

    void Start()
    {
        centro = transform.position;
        GerarCena();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ReiniciarCena();
    }

    void GerarCena()
    {
        LimparCena();

        // 1. Gerar Terra
        Vector3 posTerra = GerarPosicaoNaParede();
        GameObject terra = Instantiate(terraPrefab, posTerra, Quaternion.identity);
        objetos.Add(terra);

        // 2. Gerar Nave
        Vector3 posNave = CalcularPosicaoOposta(posTerra);
        GameObject nave = Instantiate(navePrefab, posNave, Quaternion.identity);
        objetos.Add(nave);

        // 3. Gerar Asteroides
        int asteroidesGerados = 0;
        int tentativasMax = quantidadeAsteroides * 10;

        while (asteroidesGerados < quantidadeAsteroides && tentativasMax-- > 0)
        {
            Vector3 posAsteroide = GerarPosicaoNaParede();

            if (ValidarPosicao(posAsteroide))
            {
                GameObject asteroide = Instantiate(asteroidePrefab, posAsteroide, Quaternion.identity);
                objetos.Add(asteroide);
                asteroidesGerados++;
            }
        }
    }

    Vector3 GerarPosicaoNaParede()
    {
        float angulo = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float altura = Random.Range(-alturaCilindro / 2f, alturaCilindro / 2f);

        return new Vector3(
            Mathf.Cos(angulo) * raioCilindro,
            altura,
            Mathf.Sin(angulo) * raioCilindro
        ) + centro;
    }

    Vector3 CalcularPosicaoOposta(Vector3 referencia)
    {
        Vector3 direcao = (referencia - centro).normalized;
        return centro + (-direcao * raioCilindro) + new Vector3(0, referencia.y - centro.y, 0);
    }

    bool ValidarPosicao(Vector3 novaPosicao)
    {
        foreach (GameObject obj in objetos)
        {
            if (obj != null && Vector3.Distance(novaPosicao, obj.transform.position) < distanciaMinima)
                return false;
        }
        return true;
    }

    void LimparCena()
    {
        // Destrói todas as instâncias geradas
        foreach (GameObject obj in objetos)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        objetos.Clear();
    }

    void ReiniciarCena()
    {
        LimparCena();
        GerarCena();
    }
}