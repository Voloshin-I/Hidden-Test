using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HOG.Gameplay
{
    public class GameplayItemView : MonoBehaviour, IPointerUpHandler
    {
        public static event Action<GameplayItemView> onClick;

        public void OnPointerUp(PointerEventData eventData)
        {
            onClick?.Invoke(this);
        }
    }
}