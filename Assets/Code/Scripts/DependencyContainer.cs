using UnityEngine;

namespace Code.Scripts
{
    public class DependencyContainer
    {
        [RuntimeInitializeOnLoadMethod]
        public static void Execute()
        {
            Debug.Log("HAHAHA!");
        }
    }
}