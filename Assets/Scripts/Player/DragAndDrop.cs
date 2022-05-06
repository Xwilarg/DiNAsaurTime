using UnityEngine;
using UnityEngine.InputSystem;

namespace GamedevGBG
{
    public class DragAndDrop : MonoBehaviour
    {
        private Vector2 _mousePos;
        private Camera _camera;

        private Transform _dragTarget;
        private Vector3 _lastFramePos;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_dragTarget != null) // TODO: Improve drag & drop
            {
                _lastFramePos = _dragTarget.position;
                Vector3 mouseWorld = _camera.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, -_camera.transform.position.z));
                _dragTarget.transform.position = new Vector3(
                    x: mouseWorld.x,
                    y: mouseWorld.y,
                    z: _dragTarget.transform.position.z
                );
            }
        }

        public void OnLook(InputAction.CallbackContext value)
        {
            _mousePos = value.ReadValue<Vector2>();
        }

        public void OnClick(InputAction.CallbackContext value)
        {
            if (value.phase == InputActionPhase.Started)
            {
                var ray = _camera.ScreenPointToRay(_mousePos);
                if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity) && hit.collider.CompareTag("Draggable"))
                {
                    _dragTarget = hit.collider.transform;
                    _dragTarget.GetComponent<Rigidbody>().isKinematic = true;
                    _dragTarget.transform.rotation = Quaternion.identity;
                }
            }
            else if (value.phase == InputActionPhase.Canceled && _dragTarget != null)
            {
                _dragTarget.GetComponent<Rigidbody>().isKinematic = false;
                _dragTarget.GetComponent<Rigidbody>().AddForce((_dragTarget.position - _lastFramePos) * 10f, ForceMode.Impulse);
                _dragTarget = null;
            }
        }
    }
}
