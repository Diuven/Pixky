using System;
using UnityEngine;

namespace Celestial_Body
{
    public class Star
    {
        private const int NameLength = 10;
        private const int RaLength = 10;
        private const int DecLength = 10;
        private const int MagLength = 4;


        // unit in degrees 
        public readonly float Ra, Dec, Mag;
        public readonly string Name;

        public Star(float ra, float dec, float mag, string name)
        {
            Ra = ra;
            Dec = dec;
            Name = name;
            Mag = mag;
        }

        public override string ToString()
        {
            return $"{Name,-NameLength} {Ra,RaLength:F6} {Dec,DecLength:F6} {Mag,MagLength:F2}";
        }

        public static Star Parse(string data)
        {
            try
            {
                var offset = 0;
                var name = data.Substring(offset, NameLength);
                offset += NameLength + 1;
                var ra = float.Parse(data.Substring(offset, RaLength));
                offset += RaLength + 1;
                var dec = float.Parse(data.Substring(offset, DecLength));
                offset += DecLength + 1;
                var mag = float.Parse(data.Substring(offset, MagLength));
                offset += MagLength + 1;
                return new Star(ra, dec, mag, name);
            }
            catch
            {
                Debug.LogWarning($"Couldn't parse {data}");
                return null;
            }
        }
    }
}