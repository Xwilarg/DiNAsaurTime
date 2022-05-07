using UnityEngine;
using UnityEngine.InputSystem;

namespace GamedevGBG.Prop
{
    public class MachineManager : MonoBehaviour
    {
        public static MachineManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        [SerializeField]
        private Machine[] _machines;

        [SerializeField]
        private Calculator _calculator;

        [SerializeField]
        private RoboticArm _arm;

        public void RemoveFromMachine(GameObject go)
        {
            foreach (var machine in _machines)
            {
                machine.Remove(go);
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
                        _calculator.Add(hit.collider.name[5]);
                    }
                    else if (hit.collider.name == "CONTROLS_LEFT")
                    {
                        _arm.GoLeft();
                    }
                    else if (hit.collider.name == "CONTROLS_RIGHT")
                    {
                        _arm.GoRight();
                    }
                    else if (hit.collider.name == "CONTROLS_ACTION")
                    {
                        _arm.DoAction();
                    }
                }
            }
        }
    }
}
