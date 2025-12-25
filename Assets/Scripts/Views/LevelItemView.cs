using UnityEngine;
using UnityEngine.UI;

namespace HOG.Views
{
    public class LevelItemView : MonoBehaviour
    {
        [SerializeField]
        private Image _spriteRenderer;

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}