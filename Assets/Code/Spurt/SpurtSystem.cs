using Code.InputEcsControl;
using Code.Move;
using Unity.Entities;
using Unity.Transforms;

namespace Code.Spurt
{
    public partial struct SpurtSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (spurtData, inputData, moveData, localTransform, localToWorld) 
                     in SystemAPI.Query<RefRW<SpurtData>, RefRW<UserInputData>, RefRW<MoveData>, RefRW<LocalTransform>, RefRW<LocalToWorld>>())
            {
                if (inputData.ValueRO.spurt == 0)
                {
                    return;
                }
                
                localTransform.ValueRW.Position += localToWorld.ValueRW.Forward  * spurtData.ValueRW.distance;
            }
        }
    }
}
