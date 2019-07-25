using System;

using brab.colmap;

class Program
{
    static void Main(string[] args)
    {
        var imagesPath = "/media/boris/data/panopt/tjurbacken/DJI_0007/reconstruction_txt/images.txt";
        var points3DPath = "/media/boris/data/panopt/tjurbacken/DJI_0007/reconstruction_txt/points3D.txt";

        var images = new ImagesTxt(imagesPath);
        foreach (var img in images.Images())
        {
            Console.WriteLine($"Ximg ->{img.Id} {img.qw}<-");
        }

        var points3D = new Points3DTxt(points3DPath);
        foreach (var point in points3D.Points())
        {
            Console.WriteLine($"Xp3d ->{point.Id}<-");
        }
    }
}
