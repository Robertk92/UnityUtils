
using System;

namespace GameFramework
{
    public struct InputActionEvent : IEquatable<InputActionEvent>
    {
        public readonly int ActionId;
        public readonly InputEventPollingType InputEventPollingType;
        public readonly InputEventType InputEventType;
        
        public InputActionEvent(int actionId, InputEventPollingType inputEventPollingType, InputEventType inputEventType)
        {
            this.InputEventPollingType = inputEventPollingType;
            this.InputEventType = inputEventType;
            this.ActionId = actionId;
        }
        
        public static bool operator==(InputActionEvent lhs, InputActionEvent rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(InputActionEvent lhs, InputActionEvent rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(InputActionEvent other)
        {
            return string.Equals(ActionId, other.ActionId) && InputEventPollingType == other.InputEventPollingType && InputEventType == other.InputEventType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is InputActionEvent && Equals((InputActionEvent) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (ActionId.GetHashCode());
                hashCode = (hashCode * 397) ^ (int) InputEventPollingType;
                hashCode = (hashCode * 397) ^ (int) InputEventType;
                return hashCode;
            }
        }
    }
}
