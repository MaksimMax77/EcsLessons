using Code.InputEcsControl;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Aspects;
using Unity.Transforms;

namespace Code.Shoot
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(UserInputSystem))]
    public partial struct ShootSystem: ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (gunLocalTransform, gunTransform, shootData, input) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRW<LocalToWorld>, RefRW<ShootData>, RefRW<UserInputData>>())
            {
                if (input.ValueRW.shoot == 0)
                {
                    shootData.ValueRW.shootTime = 0;
                    return;
                }
                
                var isTimeToShoot = ShootTimerUpdate(ref state, shootData);

                if (!isTimeToShoot)
                {
                    return;
                }
                
                var entity = state.EntityManager.Instantiate(shootData.ValueRW.projectile);
                SetProjectileCollisionResponse(ref state, entity);

                var localTransform = LocalTransform.FromPositionRotationScale(
                    gunTransform.ValueRW.Position + gunTransform.ValueRW.Forward,
                    gunLocalTransform.ValueRW.Rotation,
                    gunLocalTransform.ValueRW.Scale);

                var velocity = new PhysicsVelocity
                {
                    Linear = gunTransform.ValueRW.Forward * shootData.ValueRW.projectilePower,
                    Angular = float3.zero
                };

                state.EntityManager.SetComponentData(entity, localTransform);
                state.EntityManager.SetComponentData(entity, velocity);
            }
        }

        private bool ShootTimerUpdate(ref SystemState state, RefRW<ShootData> refRwShootData)
        {
            refRwShootData.ValueRW.shootTime -= SystemAPI.Time.DeltaTime;

            if (refRwShootData.ValueRW.shootTime > 0)
            {
                return false;
            }

            refRwShootData.ValueRW.shootTime = refRwShootData.ValueRW.shootDelay;
            return true;
        }

        private void SetProjectileCollisionResponse(ref SystemState state, Entity entity)
        {
            CollisionResponsePolicy collisionResponse = default;
                
            foreach (var projectilesCollisionResponse in SystemAPI.Query<RefRW<ProjectilesCollisionResponse>>())
            {
                collisionResponse = projectilesCollisionResponse.ValueRW.collisionResponse;
            }
            var aspect = state.EntityManager.GetAspect<ColliderAspect>(entity);
            aspect.SetCollisionResponse(collisionResponse);
        }
    }
}
