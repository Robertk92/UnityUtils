using System;

namespace GameFramework
{
    public struct InputAxisEvent : IEquatable<InputAxisEvent>
    {
        public readonly int ActionId;
        public readonly InputEventPollingType InputEventPollingType;
        
        public InputAxisEvent(int actionId, InputEventPollingType inputEventPollingType)
        {
            this.InputEventPollingType = inputEventPollingType;
            this.ActionId = actionId;
        }

        public static bool operator ==(InputAxisEvent lhs, InputAxisEvent rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(InputAxisEvent lhs, InputAxisEvent rhs)
        {
            return !(lhs == rhs);
        }


        public bool Equals(InputAxisEvent other)
        {
            return string.Equals(ActionId, other.ActionId) && InputEventPollingType == other.InputEventPollingType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is InputAxisEvent && Equals((InputAxisEvent) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ActionId.GetHashCode()) * 397) ^ (int) InputEventPollingType;
            }
        }
    }
}
