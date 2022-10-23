using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    private VolvoFighting fighting;

    private void Start()
    {
        fighting = GameObject.Find("Volvo240").GetComponent<VolvoFighting>();
    }

    void Update()
    {
        hpBar.fillAmount = Mathf.Clamp01(fighting.GetHealth / VolvoConfig.Get.currHealth);
    }
}
