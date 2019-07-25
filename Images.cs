using System.Collections.Generic;

namespace brab.colmap
{
    public class Image
    {
        public uint Id;
        public string Name;
        public uint CameraId;

        public float tx, ty, tz;

        /* rotation quaternion */
        public float qw, qx, qy, qz;
    }

    public class ImagesTxt
    {
        TextFile TxtFile;

        public ImagesTxt(string path)
        {
            TxtFile = new TextFile(path);
        }

        public IEnumerable<Image> Images()
        {
            var iter = TxtFile.Lines().GetEnumerator();
            while (iter.MoveNext())
            {
                yield return ParseLine.ImagePose(iter.Current);
                iter.MoveNext(); /* skip 'points' line for now */
            }
        }
    }
}
