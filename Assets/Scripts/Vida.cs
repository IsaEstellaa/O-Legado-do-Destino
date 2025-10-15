using UnityEngine;

public class Vida : MonoBehaviour
{
    public int valorVidaAdicionavel = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AdicionarVidas(valorVidaAdicionavel);
            Destroy(gameObject);
        }
    }
}
