using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class CFact
    {
        public string name { get; set; }
        public int value { get; set; }

        public CFact(string name, int value)
        {
            this.name = name;
            this.value = value;
        }
        //public bool isEqual(CFact anotherFact)
        //{
        //    return this.value == anotherFact.value;
        //}
    }

    public sealed class CWorld
    {
        private static readonly CWorld instance = new CWorld();
        private static List<CFact> facts;

        static CWorld()
        {
            facts = new List<CFact>();
        }

        private CWorld() {}

        public static CWorld Instance
        {
            get { return instance; }
        }

        public List<CFact> GetFacts()
        {
            return facts;
        }

        public CFact HasFact(string name)
        {
            return facts.Find(fa => fa.name == name);
        }

        public void ChangeFact(string name, int value)
        {
            CFact fact = facts.Find(fa => fa.name == name);
            if (fact != null)
            { 
                fact.value = value;
            }
            else
            {
                AddFact(name, value);
            }
        }
        public void AddFact(string name, int value)
        {
            CFact fact = new CFact(name, value);
            facts.Add(fact);
        }

        public void RemoveFact(string name)
        {
            CFact fact = facts.Find(fa => fa.name == name);
            if (fact != null)
            {
                facts.Remove(fact);
            }
        }

    }

}
