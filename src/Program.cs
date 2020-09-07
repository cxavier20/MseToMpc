using System;
using System.Collections.Generic;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;

namespace MseToMpc
{
    class Program
    {
        private static readonly Size NormalSize = new Size(822, 1122);
        private static readonly Size HighResSize = new Size(3288, 4488);

        static void Main(string[] args)
        {
            var size = NormalSize;

            var imagePaths = new HashSet<string>();
            foreach (var arg in args) {
                if (arg == "-hires") {
                    size = HighResSize;
                } else {
                    var imagesFound = Directory.GetFiles(arg, "*.png", SearchOption.AllDirectories);
                    foreach (var imagePath in imagesFound) {
                        imagePaths.Add(imagePath);
                    }
                }
            }

            Console.WriteLine($"Processing {imagePaths.Count} image(s){(size == HighResSize ? " in high res mode" : "")}");
            foreach (var imagePath in imagePaths) {
                Console.Write($"Processing {imagePath}...");
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
                    img.Mutate((x) => x.Resize(size.Width, size.Height));
                    img.Save(imagePath);
                    Console.WriteLine($" Done!");
                }
            }
            Console.WriteLine("All done!");
        }
    }
}
