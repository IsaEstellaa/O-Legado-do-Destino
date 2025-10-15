using UnityEngine;

public class Moeda : MonoBehaviour
{
    public int valor = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AdicionarMoedas(valor);
            Destroy(gameObject);
        }
    }
}
