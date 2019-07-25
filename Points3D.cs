using System.Collections.Generic;

namespace brab.colmap
{
    public class Point3D
    {
        public uint Id;

        /* position */
        public float x, y, z;

        /* color */
        public uint r, g, b;
    }

    public class Points3DTxt
    {
        TextFile TxtFile;

        public Points3DTxt(string path)
        {
            TxtFile = new TextFile(path);
        }

        public IEnumerable<Point3D> Points()
        {
            var iter = TxtFile.Lines().GetEnumerator();
            while (iter.MoveNext())
            {
                yield return ParseLine.Point3D(iter.Current);
            }
        }
    }
}
