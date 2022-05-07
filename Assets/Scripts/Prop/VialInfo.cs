using GamedevGBG.BodyPart;
using System;
using UnityEngine;

namespace GamedevGBG.Prop
{
    [Serializable]
    public class VialInfo
    {
        public string ID;
        public Material Mat;
        public ContentType Type;
        public AnimalType Animal;
        public GameObject Prefab;
    }
}
