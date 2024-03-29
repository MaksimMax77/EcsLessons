using Unity.Entities;
using Unity.Physics;
using UnityEngine;

namespace Code.Shoot
{
    public class ProjectilesCollisionResponseAuthoring : MonoBehaviour
    {
        [SerializeField] private CollisionResponsePolicy _defaultCollisionResponsey;

        public class ProjectilesStateBaker : Baker<ProjectilesCollisionResponseAuthoring>
        {
            public override void Bake(ProjectilesCollisionResponseAuthoring authoring)
            {
                AddComponent(new ProjectilesCollisionResponse
                {
                    collisionResponse = authoring._defaultCollisionResponsey
                });
            }
        }
    }

    public struct ProjectilesCollisionResponse : IComponentData
    {
        public CollisionResponsePolicy collisionResponse;
    }
}
