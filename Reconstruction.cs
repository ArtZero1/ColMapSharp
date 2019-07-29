using System;
using System.IO;
using System.Collections.Generic;

namespace brab.colmap
{
    public class Reconstruction
    {
        const string IMAGES_BIN = "images.bin";
        const string IMAGES_TXT = "images.txt";

        const string POINTS3D_BIN = "points3D.bin";
        const string POINTS3D_TXT = "points3D.txt";

        string DirPath;

        public Reconstruction(string dir)
        {
            DirPath = dir;
        }

        public IEnumerable<Image> Images()
        {
            foreach (var img in GetImagesObj().GetImages())
            {
                yield return img;
            }
        }

        Images GetImagesObj()
        {
            /* first check if there images file in binary format */
            var imgsPath = Path.Combine(DirPath, IMAGES_BIN);
            if (File.Exists(imgsPath))
            {
                return new ImagesBin(imgsPath);
            }

            /* try to fall-back on text format */
            imgsPath = Path.Combine(DirPath, IMAGES_TXT);
            if (File.Exists(imgsPath))
            {
                return new ImagesTxt(imgsPath);
            }

            throw new Exception($"no images file found in {DirPath}");
        }

        public IEnumerable<Point3D> Points()
        {
            foreach (var point in GetPoints3DObj().GetPoints())
            {
                yield return point;
            }

        }

        Points3D GetPoints3DObj()
        {
            /* first check if there points3D file in binary format */
            var pointsPath = Path.Combine(DirPath, POINTS3D_BIN);
            if (File.Exists(pointsPath))
            {
                return new Points3DBin(pointsPath);
            }

            /* try to fall-back on text format */
            pointsPath = Path.Combine(DirPath, POINTS3D_TXT);
            if (File.Exists(pointsPath))
            {
                return new Points3DTxt(pointsPath);
            }

            throw new Exception($"no points 3D file found in {DirPath}");
        }
    }
}
