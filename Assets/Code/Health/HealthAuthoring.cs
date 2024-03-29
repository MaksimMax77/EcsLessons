using Unity.Entities;
using UnityEngine;

namespace Code.Health
{
    public class HealthAuthoring : MonoBehaviour
    {
        [SerializeField] private float _health = 100;

        public class HealthDataBaker : Baker<HealthAuthoring>
        {
            public override void Bake(HealthAuthoring authoring)
            {
                AddComponent(new HealthData()
                {
                    health = authoring._health
                });
            }
        }
    }

    public struct HealthData: IComponentData
    {
        public float health;
    }
}
