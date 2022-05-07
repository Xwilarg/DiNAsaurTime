using GamedevGBG.Player;
using UnityEngine;

namespace GamedevGBG.Prop
{
    public class Machine : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _slots;

        private Animator _anim;

        private int _fillIndex;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_fillIndex < _slots.Length)
            {
                other.transform.position = _slots[_fillIndex].position;
                DragAndDrop.Instance.Drop();
                _fillIndex++;
                if (_fillIndex == _slots.Length)
                {
                    _anim.SetBool("IsOpen", false);
                    // TODO: Play close animation
                }
            }
        }
    }
}
