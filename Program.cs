using System;

using brab.colmap;

class Program
{
    static void Main(string[] args)
    {
        var recBinPath = "/media/boris/data/panopt/tjurbacken/map/reconstruction/0/";
        var recTxtPath = "/media/boris/data/panopt/tjurbacken/DJI_0007/reconstruction_txt/";

        // var imagesPath = "/media/boris/data/panopt/tjurbacken/DJI_0007/reconstruction_txt/images.txt";
        // var imagesPathBin = "/media/boris/data/panopt/tjurbacken/DJI_0007/reconstruction/images.bin";
        //var imagesPathBin = "/media/boris/data/panopt/tjurbacken/map/reconstruction/0/images.bin";
        // var points3DPath = "/media/boris/data/panopt/tjurbacken/DJI_0007/reconstruction_txt/points3D.txt";
        // var points3DPathBin = "/media/boris/data/panopt/tjurbacken/DJI_0007/reconstruction/points3D.bin";


        var rec = new Reconstruction(recBinPath);
        foreach (var img in rec.Images())
        {
            Console.WriteLine($"img ->{img}<-");
        }

        foreach (var point in rec.Points())
        {
             Console.WriteLine($"p3d ->{point}<-");
        }
    }
}
