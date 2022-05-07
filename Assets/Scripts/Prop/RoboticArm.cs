using UnityEngine;

namespace GamedevGBG.Prop
{
    public class RoboticArm : MonoBehaviour
    {
        [SerializeField]
        private Machine _vialsDeposit;

        [SerializeField]
        private Transform _targetObject;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private Transform _arm, _hand, _finger;

        private int _targetIndex;
        private int _oldIndex;

        private float _oldDist;

        private void Start()
        {
            _targetIndex = _vialsDeposit.SlotCount;
            _oldIndex = _targetIndex - 1;
            _oldDist = float.PositiveInfinity;
        }

        public void GoLeft()
        {
            if (_targetIndex > 0)
            {
                _oldIndex = _targetIndex;
                _targetIndex--;
                _oldDist = float.PositiveInfinity;
            }
        }

        public void GoRight()
        {
            if (_targetIndex <_vialsDeposit.SlotCount)
            {
                _oldIndex = _targetIndex;
                _targetIndex++;
                _oldDist = float.PositiveInfinity;
            }
        }

        public void DoAction()
        {
        }

        private Vector3 GetPosition(int index)
        {
            if (index < _vialsDeposit.SlotCount)
            {
                return _vialsDeposit.GetSlotPosition(index);
            }
            return _targetObject.position;
        }

        private void Update()
        {
            var target = GetPosition(_targetIndex);
            var dist = Vector3.Distance(_finger.transform.position, new Vector3(target.x, _finger.transform.position.y, target.z));
            if (dist < _oldDist)
            {
                _oldDist = dist;
                _arm.transform.position += _arm.transform.right * (_targetIndex > _oldIndex ? 1f : -1f) * _speed * Time.deltaTime;
            }
        }
    }
}
