using UnityEngine;
using UnityEngine.UI;

namespace HOG.Views
{
    public class LevelItemView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _imageBackground;
        [SerializeField]
        private GameObject _textBackground;
        [SerializeField]
        private Image _spriteRenderer;
        [SerializeField]
        private TMPro.TMP_Text _label;

        public void Set(Sprite sprite, bool imageMode)
        {
            _imageBackground.SetActive(imageMode);
            _spriteRenderer.gameObject.SetActive(imageMode);
            _textBackground.SetActive(!imageMode);
            _label.gameObject.SetActive(!imageMode);
            
            _label.text = sprite.name;
            _spriteRenderer.sprite = sprite;
        }
    }
}