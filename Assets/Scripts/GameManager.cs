using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMPro.TextMeshProUGUI ValorTotalMoedas;
    public TMPro.TextMeshProUGUI VidaTotal;
    public TMPro.TextMeshProUGUI textoPause;
    public Image BackgroundMenu;

    public int totalMoedas = 0;
    public int totalVida = 0;

    public static GameManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    /*SISTEMA DE ADIÇÃO DE MOEDAS*/
    public void AdicionarMoedas(int quantidade)
    {
        totalMoedas += quantidade;
        AtualizarTextoMoedas();
    }

    private void AtualizarTextoMoedas()
    {
        ValorTotalMoedas.text = totalMoedas.ToString();
    }
    //

    /*SISTEMA DE ADIÇÃO DE VIDA*/

    public void AdicionarVidas(int quantidade)
    {
        totalVida += quantidade;
        AtualizarTextoVidas();
    }

    private void AtualizarTextoVidas()
    {
        VidaTotal.text = totalVida.ToString();
    }

    //

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Time.timeScale == 0) //pausado
            {
                StartCoroutine(ScaleTime(0, 1, 0.5f));
                BackgroundMenu.gameObject.SetActive(false);
                textoPause.gameObject.SetActive(false);
            }

            if (Time.timeScale == 1) // despausado
            {
                StartCoroutine(ScaleTime(1, 0, 0.5f));
                BackgroundMenu.gameObject.SetActive(true);
                textoPause.gameObject.SetActive(true);
            }
        }

    }

    IEnumerator ScaleTime(float start, float end, float duration)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;

        while (timer < duration)
        {
            // Time.timescale eh onde mexe na velocidade do jogo
            // Time.fixedDeltaTime eh o tempo que a fisica afeta os G.O
            Time.timeScale = Mathf.Lerp(start, end, timer / duration);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;

            yield return null;
        }

        Time.timeScale = end;
        Time.fixedDeltaTime = 0.02f * end;
    }

    public void Enabled()
    {
        gameObject.SetActive(true);
    }
}
