using Cinemachine;
using UnityEngine;

namespace Modules.Player.Scripts
{
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Vector3 _offsetPosition;
        [SerializeField] private Quaternion _offsetRotation;
        [SerializeField] private float _cameraMoveSpeed;

        private Camera _camera;
        private CinemachineVirtualCamera _virtualCamera;
        private Transform _followTarget;
        private bool _isInit;

        public void Init(Transform followTarget)
        {
            _followTarget = followTarget;

            _camera = GetComponent<Camera>();
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();

            _virtualCamera.Follow = _followTarget;
            
            transform.position = _offsetPosition;
            transform.rotation = _offsetRotation;

            _isInit = true;
        }

        private void Update()
        {
            if (_isInit) 
            {
                Move();
            }
        }

        private void Move() 
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, 
                _followTarget.position + _offsetPosition, 
                _cameraMoveSpeed * Time.deltaTime);
        }

        public Vector3 GetRandomPositionOutsideCamera()
        {
            float cameraWidth = _camera.orthographicSize * _camera.aspect;
            float cameraHeight = _camera.orthographicSize;

            float leftEdge = _camera.transform.position.x - cameraWidth;
            float rightEdge = _camera.transform.position.x + cameraWidth;
            float bottomEdge = _camera.transform.position.y - cameraHeight;
            float topEdge = _camera.transform.position.y + cameraHeight;

            float edgeOffset = 2f;
            float randomXPosition = Random.Range(leftEdge - edgeOffset, rightEdge + edgeOffset);
            float randomZPositon = Random.Range(bottomEdge - edgeOffset, topEdge + edgeOffset);

            return new Vector3(randomXPosition, 0f, randomZPositon);
        }
    }
}
