using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Code.Move
{
    public class MoveAuthoring : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public class MoveBaker: Baker<MoveAuthoring>
        {
            public override void Bake(MoveAuthoring authoring)
            {
                AddComponent(new MoveData
                {
                    speed = authoring._speed
                });
            }
        }
    }

    public struct MoveData : IComponentData
    {
        public float speed;
        public float3 moveDirection; 
    }
}