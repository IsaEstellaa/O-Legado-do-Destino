using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameManager gm;

    public void Start()
    {
        GetComponentInChildren<TMPro.TextMeshProUGUI>().gameObject.LeanScale(new Vector3(1.2f, 1.2f), 0.4f).setLoopPingPong();
    }

    public void Play()
    {
        GetComponent<CanvasGroup>().LeanAlpha(0, 0.3f).setOnComplete(OnComplete);
    }

    private void OnComplete()
    {
        gm.Enabled();
        Destroy(gameObject);
    }
}
