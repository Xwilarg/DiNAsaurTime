using GamedevGBG.Player;
using GamedevGBG.SO;
using System.Collections;
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

        private Animator _anim;

        private int _fillIndex;
        private float _timer = -1f;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_fillIndex < _slots.Length)
            {
                other.transform.position = _slots[_fillIndex].position;
                other.transform.parent = _slots[_fillIndex].transform;
                DragAndDrop.Instance.Drop();
                _fillIndex++;
                if (_fillIndex == _slots.Length)
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
                    foreach (var slot in _slots)
                    {
                        Destroy(slot.GetChild(0).gameObject);
                    }
                    _fillIndex = 0;
                    _progression.text = string.Empty;
                    _anim.SetBool("IsOpen", true);
                }
            }
        }
    }
}
