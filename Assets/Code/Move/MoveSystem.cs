using Code.InputEcsControl;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Code.Move
{
    public partial struct MoveSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, moveData, inputData) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRW<MoveData>, RefRW<UserInputData>>()
                         .WithAll<MoveData>()
                         .WithAll<UserInputData>())
            {
                var speed = moveData.ValueRO.speed;
                var inputDir = inputData.ValueRW.inputMoveDirection;

                var deltaTime = SystemAPI.Time.DeltaTime;
                var moveDirection = new float3(inputDir.x, 0, inputDir.y);
                moveData.ValueRW.moveDirection = moveDirection;
                
                if (moveDirection.Equals(float3.zero))
                {
                    return;
                }
                
                transform.ValueRW.Position += moveDirection * speed * deltaTime;
                transform.ValueRW.Rotation = GetMoveDirectionQuaternion(moveDirection);
            }
        }
        
        private quaternion GetMoveDirectionQuaternion(float3 moveDirection)
        {
            var lookRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            return new quaternion(lookRotation.x, lookRotation.y, lookRotation.z, lookRotation.w);
        }
    }   
}
