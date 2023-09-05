using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.ObjectsSO
{
    [CreateAssetMenu()]
    public class RecipeSO : ScriptableObject
    {
        public List<KitchenObjectSO> KitchenObjectSos;
        public string RecipeName;
    }
}
