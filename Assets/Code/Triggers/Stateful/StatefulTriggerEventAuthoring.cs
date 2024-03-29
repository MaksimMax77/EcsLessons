using Unity.Entities;
using UnityEngine;

namespace Code.Triggers.Stateful
{
    public struct StatefulTriggerEventExclude : IComponentData
    {
    }

    public class StatefulTriggerEventAuthoring : MonoBehaviour
    {
        private class Baker : Baker<StatefulTriggerEventAuthoring>
        {
            public override void Bake(StatefulTriggerEventAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddBuffer<StatefulTriggerEvent>(entity);
            }
        }
    }
}