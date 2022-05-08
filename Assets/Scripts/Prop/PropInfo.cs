using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GamedevGBG.Prop
{
    public class PropInfo : MonoBehaviour
    {
        public PropType Type;

        public List<PropInfo> Inside = new();
        public string ID;

        public bool CanBeUsed(PropType allowedType)
        {
            if (Type != allowedType)
            {
                return false;
            }
            if (Type == PropType.SmallVial)
            {
                return true;
            }
            return Inside.Any();
        }
    }
}
