using Code.Shoot;
using Code.Triggers.Stateful;
using Unity.Entities;

namespace Code.Triggers.TriggerHandlers
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(StatefulTriggerEventSystem))]
    public partial class BulletsTriggerHandlerSystem: TriggerHandlerSystem
    {
        public override void OnTriggerEnter(EntityCommandBuffer ecb, Entity otherEntity, Entity entity)
        {
            var projectileLookup = SystemAPI.GetComponentLookup<ProjectileTag>();
            if (!projectileLookup.TryGetComponent(entity, out var projectileTag))
            {
                return;
            }
            
            ecb.DestroyEntity(entity);
        }
    }
}
