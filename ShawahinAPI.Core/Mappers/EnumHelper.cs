using ShawahinAPI.Core.Enums;

namespace ShawahinAPI.Core.Mappers
{
    public static class EnumHelper
    {
        public static T? ParseEnum<T>(string? value) where T : struct
        {
            if (typeof(T) == typeof(ChargerPower))
            {
                return ParseChargerType(value) as T?;
            }

            if (Enum.TryParse(value, out T result))
            {
                return result;
            }
            else
            {
                return null; // Return null for invalid enum values
            }
        }

        private static ChargerPower ParseChargerType(string? value)
        {
            switch (value?.ToLower())
            {
                case "level 1 (3 kw)":
                    return ChargerPower.Level1;
                case "level 2 (7 kw)":
                    return ChargerPower.Level2;
                case "dc fast (20 kw)":
                    return ChargerPower.DCFast20;
                case "dc fast (50 kw)":
                    return ChargerPower.DCFast50;
                case "dc fast (100 kw)":
                    return ChargerPower.DCFast100;
                case "dc fast (150 kw)":
                    return ChargerPower.DCFast150;
                case "dc fast (200 kw)":
                    return ChargerPower.DCFast200;
                case "dc fast (350 kw)":
                    return ChargerPower.DCFast350;
                default:
                    throw new ArgumentException($"Invalid value '{value}' for ChargerPower enum");
            }
        }
    }
}