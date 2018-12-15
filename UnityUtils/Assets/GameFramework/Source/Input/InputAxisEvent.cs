using System;

namespace GameFramework
{
    public struct InputAxisEvent : IEquatable<InputAxisEvent>
    {
        public readonly string InputId;
        public readonly InputEventPollingType InputEventPollingType;
        
        public InputAxisEvent(string inputId, InputEventPollingType inputEventPollingType)
        {
            this.InputEventPollingType = inputEventPollingType;
            this.InputId = inputId;
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
            return string.Equals(InputId, other.InputId) && InputEventPollingType == other.InputEventPollingType;
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
                return ((InputId != null ? InputId.GetHashCode() : 0) * 397) ^ (int) InputEventPollingType;
            }
        }
    }
}
