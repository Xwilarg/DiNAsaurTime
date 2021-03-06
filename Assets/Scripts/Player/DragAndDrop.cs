using GamedevGBG.Prop;
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

        [SerializeField]
        private AudioClip _grab, _drop;

        [SerializeField]
        private float _frontOffset;

        private AudioSource _source;

        private Vector2 _mousePos;
        private Camera _camera;

        private Transform _dragTarget;
        private Vector3 _lastFramePos;
        private Vector3 _offset;

        private void Start()
        {
            _source = GetComponent<AudioSource>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_dragTarget != null) // TODO: Improve drag & drop
            {
                _lastFramePos = _dragTarget.position;
                Vector3 mouseWorld = _camera.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, Mathf.Abs(_camera.transform.position.z - _dragTarget.position.z)));
                _dragTarget.transform.position = new Vector3(
                    x: mouseWorld.x + _offset.x,
                    y: mouseWorld.y + _offset.y,
                    z: _frontOffset
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
                if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity))
                {
                    // Debug.Log($"Click detected on {hit.collider.name}");
                    if (hit.collider.CompareTag("Draggable"))
                    {
                        MachineManager.Instance.RemoveFromMachine(hit.collider.gameObject);
                        _dragTarget = hit.collider.transform;
                        _dragTarget.GetComponent<Rigidbody>().isKinematic = true;
                        _offset = _dragTarget.transform.position - hit.point;
                        _source.PlayOneShot(_grab);
                    }
                }
            }
            else if (value.phase == InputActionPhase.Canceled && _dragTarget != null)
            {
                _dragTarget.GetComponent<Rigidbody>().isKinematic = false;
                _dragTarget.GetComponent<Rigidbody>().AddForce((_dragTarget.position - _lastFramePos) * 20f, ForceMode.Impulse);
                _dragTarget.GetComponent<Rigidbody>().AddTorque((_dragTarget.position - _lastFramePos).normalized, ForceMode.Impulse);
                Drop();
            }
        }

        public void Drop()
        {
            _dragTarget = null;
        }

        public void PlayPop()
        {
            _source.PlayOneShot(_drop);
        }
    }
}
