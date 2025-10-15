using UnityEngine;

public class Jogador : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;

    [Header("Pulo")]
    public float forcaPulo = 8f;
    public Transform chaoCheck;
    public float chaoCheckRaio = 0.1f;
    public LayerMask oQueEChao;

    [Header("Referências")]
    public Animator animator; // arraste CharacterArmature aqui

    private Rigidbody rb;
    private bool estaNoChao;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        animator.updateMode = AnimatorUpdateMode.Normal;
    }

    void Update()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        // --- CHECAGEM DE CHÃO ---
        estaNoChao = Physics.CheckSphere(chaoCheck.position, chaoCheckRaio, oQueEChao);
        animator.SetBool("IsJumping", !estaNoChao && rb.linearVelocity.y > 0.1f);


        // --- INPUT HORIZONTAL ---
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput   = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        // --- ROTACAO ---
        if (direction.magnitude > 0.1f)
        {
            direction = direction.normalized;

            // Rotação suave
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // --- MOVIMENTO HORIZONTAL ---
        Vector3 move = direction * velocidade * Time.deltaTime;
        rb.MovePosition(rb.position + new Vector3(move.x, 0, move.z));  

        // --- ANIMATOR SPEED ---
        float speedForAnimator = direction.magnitude > 0.05f ? direction.magnitude : 0f;
        animator.SetFloat("Speed", speedForAnimator);

        // --- PULO ---
        if (Input.GetButtonDown("Jump") && estaNoChao)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, forcaPulo, rb.linearVelocity.z);
            animator.SetBool("IsJumping", true); // garante que o Animator detecte o salto no mesmo frame
        }


    }
}
