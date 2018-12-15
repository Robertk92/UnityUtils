
using System;

namespace GameFramework
{
    public struct InputButtonEvent : IEquatable<InputButtonEvent>
    {
        public readonly string InputId;
        public readonly InputEventPollingType InputEventPollingType;
        public readonly InputEventType InputEventType;
        
        public InputButtonEvent(string inputId, InputEventPollingType inputEventPollingType, InputEventType inputEventType)
        {
            this.InputEventPollingType = inputEventPollingType;
            this.InputEventType = inputEventType;
            this.InputId = inputId;
        }
        
        public static bool operator==(InputButtonEvent lhs, InputButtonEvent rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(InputButtonEvent lhs, InputButtonEvent rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(InputButtonEvent other)
        {
            return string.Equals(InputId, other.InputId) && InputEventPollingType == other.InputEventPollingType && InputEventType == other.InputEventType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is InputButtonEvent && Equals((InputButtonEvent) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (InputId != null ? InputId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) InputEventPollingType;
                hashCode = (hashCode * 397) ^ (int) InputEventType;
                return hashCode;
            }
        }
    }
}
