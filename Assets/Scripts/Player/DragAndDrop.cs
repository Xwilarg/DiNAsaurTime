using UnityEngine;
using UnityEngine.InputSystem;

namespace GamedevGBG.Player
{
    public class DragAndDrop : MonoBehaviour
    {
        public static DragAndDrop Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private Vector2 _mousePos;
        private Camera _camera;

        private Transform _dragTarget;
        private Vector3 _lastFramePos;
        private Vector3 _offset;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_dragTarget != null) // TODO: Improve drag & drop
            {
                _lastFramePos = _dragTarget.position;
                Vector3 mouseWorld = _camera.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, 1.5f));
                _dragTarget.transform.position = new Vector3(
                    x: mouseWorld.x + _offset.x,
                    y: mouseWorld.y + _offset.y,
                    z: -10.1f
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
                    _offset = _dragTarget.transform.position - hit.point;
                }
            }
            else if (value.phase == InputActionPhase.Canceled && _dragTarget != null)
            {
                _dragTarget.GetComponent<Rigidbody>().isKinematic = false;
                _dragTarget.GetComponent<Rigidbody>().AddForce((_dragTarget.position - _lastFramePos) * 20f, ForceMode.Impulse);
                Drop();
            }
        }

        public void Drop()
        {
            _dragTarget = null;
        }
    }
}
