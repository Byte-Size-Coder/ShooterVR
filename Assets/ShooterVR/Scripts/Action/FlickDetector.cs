using UnityEngine;
using UnityEngine.Events;

namespace BSC.SVR.Action
{
    public class FlickDetector : MonoBehaviour
    {
        [SerializeField] private float beginThreshold = 1.25f;
        [SerializeField] private float endThreshold = 0.25f;

        public UnityEvent OnFlick;

        private bool _brokenThreshold = false;

        public void CheckFlick(HandEquip hand)
        {
            var speed = Mathf.Abs(hand.GetRotationSpeed());
            _brokenThreshold = HasFlickBegun(speed);

            if (!HasFlickEnded(speed)) return;
            OnFlick.Invoke();
            ResetThreshold();
        }

        private bool HasFlickBegun(float speed)
        {
            return _brokenThreshold || (speed > beginThreshold);
        }

        private bool HasFlickEnded(float speed)
        {
            return _brokenThreshold && (speed < endThreshold);
        }

        private void ResetThreshold()
        {
            _brokenThreshold = false;
        }
    }
}

