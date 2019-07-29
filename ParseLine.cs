using System.Text.RegularExpressions;
using System.Globalization;

namespace brab.colmap
{
    public class ParseLine
    {
        const string FLOAT = @"\-?\d*(\.\d*(e-\d*)?)?";

        static Regex ImgPosRegex = new Regex(
            /* image ID */
            @"^(?<id>\d*) " +
            /* rotation quaternion */
            $@"(?<qw>{FLOAT}) (?<qx>{FLOAT}) (?<qy>{FLOAT}) (?<qz>{FLOAT}) " +
            /* transform vector */
            $@"(?<tx>{FLOAT}) (?<ty>{FLOAT}) (?<tz>{FLOAT}) " +
            /* camera ID */
            @"(?<cam>\d*) " +
            /* image name */
            "(?<name>.*)",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        static Regex Point3DRegex = new Regex(
            /* point3D ID */
            @"^(?<id>\d*) " +
            /* position */
            $@"(?<x>{FLOAT}) (?<y>{FLOAT}) (?<z>{FLOAT}) " +
            /* color */
            @"(?<r>\d*) (?<g>\d*) (?<b>\d*) " +
            $@"(?<error>{FLOAT}) " +
            @".*",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        ///
        /// parse integer number culturally independent
        ///
        static int ParseInt(string txt)
        {
            return int.Parse(txt, CultureInfo.InvariantCulture);
        }

        ///
        /// parse integer number culturally independent
        ///
        static uint ParseUint(string txt)
        {
            return uint.Parse(txt, CultureInfo.InvariantCulture);
        }

        ///
        /// parse integer number culturally independent
        ///
        static ulong ParseUlong(string txt)
        {
            return ulong.Parse(txt, CultureInfo.InvariantCulture);
        }


        ///
        /// parse decimal number culturally independent
        ///
        static double ParseDouble(string txt)
        {
            return double.Parse(txt, CultureInfo.InvariantCulture);
        }

        static string Get(GroupCollection groups, string name)
        {
            return groups[name].Value;
        }

        public static Image ImagePose(string line)
        {
            var groups = ImgPosRegex.Match(line).Groups;

            // TODO: check that regexp matched successfully

            var img = new Image();

            img.Id = ParseInt(Get(groups, "id"));
            img.qw = ParseDouble(Get(groups, "qw"));
            img.qx = ParseDouble(Get(groups, "qx"));
            img.qy = ParseDouble(Get(groups, "qy"));
            img.qz = ParseDouble(Get(groups, "qz"));
            img.tx = ParseDouble(Get(groups, "tx"));
            img.ty = ParseDouble(Get(groups, "ty"));
            img.tz = ParseDouble(Get(groups, "tz"));

            img.CameraId = ParseInt(Get(groups, "cam"));
            img.Name = Get(groups, "name");

            return img;
        }

        public static Point3D Point3D(string line)
        {
            var groups = Point3DRegex.Match(line).Groups;

            // TODO: check that regexp matched successfully

            var point = new Point3D();

            point.Id = ParseUlong(Get(groups, "id"));

            point.x = ParseDouble(Get(groups, "x"));
            point.y = ParseDouble(Get(groups, "y"));
            point.z = ParseDouble(Get(groups, "z"));

            point.r = ParseUint(Get(groups, "r"));
            point.g = ParseUint(Get(groups, "g"));
            point.b = ParseUint(Get(groups, "b"));

            point.Error = ParseDouble(Get(groups, "error"));


            return point;
        }
    }
}