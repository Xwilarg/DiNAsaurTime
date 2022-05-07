using System.Collections.Generic;
using UnityEngine;

namespace GamedevGBG.Prop
{
    public class PropInfo : MonoBehaviour
    {
        public PropType Type;

        public List<PropInfo> Inside = new();
    }
}
