using System;
using System.Linq;
using UnityEngine;

namespace GamedevGBG.Prop
{
    public abstract class AContainer : MonoBehaviour
    {
        protected GameObject[] _targets;

        public abstract int TargetCount { get; }
        public abstract Vector3 GetPosition(int index);
        public PropInfo GetPropInfo(int index)
        {
            if (_targets[index] == null)
            {
                return null;
            }
            return _targets[index].GetComponent<PropInfo>();
        }

        public bool IsEmpty(int index)
        {
            return _targets[index] == null;
        }

        protected void Init()
        {
            MachineManager.Instance.RegisterContainer(this);
            _targets = new GameObject[TargetCount];
        }

        public virtual int Remove(GameObject go)
        {
            for (int i = 0; i < _targets.Length; i++)
            {
                var t = _targets[i];
                if (t != null && t.GetInstanceID() == go.GetInstanceID())
                {
                    _targets[i] = null;
                    return i;
                }
            }
            return -1;
        }

        public virtual void Add(GameObject go)
        {
            if (!_targets.Any(x => x != null && x.GetInstanceID() == go.GetInstanceID()) && _targets.Any(x => x == null))
            {
                var index = Array.IndexOf(_targets, null);
                AddAtPosition(go, index);
            }
        }

        public void AddAtPosition(GameObject go, int index)
        {
            go.transform.position = GetPosition(index);
            _targets[index] = go;
        }
    }
}
