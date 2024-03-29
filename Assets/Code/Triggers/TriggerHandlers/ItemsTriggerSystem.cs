using Code.Health;
using Code.Shoot;
using Code.Triggers.Stateful;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

namespace Code.Triggers.TriggerHandlers
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(StatefulTriggerEventSystem))]
    public partial class ItemsTriggerSystem : TriggerHandlerSystem
    {
        public override void OnTriggerEnter(EntityCommandBuffer ecb, Entity otherEntity, Entity entity)
        {
            var triggerItemLookup = SystemAPI.GetComponentLookup<TriggerItemData>();
            if (!triggerItemLookup.TryGetComponent(entity, out var triggerItemData))
            {
                return;
            }

            switch (triggerItemData.triggerItemType)
            {
                case TriggerItemType.HealthItem:
                    OnTriggerChangeHealth(otherEntity, entity, ecb, triggerItemData, false);
                    break;
                case TriggerItemType.DamageItem:
                    OnTriggerChangeHealth(otherEntity, entity, ecb, triggerItemData, true);
                    break;
                case TriggerItemType.ProjectileBouncingAbility:
                    foreach (var (projectilesCollision, collisionResponseEntity) in
                             SystemAPI.Query<RefRW<ProjectilesCollisionResponse>>().WithEntityAccess())
                    {
                        projectilesCollision.ValueRW.collisionResponse = CollisionResponsePolicy.RaiseTriggerEvents;
                        ecb.DestroyEntity(entity);
                    }

                    break;
            }
        }

        private void OnTriggerChangeHealth(Entity otherEntity, Entity entity, EntityCommandBuffer ecb,
            TriggerItemData triggerItemData, bool isRemoveHealth)
        {
            var healthDataLookup = SystemAPI.GetComponentLookup<HealthData>();
            if (!healthDataLookup.TryGetComponent(otherEntity, out var healthData))
            {
                return;
            }
            
            switch (isRemoveHealth)
            {
                case true:
                    healthData.health -= triggerItemData.effectValue;
                    Debug.LogError("Trap = " + healthData.health);
                    break;
                case false:
                    healthData.health += triggerItemData.effectValue;
                    Debug.LogError("HealthAdd = " + healthData.health);
                    break;
            }

            ecb.SetComponent(otherEntity, healthData);
            ecb.DestroyEntity(entity);
        }
    }
}
