using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Celestial_Body;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class PreprocessV50Data : MonoBehaviour
    {
        private const int BufferSize = 256;

        /*
         *  1-  4  I4     ---     HR       [1/9110]+ Harvard Revised Number
         *  5- 14  A10    ---     Name     Name, generally Bayer and/or Flamsteed name
         * 76- 77  I2     h       RAh      ?Hours RA, equinox J2000, epoch 2000.0 (1)
         * 78- 79  I2     min     RAm      ?Minutes RA, equinox J2000, epoch 2000.0 (1)
         * 80- 83  F4.1   s       RAs      ?Seconds RA, equinox J2000, epoch 2000.0 (1)
         *     84  A1     ---     DE-      ?Sign Dec, equinox J2000, epoch 2000.0 (1)
         * 85- 86  I2     deg     DEd      ?Degrees Dec, equinox J2000, epoch 2000.0 (1)
         * 87- 88  I2     arcmin  DEm      ?Minutes Dec, equinox J2000, epoch 2000.0 (1)
         * 89- 90  I2     arcsec  DEs      ?Seconds Dec, equinox J2000, epoch 2000.0 (1)
         *103-107  F5.2   mag     Vmag     ?Visual magnitude (1)
         */

        [MenuItem("Pixky/Preprocess V50 %F8")]
        private static void PreprocessV50()
        {
            Debug.Log("Preprocessing V50 dataset");
            
            const string readPath = "Assets/Data/V_50/catalog";
            const string writePath = "Assets/Data/Resources/v50.txt";

            // var data = AssetDatabase.LoadAssetAtPath<TextAsset>(path); // should change extension to txt or such
            // Debug.Log(data.text);

            var stars = new List<Star>();

            using (var fileStream = File.OpenRead(readPath))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        try
                        {
                            var mag = float.Parse(line.Substring(102, 5));
                            if (mag > 4) continue;
                            
                            var name = line.Substring(4, 10).Trim();
                            if (name.Length == 0) name = "Unnamed";
                            var rah = int.Parse(line.Substring(75, 2));
                            var ram = int.Parse(line.Substring(77, 2));
                            var ras = float.Parse(line.Substring(79, 4));

                            var de = line[83] == '-' ? -1 : 1;
                            var ded = int.Parse(line.Substring(84, 2));
                            var dem = int.Parse(line.Substring(86, 2));
                            var des = int.Parse(line.Substring(88, 2));

                            Debug.Log($"{name} {rah}, {ram}, {ras}, {de}, {ded}, {dem}, {des}, {mag}");
                            
                            var ra = (rah + ram / 60f + ras / 3600f) * 360 / 24f;
                            var dec = de * (ded + dem / 60f + des / 3600f);
                            var star = new Star(ra, dec, mag, name);
                            stars.Add(star);
                        }
                        catch
                        {
                            Debug.Log("Skipped");
                        }
                    }
                }
            }

            var lines = stars.ConvertAll((star) => star.ToString()).ToArray();

            File.WriteAllLines(writePath, lines);
            
            Debug.Log($"Finished preprocessing V50 dataset");
            Debug.Log($"Saved {stars.Count} entities");
        }
    }
}