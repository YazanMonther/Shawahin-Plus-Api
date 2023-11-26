using ShawahinAPI.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.Mappers
{
    public static class ChargerPowerExtensions
    {
        public static string? GetDescription( ChargerPower? chargerPower)
        {
            switch (chargerPower)
            {
                case ChargerPower.Level1:
                    return "Level 1 (3 kW)";
                case ChargerPower.Level2:
                    return "Level 2 (7 kW)";
                case ChargerPower.DCFast20:
                    return "DC Fast (20 kW)";
                case ChargerPower.DCFast50:
                    return "DC Fast (50 kW)";
                case ChargerPower.DCFast100:
                    return "DC Fast (100 kW)";
                case ChargerPower.DCFast150:
                    return "DC Fast (150 kW)";
                case ChargerPower.DCFast200:
                    return "DC Fast (200 kW)";
                case ChargerPower.DCFast350:
                    return "DC Fast (350 kW)";
                default:
                    return chargerPower.ToString();
            }
        }
    }
}
