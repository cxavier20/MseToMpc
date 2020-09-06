using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;

namespace MseToMpc
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var dirPath in args) {
                var images = Directory.GetFiles(dirPath, "*.png", SearchOption.AllDirectories);
                foreach (var imagePath in images) {
                    using (var img = Image.Load(imagePath)) {
                        img.Mutate((x) => x.Resize(
                            405,
                            553,
                            KnownResamplers.Bicubic,
                            new Rectangle(0, 0, 375, 523),
                            new Rectangle(15, 15, 375, 523),
                            false
                        ));
                        img.Mutate((x) => x.Fill(Color.Black, new Rectangle(0, 0, 30, 553)));
                        img.Mutate((x) => x.Fill(Color.Black, new Rectangle(375, 0, 30, 553)));
                        img.Mutate((x) => x.Fill(Color.Black, new Rectangle(0, 0, 405, 30)));
                        img.Mutate((x) => x.Fill(Color.Black, new Rectangle(0, 523, 405, 553)));
                        img.Mutate((x) => x.Resize(3288, 4488));
                        img.Save(imagePath);
                    }
                }
            }
            Console.WriteLine("Done!");
        }
    }
}
