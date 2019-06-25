using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output"); ;

            ImageProcess imageProcess = new ImageProcess();

            for (int index = 0; index < 10; index++)
            {
                imageProcess.Clean(destinationPath);
                Stopwatch sw1 = new Stopwatch();
                Stopwatch sw2 = new Stopwatch();
                Stopwatch sw3 = new Stopwatch();

                // Original
                imageProcess.Clean(destinationPath);
                sw1.Start();
                imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
                sw1.Stop();
                GC.Collect();
                Thread.Sleep(3000);

                // PLINQ
                imageProcess.Clean(destinationPath);
                sw2.Start();
                imageProcess.ResizeImagesPLINQ(sourcePath, destinationPath, 2.0);
                sw2.Stop();
                GC.Collect();
                Thread.Sleep(3000);

                // Async
                imageProcess.Clean(destinationPath);
                sw3.Start();
                imageProcess.ResizeImagesAsync(sourcePath, destinationPath, 2.0);
                sw3.Stop();
                GC.Collect();
                Thread.Sleep(3000);

                Console.WriteLine($"Original - 第{index+1}次花費時間: {sw1.ElapsedMilliseconds} ms");
                Console.WriteLine($"PLINQ - 第{index+1}次花費時間: {sw2.ElapsedMilliseconds} ms");
                Console.WriteLine($"Async - 第{index+1}次花費時間: {sw3.ElapsedMilliseconds} ms");
                Console.WriteLine($"========================================================");
            }
        }
    }
}
