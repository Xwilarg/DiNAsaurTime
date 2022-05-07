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

        private bool _isMoving;

        private int _targetIndex;
        private int _oldIndex;

        private float _oldDist;

        private float _baseValue;
        private float _objValue;
        private float _actionTimer;

        private enum ActionState
        {
            GoDown,
            GoUp,
            Done
        }
        private ActionState _currentAction = ActionState.Done;

        private void Start()
        {
            _targetIndex = _vialsDeposit.TargetCount;
            _oldIndex = _targetIndex - 1;
            _oldDist = float.PositiveInfinity;
            _baseValue = _arm.transform.position.y;
            _objValue = _baseValue - .05f;
        }

        public void GoLeft()
        {
            if (_targetIndex > 0 && _currentAction == ActionState.Done)
            {
                _oldIndex = _targetIndex;
                _targetIndex--;
                _oldDist = float.PositiveInfinity;
            }
        }

        public void GoRight()
        {
            if (_targetIndex <_vialsDeposit.TargetCount && _currentAction == ActionState.Done)
            {
                _oldIndex = _targetIndex;
                _targetIndex++;
                _oldDist = float.PositiveInfinity;
            }
        }

        public void DoAction()
        {
            if (!_isMoving && _currentAction == ActionState.Done)
            {
                _currentAction = ActionState.GoDown;
                _actionTimer = 0f;
            }
        }

        private Vector3 GetPosition(int index)
        {
            if (index < _vialsDeposit.TargetCount)
            {
                return _vialsDeposit.GetPosition(index);
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
                _isMoving = true;
            }
            else
            {
                _isMoving = false;
            }

            if (_currentAction == ActionState.GoDown)
            {
                _actionTimer += Time.deltaTime;
                _arm.position = new Vector3(
                    x: _arm.transform.position.x,
                    y: Mathf.Lerp(_baseValue, _objValue, _actionTimer),
                    z: _arm.transform.position.z
                    );
                if (_actionTimer >= 1f)
                {
                    _actionTimer = 0f;
                    _currentAction = ActionState.GoUp;
                }
            }
            else if (_currentAction == ActionState.GoUp)
            {
                _actionTimer += Time.deltaTime;
                _arm.position = new Vector3(
                    x: _arm.transform.position.x,
                    y: Mathf.Lerp(_objValue, _baseValue, _actionTimer),
                    z: _arm.transform.position.z
                    );
                if (_actionTimer >= 1f)
                {
                    _actionTimer = 0f;
                    _currentAction = ActionState.Done;
                }
            }
        }
    }
}
