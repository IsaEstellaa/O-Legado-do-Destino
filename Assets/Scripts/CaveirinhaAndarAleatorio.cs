using UnityEngine;
using UnityEngine.AI;

public class CaveirinhaAndarAleatorio : MonoBehaviour
{
    public float raio = 10f; // quanto ela pode se afastar do ponto inicial
    public float tempoEntreMovimentos = 3f; // tempo entre mudanças de destino

    private NavMeshAgent agente;
    private Animator animator;
    private float contadorTempo;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        contadorTempo = tempoEntreMovimentos;
        MoverParaNovoDestino();
    }

    void Update()
    {
        contadorTempo -= Time.deltaTime;
        // velocidade definida
        animator.SetFloat("Speed", agente.speed);

        if ((!agente.pathPending && agente.remainingDistance < 0.5f) || contadorTempo <= 0f)
        {
            MoverParaNovoDestino();
            contadorTempo = tempoEntreMovimentos;
        }
    }

    void MoverParaNovoDestino()
    {
        Vector3 novoDestino = PontoAleatorio(transform.position, raio);
        agente.SetDestination(novoDestino);
    }

    Vector3 PontoAleatorio(Vector3 origem, float raio)
    {
        Vector3 randomDirection = Random.insideUnitSphere * raio;
        randomDirection += origem;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, raio, NavMesh.AllAreas);
        return hit.position;
    }
}
