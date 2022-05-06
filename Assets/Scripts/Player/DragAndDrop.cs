using UnityEngine;
using UnityEngine.InputSystem;

namespace GamedevGBG
{
    public class DragAndDrop : MonoBehaviour
    {
        private Vector2 _mousePos;
        private Camera _camera;

        private Transform _dragTarget;
        private Vector3 _offset;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_dragTarget != null)
            {
                Vector3 mouseWorld = _camera.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, Vector3.Distance(transform.position, _camera.transform.position)));
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
            Debug.Log(value.phase);
            if (value.phase == InputActionPhase.Started)
            {
                var ray = _camera.ScreenPointToRay(_mousePos);
                if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity))
                {
                    _dragTarget = hit.collider.transform;
                    _dragTarget.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
            else if (value.phase == InputActionPhase.Canceled)
            {
                _dragTarget.GetComponent<Rigidbody>().isKinematic = false;
                _dragTarget = null;
            }
        }
    }
}
