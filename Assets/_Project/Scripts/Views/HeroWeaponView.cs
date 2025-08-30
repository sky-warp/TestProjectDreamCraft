using UnityEngine;
using UnityEngine.UI;

public class HeroWeaponView : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    public void SetSprite(Sprite sprite) => _button.image.sprite = sprite;
}