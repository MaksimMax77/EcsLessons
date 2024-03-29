using Code.Triggers.Stateful;
using Unity.Entities;

namespace Code.Triggers.TriggerHandlers
{
    public abstract partial class TriggerHandlerSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var ecb = SystemAPI.GetSingleton<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(EntityManager.WorldUnmanaged);

            foreach (var (triggerEventBuffer, entity) in
                     SystemAPI.Query<DynamicBuffer<StatefulTriggerEvent>>()
                         .WithEntityAccess())
            {
                for (var i = 0; i < triggerEventBuffer.Length; ++i)
                {
                    var triggerEvent = triggerEventBuffer[i];
                    var otherEntity = triggerEvent.GetOtherEntity(entity);

                    if (triggerEvent.State == StatefulEventState.Enter)
                    {
                        OnTriggerEnter(ecb, otherEntity, entity);
                    }
                }
            }
        }

        public abstract void OnTriggerEnter(EntityCommandBuffer ecb, Entity otherEntity, Entity entity);
    }
}
