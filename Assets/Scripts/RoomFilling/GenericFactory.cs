using System.Collections.Generic;
using UnityEngine;

namespace RoomFilling
{
    public class GenericFactory:MonoBehaviour
    {
        [SerializeField]
        private MonoBehaviour prefab;
        public MonoBehaviour GetNewInstance()
        {
            return Instantiate(prefab);
        }
    }
    [SerializeField]

    public class EntranceFactory : GenericFactory
    {

    }

}
