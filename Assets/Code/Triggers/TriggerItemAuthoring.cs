using Unity.Entities;
using UnityEngine;

namespace Code.Triggers
{
    public class TriggerItemAuthoring : MonoBehaviour
    {
        [SerializeField] private TriggerItemType _triggerItemType;
        [SerializeField] private float _effectValue; 
        
        public class TriggerItemBaker: Baker<TriggerItemAuthoring>
        {
            public override void Bake(TriggerItemAuthoring authoring)
            {
                AddComponent(new TriggerItemData
                {
                    triggerItemType = authoring._triggerItemType,
                    effectValue = authoring._effectValue
                });
            }
        }
    }

    public enum TriggerItemType
    {
        HealthItem,
        DamageItem,
        ProjectileBouncingAbility
    }

    public struct TriggerItemData : IComponentData
    {
        public TriggerItemType triggerItemType;
        public float effectValue;
    }
}
