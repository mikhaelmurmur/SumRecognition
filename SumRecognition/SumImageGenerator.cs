using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace SumRecognition
{
    public static class SumImageGenerator
    {
        const int PICTUREWIDTH = 58;
        const int PICTUREHEIGHT = 95;
        static Image[] digitsImages = new Image[]
        {
            Image.FromFile(ApplicationInfo.instance.appPath+"\\images\\0.png"),
            Image.FromFile(ApplicationInfo.instance.appPath+"\\images\\1.png"),
            Image.FromFile(ApplicationInfo.instance.appPath+"\\images\\2.png"),
            Image.FromFile(ApplicationInfo.instance.appPath+"\\images\\3.png"),
            Image.FromFile(ApplicationInfo.instance.appPath+"\\images\\4.png"),
            Image.FromFile(ApplicationInfo.instance.appPath+"\\images\\5.png"),
            Image.FromFile(ApplicationInfo.instance.appPath+"\\images\\6.png"),
            Image.FromFile(ApplicationInfo.instance.appPath+"\\images\\7.png"),
            Image.FromFile(ApplicationInfo.instance.appPath+"\\images\\8.png"),
            Image.FromFile(ApplicationInfo.instance.appPath+"\\images\\9.png")
        };

        public static BitmapImage GetSumImage()
        {
            int firstNumber, secondNumber, sumNumber;
            Random random = new Random();
            int numbersLength = random.Next(1, 10);
            Bitmap resultImage = new Bitmap((numbersLength + 1) * PICTUREWIDTH, PICTUREHEIGHT * 3);
            Graphics image = Graphics.FromImage(resultImage);
            int digitIndex = random.Next(1, 9);
            firstNumber = digitIndex;
            image.Clear(Color.White);
            image.DrawImage(digitsImages[digitIndex], new System.Drawing.Point(PICTUREWIDTH, 0));
            for (int positionIndex = 2; positionIndex < numbersLength; positionIndex++)
            {
                digitIndex = random.Next(0, 9);
                firstNumber *= 10;
                firstNumber += digitIndex;
                image.DrawImage(digitsImages[digitIndex], new System.Drawing.Point(PICTUREWIDTH * positionIndex, 0));
            }

            digitIndex = random.Next(1, 9);
            secondNumber = digitIndex;
            image.DrawImage(digitsImages[digitIndex], new System.Drawing.Point(PICTUREWIDTH, PICTUREHEIGHT));
            for (int positionIndex = 2; positionIndex < numbersLength; positionIndex++)
            {
                digitIndex = random.Next(0, 9);
                secondNumber *= 10;
                secondNumber += digitIndex;
                image.DrawImage(digitsImages[digitIndex], new System.Drawing.Point(PICTUREWIDTH * positionIndex, PICTUREHEIGHT));
            }

            sumNumber = secondNumber + firstNumber;
            int[] digitArray = GetIntArrayFromNumber(sumNumber);
            int position = numbersLength != digitArray.Length ? 1 : 0;
            
            foreach (int digit in digitArray)
            {
                image.DrawImage(digitsImages[digit], new PointF(PICTUREWIDTH * position, PICTUREHEIGHT * 2));
                position++;
            }
            image.Save();
            resultImage.Save(ApplicationInfo.instance.appPath + "\\images\\new.gif", System.Drawing.Imaging.ImageFormat.Gif);
            return new BitmapImage(new Uri(ApplicationInfo.instance.appPath + "\\images\\new.gif"));//Bitmap2BitmapImage(resultImage);
        }

        static int[] GetIntArrayFromNumber(int number)
        {
            List<int> digits = new List<int>();
            while (number > 0)
            {
                digits.Add(number % 10);
                number /= 10;
            }
            digits.Reverse();
            return digits.ToArray();
        }

        static private BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        {
            BitmapSource i = Imaging.CreateBitmapSourceFromHBitmap(
                           bitmap.GetHbitmap(),
                           IntPtr.Zero,
                           Int32Rect.Empty,
                           BitmapSizeOptions.FromEmptyOptions());
            return (BitmapImage)i;
        }

    }
}
