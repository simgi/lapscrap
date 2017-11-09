using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace lapscrap.DAL
{
    public class Util
    {
        public static int parseInt(string nrString)
        {
            int x = -1;
            Regex digitsOnly = new Regex(@"[^\d]");
            Int32.TryParse(digitsOnly.Replace(nrString, ""), out x);
            return x;
        }
        public static int firstInt(string nrString)
        {
            // Funktioniert nur wenn string mit Zahl beginnt.
            int x = -1;
            nrString = nrString.Trim();
            string s = new String(nrString.TakeWhile(Char.IsDigit).ToArray());
            Int32.TryParse(s, out x);
            return x;
        }
        public static float parseFloat(string nrString)
        {
            float x = -1.0f;
            string s = Regex.Match(nrString, @"[\d]{1,9}([.|,][\d]{1,2})?").Groups[0].Value;
            float.TryParse(s, out x);
            return x;
        }
        public static float firstFloat(string nrString)
        {
            NumberStyles style;
            CultureInfo culture;
            style = NumberStyles.AllowDecimalPoint;
            culture = CultureInfo.CreateSpecificCulture("en-US");
            float x = -1.0f;
            string s = Regex.Match(nrString, @"[-+]?[0-9]*\.[0-9]+").Groups[0].Value;
            //tryparse ignoriert den Punkt als Dezimaltrenner aufgrund der Landeseinstellung (",") -> auf US setzen.
            float.TryParse(s, style, culture, out x);
            return x;
        }
    }
}