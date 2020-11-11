namespace RoomFilling
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;


    public class EncounterFactory:MonoBehaviour
    {
        public int difficulty = 1;
        [SerializeField]
        public List<Encounter> EncounterPool;
        private void Start()
        {
            GetEncounter();
        }

        public List<MonoBehaviour> GetEncounter(Room room)
        {
            var pool = GetEncounter();
            foreach (var enemy in pool)
            {
                enemy.GetComponent<Enemy>().room = room;
                room.PlaceObject(enemy);
                enemy.transform.position += Vector3.up * UnityEngine.Random.Range(-1, 1) + Vector3.right * UnityEngine.Random.Range(-1, 1);
            }
            return pool;
        }
        public List<MonoBehaviour> GetEncounter()
        {
            int j = UnityEngine.Random.Range(0, EncounterPool.Count);
            List<MonoBehaviour> pool = new List<MonoBehaviour>();
            foreach (var mob in EncounterPool[j].EncounterDescription)
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
    public class Encounter
    {
        [SerializeField]
        public List<GenericPoolItem<MonoBehaviour>> EncounterDescription;
    }
}
