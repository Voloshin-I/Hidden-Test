using System;
using System.Collections;
using UnityEngine;

namespace HOG.Gameplay
{
    public class GameplayItemView : MonoBehaviour
    {
        public static event Action<GameplayItemView> onClick;

        public void Hide()
        {
            StartCoroutine(HideCoroutine());
        }

        private IEnumerator HideCoroutine()
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            float hideTime = 1.5f;
            float currentTime = hideTime;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            while (currentTime > 0f)
            {
                currentTime -= Time.deltaTime;
                float alpha = currentTime / hideTime;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
                yield return null;
            }
            gameObject.SetActive(false);
        }
        
        private void Start()
        {
            gameObject.AddComponent<PolygonCollider2D>();
        }

        private void OnMouseUp()
        {
            onClick?.Invoke(this);
        }
    }
}