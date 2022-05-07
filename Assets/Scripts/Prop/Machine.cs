﻿using GamedevGBG.Player;
using GamedevGBG.SO;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace GamedevGBG.Prop
{
    public class Machine : AContainer
    {
        [SerializeField]
        private MachineInfo _info;

        [SerializeField]
        private Transform[] _slots;

        [SerializeField]
        private TMP_Text _progression;

        [SerializeField]
        private bool _processOnDone;

        [SerializeField]
        private PropType _allowedType;

        [SerializeField]
        private AudioClip _onDoing, _onDone;

        private AudioSource _source;

        private Animator _anim;

        private float _timer = -1f;

        public override int TargetCount => _slots.Length;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _source = GetComponent<AudioSource>();
            Init();
        }

        private void OnTriggerEnter(Collider other)
        {
            // Make sure that the object isn't already there and that there is empty space
            if (other.CompareTag("Draggable") && other.GetComponent<PropInfo>().Type == _allowedType)
            {
                Add(other.gameObject);
            }
        }

        private IEnumerator WaitAndProcess()
        {
            yield return new WaitForSeconds(1f);
            if (_source != null && _onDoing != null)
            {
                _source.clip = _onDoing;
                _source.loop = true;
                _source.Play();
            }
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
                    _source.Stop();
                    if (_source != null && _onDone != null)
                    {
                        _source.clip = _onDone;
                        _source.loop = false;
                        _source.Play();
                    }
                }
            }
        }

        public override Vector3 GetPosition(int index)
        {
            return _slots[index].position;
        }

        public override void Add(GameObject go)
        {
            base.Add(go);
            DragAndDrop.Instance.Drop();
            if (!_targets.Any(x => x == null) && _processOnDone) // All emplacement full
            {
                _anim.SetBool("IsOpen", false);
                StartCoroutine(WaitAndProcess());
            }
        }
    }
}
