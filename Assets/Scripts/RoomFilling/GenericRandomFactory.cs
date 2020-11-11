namespace RoomFilling
{
    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class GenericRandomFactory:MonoBehaviour
    {
        [SerializeField]
        public List<GenericPoolItem<MonoBehaviour>> variantPool;
        public List<MonoBehaviour> GetPool()
        {
            List<MonoBehaviour> pool = new List<MonoBehaviour>();
            foreach (var mob in variantPool)
            {
                int number = mob.diceRoll.Roll();
                for (int i = 0; i < number; i++)
                {
                    pool.Add(Instantiate(mob.prefab));
                }
            }
            return pool;
        }

    }

    [Serializable]
    public class GenericPoolItem<T> where T : MonoBehaviour
    {
        public T prefab;
        public Dice diceRoll;
    }
    [Serializable]
    public class Dice
    {
        //2d4
        [SerializeField]
        int dieSides;
        [SerializeField]
        int Count;

        public Dice(int sides, int number)
        {
            this.dieSides = sides;
            this.Count = number;
        }

        public int Roll()
        {
            int sum = 0;
            for (int i = 0; i < Count; i++)
            {
                sum += UnityEngine.Random.Range(1, dieSides);
            }
            return sum;
        }
    }
}
