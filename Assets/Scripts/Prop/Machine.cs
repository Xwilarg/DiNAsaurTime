using GamedevGBG.Player;
using GamedevGBG.SO;
using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace GamedevGBG.Prop
{
    public class Machine : MonoBehaviour
    {
        [SerializeField]
        private MachineInfo _info;

        [SerializeField]
        private Transform[] _slots;

        [SerializeField]
        private TMP_Text _progression;

        [SerializeField]
        private bool _processOnDone;

        private Animator _anim;

        private float _timer = -1f;
        private GameObject[] _targets;

        public int SlotCount => _slots.Length;

        public Vector3 GetSlotPosition(int index)
        {
            return _slots[index].position;
        }

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _targets = new GameObject[_slots.Length];
        }

        public void Remove(GameObject go)
        {
            for (int i = 0; i < _targets.Length; i++)
            {
                var t = _targets[i];
                if (t != null && t.GetInstanceID() == go.GetInstanceID())
                {
                    _targets[i] = null;
                    break;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Make sure that the object isn't already there and that there is empty space
            if (other.CompareTag("Draggable") && !_targets.Any(x => x != null && x.GetInstanceID() == other.gameObject.GetInstanceID()) && _targets.Any(x => x == null))
            {
                var index = Array.IndexOf(_targets, null);
                other.transform.position = _slots[index].position;
                _targets[index] = other.gameObject;
                DragAndDrop.Instance.Drop();
                if (!_targets.Any(x => x == null) && _processOnDone) // All emplacement full
                {
                    _anim.SetBool("IsOpen", false);
                    StartCoroutine(WaitAndProcess());
                }
            }
        }

        private IEnumerator WaitAndProcess()
        {
            yield return new WaitForSeconds(1f);
            _timer = _info.ProcessTime;
        }

        private void Update()
        {
            if (_timer > 0f)
            {
                _timer -= Time.deltaTime;
                if (_progression != null)
                {
                    _progression.text = $"{(_info.ProcessTime - _timer) * 100f / _info.ProcessTime:0.00}%";
                }
                if (_timer <= 0f)
                {
                    foreach (var t in _targets)
                    {
                        Destroy(t);
                    }
                    _targets = new GameObject[_slots.Length];
                    _progression.text = string.Empty;
                    _anim.SetBool("IsOpen", true);
                }
            }
        }
    }
}
