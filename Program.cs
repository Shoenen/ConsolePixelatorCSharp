using System.Drawing;

Console.Write("Insert here the path of the image you want to pixelate: ");
string? input = Console.ReadLine();

if (input == null) return;

Bitmap imageToWorkWith = new Bitmap(input);

String pixelatedName = input.Substring(input.LastIndexOf('\\'), input.IndexOf('.')-input.LastIndexOf('\\'));
Bitmap newPixelatedImage = new Bitmap(imageToWorkWith.Width, imageToWorkWith.Height);
Console.Write("Insert here how much you want to pixelate the image: ");
string? inputPixelationAmount = Console.ReadLine();
if (inputPixelationAmount == null) return;
Int32 pixelationAmount = Int32.Parse(inputPixelationAmount);

for (int x = 0; x < imageToWorkWith.Width - pixelationAmount; x += pixelationAmount)
{
    for (int y = 0; y < imageToWorkWith.Height - pixelationAmount; y += pixelationAmount)
    {
        List<Color> colorsList = new List<Color>(pixelationAmount*pixelationAmount);
        
        for (int x2 = 0; x2 < pixelationAmount; x2++)
        {
            for (int y2 = 0; y2 < pixelationAmount; y2++)
            {
                colorsList.Add(imageToWorkWith.GetPixel(x+x2, y+y2));
            }
        }
        Int32 redColor=0, greenColor=0, blueColor=0;
        foreach(Color c in colorsList)
        {
            redColor += c.R;
            greenColor += c.G;
            blueColor += c.B;
        }

        redColor /= (pixelationAmount * pixelationAmount);
        greenColor /= (pixelationAmount * pixelationAmount);
        blueColor /= (pixelationAmount * pixelationAmount);
        Color newColor = Color.FromArgb(redColor, greenColor, blueColor);
        
        for (int x2 = 0; x2 < pixelationAmount; x2++)
        {
            for (int y2 = 0; y2 < pixelationAmount; y2++)
            {
                newPixelatedImage.SetPixel(x+x2, y+y2, newColor);
            }
        }
    }
}
newPixelatedImage.Save(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+"\\"+pixelatedName+"_pixelated"+pixelationAmount+".png", System.Drawing.Imaging.ImageFormat.Png);