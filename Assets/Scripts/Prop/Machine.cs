using GamedevGBG.Player;
using GamedevGBG.Translation;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GamedevGBG.Prop
{
    public class Machine : AContainer
    {
        [SerializeField]
        private float _timeUntilCompletion;

        [SerializeField]
        private Transform[] _slots;

        [SerializeField]
        private TMP_Text _progression;

        [SerializeField]
        private string _endText;

        [SerializeField]
        private bool _processOnDone;

        [SerializeField]
        private PropType _allowedType;

        [SerializeField]
        private AudioClip _onDoing, _onDone;

        [SerializeField]
        private UnityEvent<string> _onComplete;

        private AudioSource _source;

        private Animator _anim;

        private float _timer = -1f;

        public override int TargetCount => _slots.Length;

        private void Start()
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
            _timer = _timeUntilCompletion;
        }

        private void Update()
        {
            if (_timer > 0f)
            {
                _timer -= Time.deltaTime;
                if (_progression != null)
                {
                    _progression.text = $"{(_timeUntilCompletion - _timer) * 100f / _timeUntilCompletion:0.00}%";
                }
                if (_timer <= 0f)
                {
                    _anim.SetBool("IsOpen", true);
                    _source.Stop();
                    if (_source != null && _onDone != null)
                    {
                        _source.clip = _onDone;
                        _source.loop = false;
                        _source.Play();
                    }
                    var endText = string.Empty;
                    if (!string.IsNullOrEmpty(_endText))
                    {
                        endText = Translate.Instance.Tr(_endText);
                    }
                    _progression.text = endText;
                    StartCoroutine(CleanText());
                    _onComplete?.Invoke(_targets[0].GetComponent<PropInfo>().ID);
                    foreach (var t in _targets)
                    {
                        Destroy(t);
                    }
                    _targets = new GameObject[_slots.Length];
                }
            }
        }

        private IEnumerator CleanText()
        {
            yield return new WaitForSeconds(3f);
            _progression.text = string.Empty;
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
