using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    
    public void UpdateHealthBar(float delta)
    {
        if (delta >= 0)
        {
            _image.fillAmount = delta;
        }
    }
}