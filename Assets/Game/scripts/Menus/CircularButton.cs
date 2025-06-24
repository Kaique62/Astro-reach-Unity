using UnityEngine;
using UnityEngine.UI; // Para manipular a imagem de carregamento
using System.Collections; // Para usar Coroutines

public class BotaoHandTracking : MonoBehaviour
{
    public Image imagemCarregamento; // Arraste a imagem de carregamento aqui no Inspector
    public float tempoCarregamento = 2f; // Tempo em segundos para o carregamento completo

    private bool maoSobreBotao = false;
    private Coroutine rotinaCarregamento;

    void OnTriggerEnter(Collider other)
    {
        // Certifique-se de que o objeto que entra no trigger é a mão (ou o objeto que representa a ponta do dedo)
        // Você pode usar tags ("HandTip", "Finger") ou nomes de layer para identificar.
        if (other.CompareTag("Mao")) // Assuma que sua mão tem a tag "Mao"
        {
            maoSobreBotao = true;
            rotinaCarregamento = StartCoroutine(IniciarCarregamento());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mao"))
        {
            maoSobreBotao = false;
            if (rotinaCarregamento != null)
            {
                StopCoroutine(rotinaCarregamento);
            }
            imagemCarregamento.fillAmount = 0; // Reseta o carregamento
        }
    }

    IEnumerator IniciarCarregamento()
    {
        float tempoDecorrido = 0f;
        imagemCarregamento.fillAmount = 0; // Garante que começa do zero

        while (tempoDecorrido < tempoCarregamento && maoSobreBotao)
        {
            tempoDecorrido += Time.deltaTime;
            imagemCarregamento.fillAmount = tempoDecorrido / tempoCarregamento;
            yield return null; // Espera o próximo frame
        }

        if (maoSobreBotao) // Se a mão ainda estiver sobre o botão após o carregamento
        {
            ExecutarFuncaoDoBotao();
            imagemCarregamento.fillAmount = 0; // Reseta o carregamento após a execução
        }
    }

    void ExecutarFuncaoDoBotao()
    {
        Debug.Log("Função do botão executada!");
        // --- COLOCAR A LÓGICA DA SUA FUNÇÃO AQUI ---
        // Exemplo: SceneManager.LoadScene("MinhaCena");
        // Exemplo: GetComponent<AudioSource>().Play();
        // Exemplo: gameObject.SetActive(false);
    }
}