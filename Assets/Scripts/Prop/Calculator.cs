using System.Globalization;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GamedevGBG.Prop
{
    public class Calculator : MonoBehaviour
    {
        private string _number = string.Empty;
        private float _total;
        private Operation _nextOperation = Operation.Sum;

        private bool _isBroken;

        [SerializeField]
        private TMP_Text _result;

        private void Start()
        {
            CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;
        }

        private void ReadyNextOperation(Operation op)
        {
            switch (_nextOperation)
            {
                case Operation.Add:
                    _total += float.Parse(_number);
                    break;

                case Operation.Subscract:
                    _total -= float.Parse(_number);
                    break;

                case Operation.Multiply:
                    _total *= float.Parse(_number);
                    break;

                case Operation.Divide:
                    if (float.Parse(_number) == 0)
                    {
                        _isBroken = true;
                        _result.text = "ERROR";
                        var rb = GetComponent<Rigidbody>();
                        rb.AddForce((Vector3.up + Vector3.right * Random.Range(-.25f, .25f)) * 10f, ForceMode.Impulse);
                        rb.AddTorque(Random.rotation.eulerAngles, ForceMode.Impulse);
                        return;
                    }
                    else
                    {
                        _total /= float.Parse(_number);
                    }
                    break;

                case Operation.Sum:
                    _total = float.Parse(_number);
                    _number = string.Empty;
                    break;
            }

            _nextOperation = op;
            _result.text = _total.ToString();
            _number = string.Empty;
        }

        public void Add(char nb)
        {
            if (_isBroken)
            {
                return;
            }
            if (nb == '.')
            {
                if (!_number.Contains("."))
                {
                    _number = ".";
                }
            }
            else if (nb == '+')
            {
                ReadyNextOperation(Operation.Add);
            }
            else if (nb == '-')
            {
                ReadyNextOperation(Operation.Subscract);
            }
            else if (nb == '/')
            {
                ReadyNextOperation(Operation.Divide);
            }
            else if (nb == '*')
            {
                ReadyNextOperation(Operation.Multiply);
            }
            else if (nb == '=')
            {
                ReadyNextOperation(Operation.Sum);
            }
            else
            {
                _number += nb;
            }

            if (_number != string.Empty)
            {
                _result.text = _number;
            }
        }

        public void OnClick(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit))
                {
                    if (hit.collider.name.StartsWith("CALC_"))
                    {
                        Add(hit.collider.name[5]);
                    }
                }
            }
        }

        private enum Operation
        {
            Add,
            Multiply,
            Divide,
            Subscract,
            Sum
        }
    }
}
