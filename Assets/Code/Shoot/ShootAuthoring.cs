using Unity.Entities;
using UnityEngine;

namespace Code.Shoot
{
    public class ShootAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private float _projectilePower;
        [SerializeField] private float _shootDelay;

        public class ShootDataBaker: Baker<ShootAuthoring>
        {
            public override void Bake(ShootAuthoring authoring)
            {
                AddComponent(new ShootData
                {
                    projectile = GetEntity(authoring._projectilePrefab, TransformUsageFlags.Dynamic),
                    projectilePower = authoring._projectilePower,
                    shootDelay = authoring._shootDelay
                });
            }
        }
    }

    public struct ShootData : IComponentData
    {
        public Entity projectile;
        public float projectilePower;
        public float shootDelay;
        public float shootTime;
    }
}
