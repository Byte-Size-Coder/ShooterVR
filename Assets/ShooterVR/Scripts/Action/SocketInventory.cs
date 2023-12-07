using System;
using UnityEngine;

namespace BSC.SVR.Action
{
    [Serializable]
    public struct BodySocket
    {
        public GameObject SocketObject;

        [Range(0.0f, 1.0f)]
        public float HeightRatio;
    }

    public class SocketInventory : MonoBehaviour
    {
        [SerializeField] private GameObject HeadMountDisplay;
        [SerializeField] private BodySocket[] BodySockets;

        private Vector3 _currentHmdPosition;

        void Update()
        {
            _currentHmdPosition = HeadMountDisplay.transform.localPosition;

            foreach (var bodySocket in BodySockets)
            {
                UpdateBodySocketHeight(bodySocket);
            }

            UpdateSocketInventory();
        }

        private void UpdateBodySocketHeight(BodySocket bodySocket)
        {
            var socketPosition = bodySocket.SocketObject.transform.localPosition;
            bodySocket.SocketObject.transform.localPosition = new Vector3(socketPosition.x,
                _currentHmdPosition.y * bodySocket.HeightRatio, socketPosition.z);
        }

        private void UpdateSocketInventory()
        {
            transform.localPosition = new Vector3(_currentHmdPosition.x, 0, _currentHmdPosition.z);
        }
    }
}
