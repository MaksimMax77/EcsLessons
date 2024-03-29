using Unity.Entities;
using UnityEngine;

namespace Code.InputEcsControl
{
    public class UserInputAuthoring : MonoBehaviour
    {
        [HideInInspector] public Vector2 inputMoveDirection;
        
        public class UserInputDataBaker : Baker<UserInputAuthoring>
        {
            public override void Bake(UserInputAuthoring authoring)
            {
                AddComponent(new UserInputData
                {
                    inputMoveDirection = authoring.inputMoveDirection
                });
            }
        }
    }
    
    public struct UserInputData : IComponentData
    {
        public Vector2 inputMoveDirection;
        public int shoot;
        public int spurt; 
    }
}
