using System;
using UnityEngine;
using UnityEngine.UI;

namespace MimiCast.Scripts.Adapter
{
    public class MimiAvatarCameraController : MonoBehaviour
    {

        private Camera _mainCamera;
        [SerializeField] private float zPosition = 1;
        [SerializeField] private float lerpTime = 5;

        [SerializeField] private MimiAvatarConnector avatarConnector;
        [SerializeField] private Slider fieldOfValueSlider;
        
        public void Start()
        {
            _mainCamera = gameObject.GetComponent<Camera>();
            fieldOfValueSlider.value = _mainCamera.fieldOfView;
        }

        public void FixedUpdate()
        {
            if (avatarConnector.AttachedAvatar == null)
            {
                return;
            }
    
            var cameraPos = _mainCamera.transform.position;
            var avatarPos = avatarConnector.AttachedAvatar.Head.position;
            
            var targetPos = new Vector3(cameraPos.x, avatarPos.y, zPosition);

            _mainCamera.transform.position = Vector3.Lerp(cameraPos, targetPos, lerpTime);
            _mainCamera.fieldOfView = Mathf.Lerp(_mainCamera.fieldOfView, fieldOfValueSlider.value, lerpTime);
        }

    }
}