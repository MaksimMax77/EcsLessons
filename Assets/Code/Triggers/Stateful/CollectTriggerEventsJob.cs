using Unity.Burst;
using Unity.Collections;
using Unity.Physics;

namespace Code.Triggers.Stateful
{
    [BurstCompile]
    public struct CollectTriggerEventsJob : ITriggerEventsJob
    {
        public NativeList<StatefulTriggerEvent> TriggerEvents;
        public void Execute(TriggerEvent triggerEvent) => TriggerEvents.Add(new StatefulTriggerEvent(triggerEvent));
    }
}
