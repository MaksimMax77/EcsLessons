using Unity.Entities;
using Unity.Physics;

namespace Code.Triggers.Stateful
{
    public enum StatefulEventState : byte
    {
        Undefined,
        Enter,
        Stay,
        Exit
    }

    public interface IStatefulSimulationEvent<T> : IBufferElementData, ISimulationEvent<T>
    {
        public StatefulEventState State { get; set; }
    }
}