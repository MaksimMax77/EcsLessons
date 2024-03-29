using Unity.Entities;
using UnityEngine;

namespace Code.Shoot
{
    public class ProjectileAuthoring : MonoBehaviour
    {
        public class ProjectileBaker : Baker<ProjectileAuthoring>
        {
            public override void Bake(ProjectileAuthoring authoring)
            {
                AddComponent(new ProjectileTag());
            }
        }
    }

    public struct ProjectileTag : IComponentData
    {
    }
}
