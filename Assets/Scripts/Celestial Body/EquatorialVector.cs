using UnityEngine;

namespace Celestial_Body
{
    public class EquatorialVector
    {
        public const int ParseLength = 8 + 7;
        public readonly float Ra, Dec; // in degree

        public readonly int rah, ram;
        public readonly float ras;
        public readonly int de, ded, dem, des;


        public EquatorialVector(float ra, float dec)
        {
            Debug.Assert(0 <= ra && ra < 360);
            Debug.Assert(-90 <= dec && dec <= 90);
            Ra = ra;
            Dec = dec;

            var raHms = ra * 24 / 360;
            rah = Mathf.FloorToInt(raHms);
            ram = Mathf.FloorToInt((raHms % 1) * 60);
            ras = (raHms - rah) * 3600 - ram * 60;

            var absDec = dec;
            de = absDec < 0 ? -1 : 1;
            absDec *= de;
            ded = Mathf.FloorToInt(absDec);
            dem = Mathf.FloorToInt((absDec % 1) * 60);
            des = Mathf.FloorToInt((absDec - ded) * 3600 - dem * 60);

            Debug.Assert(0 <= rah && rah < 24);
            Debug.Assert(0 <= ram && ram < 60);
            Debug.Assert(0 <= ras && ras < 60);
            Debug.Assert(de == 1 || de == -1);
            Debug.Assert(-90 <= ded && ded <= 90);
            Debug.Assert(0 <= dem && dem < 60);
            Debug.Assert(0 <= des && des < 60);
        }

        public EquatorialVector(int rah, int ram, float ras, int de, int ded, int dem, int des)
        {
            Debug.Assert(0 <= rah && rah < 24);
            Debug.Assert(0 <= ram && ram < 60);
            Debug.Assert(0 <= ras && ras < 60);
            Debug.Assert(de == 1 || de == -1);
            Debug.Assert(-90 <= ded && ded <= 90);
            Debug.Assert(0 <= dem && dem < 60);
            Debug.Assert(0 <= des && des < 60);

            this.rah = rah;
            this.ram = ram;
            this.ras = ras;

            this.de = de;
            this.ded = ded;
            this.dem = dem;
            this.des = des;

            this.Ra = (rah + ram / 60f + ras / 3600f) * 360 / 24f;
            this.Dec = de * (ded + dem / 60f + des / 3600f);

            Debug.Assert(0 <= this.Ra && this.Ra < 360);
            Debug.Assert(-90 <= this.Dec && this.Dec <= 90);
        }

        public override string ToString()
        {
            var sign = de == -1 ? '-' : ' ';
            return $"{rah:D2}{ram:D2}{ras:00.0}{sign}{ded:D2}{dem:D2}{des:D2}";
        }

        public static EquatorialVector Parse(string data)
        {
            var rah = int.Parse(data.Substring(0, 2));
            var ram = int.Parse(data.Substring(2, 2));
            var ras = float.Parse(data.Substring(4, 4));

            var de = data[8] == '-' ? -1 : 1;
            var ded = int.Parse(data.Substring(9, 2));
            var dem = int.Parse(data.Substring(11, 2));
            var des = int.Parse(data.Substring(13, 2));

            return new EquatorialVector(rah, ram, ras, de, ded, dem, des);
        }

        public static float GetAngle(EquatorialVector v, EquatorialVector w)
        {
            // TODO
            return 0;
        }

        public static Vector2 GetDirection(EquatorialVector v, EquatorialVector w)
        {
            // TODO
            return Vector2.zero;
        }

        
    }
}