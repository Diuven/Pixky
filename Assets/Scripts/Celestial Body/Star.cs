using System;
using UnityEngine;

namespace Celestial_Body
{
    public class Star
    {
        private const int NameLength = 10;
        private const int MagLength = 4;

        // unit in degrees 
        public readonly EquatorialVector Coord;
        public readonly float Mag;
        public readonly string Name;

        public Star(EquatorialVector coord, float mag, string name)
        {
            Coord = coord;
            Name = name;
            Mag = mag;
        }

        public override string ToString()
        {
            return $"{Name,-NameLength} {Coord,EquatorialVector.ParseLength} {Mag,MagLength:F2}";
        }

        public static Star Parse(string data)
        {
            try
            {
                var offset = 0;
                var name = data.Substring(offset, NameLength);
                offset += NameLength + 1;
                var vec = EquatorialVector.Parse(data.Substring(offset, EquatorialVector.ParseLength));
                offset += EquatorialVector.ParseLength + 1;
                var mag = float.Parse(data.Substring(offset, MagLength));
                offset += MagLength + 1;
                return new Star(vec, mag, name);
            }
            catch
            {
                Debug.LogWarning($"Couldn't parse {data}");
                return null;
            }
        }
    }
}