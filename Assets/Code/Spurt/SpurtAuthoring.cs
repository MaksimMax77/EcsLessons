using Unity.Entities;
using UnityEngine;

namespace Code.Spurt
{
    public class SpurtAuthoring : MonoBehaviour
    {
        [SerializeField] private float _distance;

        public class SpurtDataBaker: Baker<SpurtAuthoring>
        {
            public override void Bake(SpurtAuthoring authoring)
            {
                AddComponent(new SpurtData
                {
                    distance = authoring._distance,
                });
            }
        }
    }
    
    public struct SpurtData: IComponentData
    {
        public float distance;
    }
}
