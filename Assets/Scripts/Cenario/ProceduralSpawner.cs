using UnityEngine;
using System.Collections.Generic;

public class ProceduralSpawner : MonoBehaviour
{
    [Header("Configurações do Cilindro")]
    public float raioCilindro = 5f;
    public float alturaCilindro = 10f;
    public float distanciaMinima = 1f;
    public int quantidadeAsteroides = 35;

    [Header("Referências")]
    public GameObject terraPrefab;
    public GameObject navePrefab;
    public GameObject asteroidePrefab;
    public Transform ElementosGerados;

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
        Vector3 posTerra = GerarPosicaoNaEsfera(60f, 120f);
        GameObject terra = Instantiate(terraPrefab, posTerra, Quaternion.identity, ElementosGerados);
        objetos.Add(terra);

        // 2. Gerar Nave na posição oposta à Terra
        Vector3 posNave = CalcularPosicaoOposta(posTerra);
        GameObject nave = Instantiate(navePrefab, posNave, Quaternion.identity, ElementosGerados);
        objetos.Add(nave);

        // 3. Gerar Asteroides (em toda a esfera)
        int asteroidesGerados = 0;
        int tentativasMax = quantidadeAsteroides * 10;

        while (asteroidesGerados < quantidadeAsteroides && tentativasMax-- > 0)
        {
            Vector3 posAsteroide = GerarPosicaoNaEsfera(0f, 180f); // esfera completa

            if (ValidarPosicao(posAsteroide))
            {
                GameObject asteroide = Instantiate(asteroidePrefab, posAsteroide, Quaternion.identity, ElementosGerados);
                objetos.Add(asteroide);
                asteroidesGerados++;
            }
        }
    }

    Vector3 GerarPosicaoNaEsfera(float latitudeMin, float latitudeMax)
    {
        float anguloHorizontal = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float anguloVertical = Random.Range(latitudeMin, latitudeMax) * Mathf.Deg2Rad;

        float x = raioCilindro * Mathf.Sin(anguloVertical) * Mathf.Cos(anguloHorizontal);
        float y = raioCilindro * Mathf.Cos(anguloVertical);
        float z = raioCilindro * Mathf.Sin(anguloVertical) * Mathf.Sin(anguloHorizontal);

        return centro + new Vector3(x, y, z);
    }

    Vector3 CalcularPosicaoOposta(Vector3 referencia)
    {
        Vector3 direcao = (referencia - centro).normalized;
        return centro - direcao * raioCilindro;
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
