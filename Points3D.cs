using System.IO;
using System.Collections.Generic;

namespace brab.colmap
{
    public class Point3D
    {
        public ulong Id;

        /* position */
        public double x, y, z;

        /* color */
        public uint r, g, b;

        public double Error;

        public Track[] Track;

        override public string ToString()
        {
            return $"Point3D: Id {Id} pos {x} {y} {z} color {r} {g} {b} Error {Error}";
        }
    }

    public class Track
    {
        public int ImageId;
        public int Point2DIdx;

        override public string ToString()
        {
            return $"Track: {ImageId} {Point2DIdx}";
        }
    }

    ///
    /// Note: parsing track for each point is not implemented
    ///
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

    public class Points3DBin
    {
        string FilePath;

        public Points3DBin(string path)
        {
            FilePath = path;
        }

        public IEnumerable<Point3D> Points()
        {
            using (var reader = new BinaryReader(File.Open(FilePath, FileMode.Open)))
            {
                var numPoints = reader.ReadUInt64();

                for (ulong i = 0; i < numPoints; i += 1)
                {
                    yield return ReadPoint3D(reader);
                }
            }
        }

        Point3D ReadPoint3D(BinaryReader reader)
        {
            var point = new Point3D();

            point.Id = reader.ReadUInt64();

            point.x = reader.ReadDouble();
            point.y = reader.ReadDouble();
            point.z = reader.ReadDouble();

            point.r = reader.ReadByte();
            point.g = reader.ReadByte();
            point.b = reader.ReadByte();

            point.Error = reader.ReadDouble();
            point.Track = ReadTrack(reader);

            return point;
        }

        Track[] ReadTrack(BinaryReader reader)
        {
            var trackLen = reader.ReadUInt64();
            var track = new Track[trackLen];

            for (ulong i = 0; i < trackLen; i += 1)
            {
                track[i] = new Track();
                track[i].ImageId = reader.ReadInt32();
                track[i].Point2DIdx = reader.ReadInt32();
            }

            return track;
        }
    }
}
