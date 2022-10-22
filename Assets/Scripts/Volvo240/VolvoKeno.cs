using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolvoKeno : MonoBehaviour
{
    private void Start()
    {
        KenoManager.Instance.StartCoroutine(StartKeno());
    }

    IEnumerator StartKeno()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("KenoScene", LoadSceneMode.Additive);
        GameObject.Find("PlayObj").SetActive(false);
        yield return new WaitForSeconds(1f);
        KenoManager.Instance.OnKenoCalled(KenoManager.KenoType.Pickup);
    }
}
