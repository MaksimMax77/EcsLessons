using Unity.Assertions;
using Unity.Entities;
using Unity.Physics;

namespace Code.Triggers.Stateful
{
    public struct StatefulTriggerEvent : IBufferElementData, IStatefulSimulationEvent<StatefulTriggerEvent>
    {
        public Entity EntityA { get; }
        public Entity EntityB { get; }
        public int BodyIndexA { get; }
        public int BodyIndexB { get; }
        public ColliderKey ColliderKeyA { get; }
        public ColliderKey ColliderKeyB { get; }
        public StatefulEventState State { get; set; }

        public StatefulTriggerEvent(TriggerEvent triggerEvent)
        {
            EntityA = triggerEvent.EntityA;
            EntityB = triggerEvent.EntityB;
            BodyIndexA = triggerEvent.BodyIndexA;
            BodyIndexB = triggerEvent.BodyIndexB;
            ColliderKeyA = triggerEvent.ColliderKeyA;
            ColliderKeyB = triggerEvent.ColliderKeyB;
            State = default;
        }

        public Entity GetOtherEntity(Entity entity)
        {
            Assert.IsTrue((entity == EntityA) || (entity == EntityB));
            return (entity == EntityA) ? EntityB : EntityA;
        }
        

        public int CompareTo(StatefulTriggerEvent other) => ISimulationEventUtilities.CompareEvents(this, other);
    }
}