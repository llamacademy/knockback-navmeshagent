using UnityEngine;

namespace LlamAcademy
{
    public class AutoDisable : MonoBehaviour
    {
        [SerializeField] private float AutoDisableTime = 1.5f;
        
        private void OnEnable()
        {
            CancelInvoke();
            Invoke(nameof(Disable), AutoDisableTime);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}