using UnityEngine;
using System.Collections.Generic;

public class ProceduralSpawner : MonoBehaviour
{
    public string prefabFolder = "Prefabs";
    public int quantidade = 10;
    public float distanciaMinima = 1.5f;
    public float campoDeVisaoGraus = 360f; // 360 = tudo, 90 = só frente

    private List<GameObject> objetosGerados = new List<GameObject>();
    private float raio, altura;

    void Start()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        raio = transform.localScale.x * 0.5f;
        altura = transform.localScale.y * 1f;

        GameObject[] prefabs = Resources.LoadAll<GameObject>(prefabFolder);
        if (prefabs.Length == 0)
        {
            Debug.LogError("Nenhum prefab encontrado em Resources/" + prefabFolder);
            return;
        }

        int tentativasMax = 1000;
        int gerados = 0;

        while (gerados < quantidade && tentativasMax-- > 0)
        {
            Vector3 pos = GerarPosicaoAleatoria();
            bool valido = true;

            foreach (var obj in objetosGerados)
            {
                if (Vector3.Distance(pos, obj.transform.position) < distanciaMinima)
                {
                    valido = false;
                    break;
                }
            }

            if (valido)
            {
                GameObject prefabEscolhido = prefabs[Random.Range(0, prefabs.Length)];
                GameObject instancia = Instantiate(prefabEscolhido, pos, Quaternion.identity);
                objetosGerados.Add(instancia);
                gerados++;
            }
        }
    }

    Vector3 GerarPosicaoAleatoria()
    {
        float angulo = Random.Range(-campoDeVisaoGraus / 2f, campoDeVisaoGraus / 2f) * Mathf.Deg2Rad;
        float alturaY = Random.Range(-altura / 2f, altura / 2f);

        float x = Mathf.Cos(angulo) * raio;
        float z = Mathf.Sin(angulo) * raio;

        return transform.position + new Vector3(x, alturaY, z);
    }
}
