using System.Collections.Generic;
using Celestial_Body;
using UnityEngine;

namespace Data_Loader
{
    public static class DataLoader
    {
        private const string V50ResourcePath = "v50";
        public static List<Star> LoadV50()
        {
            var text = Resources.Load<TextAsset>(V50ResourcePath).text;

            var result = new List<Star>();
            
            var lines = text.Split("\n" [0]);
            foreach (var line in lines)
            {
                if (line.Length == 0) continue;
                var star = Star.Parse(line.Trim());
                result.Add(star);
            }

            return result;
        }
    }
}