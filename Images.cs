using System.IO;
using System.Text;
using System.Collections.Generic;

namespace brab.colmap
{
    public class Point2D
    {
        public double x, y;
        public long Point3DId;

        override public string ToString()
        {
            return $"Point2D: {x}, {y} {Point3DId}";
        }
    }

    public class Image
    {
        public int Id;
        public string Name;
        public int CameraId;

        public double tx, ty, tz;

        /* rotation quaternion */
        public double qw, qx, qy, qz;

        public Point2D[] Points2D;

        override public string ToString()
        {
            return $"Image: Id {Id} qvec {qw} {qx} {qy} {qz} " +
                   $"tvec {tx} {ty} {tz} camID {CameraId} '{Name}'";
        }
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

    public class ImagesBin
    {
        string FilePath;

        public ImagesBin(string path)
        {
            FilePath = path;
        }

        public IEnumerable<Image> Images()
        {
            using (BinaryReader reader = new BinaryReader(File.Open(FilePath, FileMode.Open)))
            {
                var numImages = reader.ReadUInt64();
                for (ulong i = 0; i < numImages; i += 1)
                {
                    yield return ReadImage(reader);
                }
            }
        }

        Image ReadImage(BinaryReader reader)
        {
            var img = new Image();
            img.Id = reader.ReadInt32();

            img.qw = reader.ReadDouble();
            img.qx = reader.ReadDouble();
            img.qy = reader.ReadDouble();
            img.qz = reader.ReadDouble();

            img.tx = reader.ReadDouble();
            img.ty = reader.ReadDouble();
            img.tz = reader.ReadDouble();

            img.CameraId = reader.ReadInt32();
            img.Name = ReadImageName(reader);
            img.Points2D = ReadPoints2D(reader);

            return img;
        }

        string ReadImageName(BinaryReader reader)
        {
            var name = new StringBuilder();
            byte b;

            while ((b = reader.ReadByte()) != 0)
            {
                name.Append((char)b, 1);
            }
            return name.ToString();
        }

        Point2D ReadPoint2D(BinaryReader reader)
        {
            var point = new Point2D();

            point.x = reader.ReadDouble();
            point.y = reader.ReadDouble();
            point.Point3DId = reader.ReadInt64();

            return point;
        }

        Point2D[] ReadPoints2D(BinaryReader reader)
        {
            var numPoints = reader.ReadUInt64();
            var points = new Point2D[numPoints];

            for (ulong i = 0; i < numPoints; i += 1)
            {
                points[i] = ReadPoint2D(reader);
            }

            return points;
        }
    }
}
