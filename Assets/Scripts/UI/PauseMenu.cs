using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private VolvoController volvoContrl;
    private bool paused = false;
    [SerializeField] private GameObject firstChild;

    private void Start()
    {
        VolvoController.OnReceivePause += Pause;
    }

    public void Pause()
    {
        paused = !paused;
        Time.timeScale = paused ? 0f : 1f;
        print(firstChild);
        firstChild.SetActive(paused);
    }

    public void ToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDestroy()
    {
        VolvoController.OnReceivePause -= Pause;
    }
}
