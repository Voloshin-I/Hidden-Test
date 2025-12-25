using UnityEngine;
using VContainer.Unity;

namespace HOG.Gameplay
{
    public class EscapeListener : ITickable
    {
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
    }
}
