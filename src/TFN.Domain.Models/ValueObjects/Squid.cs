using System;

namespace TFN.Domain.Models.ValueObjects
{
    public class Squid
    {
        public Guid Guid { get; }

        public string Value { get; }

        private Squid(string squidValue)
        {
            Value = squidValue;
            Guid = Decode(squidValue);

            if (Squid.From(Guid).Value != Value)
            {
                throw new InvalidOperationException($"Provided squid value '{squidValue}' is not a valid squid.");
            }
        }

        private Squid(Guid guid)
        {
            Value = Encode(guid);
            Guid = guid;
        }

        public Squid(Squid squid)
            : this(squid.Guid)
        {
        }

        public static Squid NewSquid()
        {
            return new Squid(Guid.NewGuid());
        }

        public static Squid From(Guid guid)
        {
            return new Squid(guid);
        }

        public static Squid From(string squidValue)
        {
            try
            {
                return new Squid(squidValue);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Value provided '{squidValue}' is not a valid squid.", ex);
            }
        }

        public static string Encode(string value)
        {
            var guid = new Guid(value);
            return Encode(guid);
        }

        public static string Encode(Guid guid)
        {
            var encoded = Convert
                .ToBase64String(guid.ToByteArray())
                .Replace("/", "_")
                .Replace("+", "-")
                .Substring(0, 22);

            return encoded;
        }

        public static Guid Decode(string value)
        {
            byte[] buffer = Convert.FromBase64String(PrepareSquidValueToBase64(value));

            return new Guid(buffer);
        }

        public static bool TryParse(string value, out Squid squid)
        {
            squid = null;

            if (value == null)
            {
                return false;
            }

            if (value.Length != 22)
            {
                return false;
            }

            if (!IsBase64(PrepareSquidValueToBase64(value)))
            {
                return false;
            }

            try
            {
                squid = new Squid(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool CanParse(string value)
        {
            return TryParse(value, out Squid squid);
        }

        public override bool Equals(object obj)
        {
            if (obj is Squid)
            {
                return Guid.Equals(((Squid)obj).Guid);
            }

            if (obj is Guid)
            {
                return Guid.Equals((Guid)obj);
            }

            return false;
        }

        public static bool operator ==(Squid x, Squid y)
        {
            if ((object)x == null)
            {
                return (object)y == null;
            }

            if ((object)y == null)
            {
                return (object)x == null;
            }

            return x.Guid == y.Guid;
        }

        public static bool operator !=(Squid x, Squid y)
        {
            return !(x == y);
        }

        public static implicit operator string(Squid squid)
        {
            return squid.Value;
        }

        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }

        private static string PrepareSquidValueToBase64(string squidValue)
        {
            return squidValue.Replace("_", "/").Replace("-", "+") + "==";
        }

        private static bool IsBase64(string base64String)
        {
            if (base64String.Contains(" ") ||
                base64String.Contains("\t") ||
                base64String.Contains("\r") ||
                base64String.Contains("\n"))
            {
                return false;
            }

            var index = base64String.Length - 1;

            if (base64String[index] == '=')
            {
                index--;
            }

            if (base64String[index] == '=')
            {
                index--;
            }

            for (var i = 0; i <= index; i++)
            {
                if (IsInvalid(base64String[i]))
                {
                    return false;
                }
            }

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool IsInvalid(char value)
        {
            var intValue = (Int32)value;

            // 1 - 9
            if (intValue >= 48 && intValue <= 57)
            {
                return false;
            }

            // A - Z
            if (intValue >= 65 && intValue <= 90)
            {
                return false;
            }

            // a - z
            if (intValue >= 97 && intValue <= 122)
            {
                return false;
            }

            // + or /
            return intValue != 43 && intValue != 47;
        }
    }
}