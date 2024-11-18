using System;
using System.Drawing;

public class ConsoleTextPrinter
{
    public void PrintColoredText(string text, Color color)
    {
        ConsoleColor closestConsoleColor = GetClosestConsoleColor(color);
        Console.ForegroundColor = closestConsoleColor;
        Console.Write(text);
        Console.ResetColor();
    }

    private ConsoleColor GetClosestConsoleColor(Color color)
    {
        ConsoleColor closestColor = ConsoleColor.White;
        double smallestDistance = double.MaxValue;

        // Loop through all ConsoleColors and find the closest match
        foreach (ConsoleColor consoleColor in Enum.GetValues(typeof(ConsoleColor)))
        {
            Color consoleColorValue = GetColorFromConsoleColor(consoleColor);
            double distance = ColorDistance(color, consoleColorValue);

            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                closestColor = consoleColor;
            }
        }

        return closestColor;
    }

    // Helper method to get the approximate RGB value of a ConsoleColor
    private Color GetColorFromConsoleColor(ConsoleColor color)
    {
        return color switch
        {
            ConsoleColor.Black => Color.FromArgb(0, 0, 0),
            ConsoleColor.DarkBlue => Color.FromArgb(0, 0, 139),
            ConsoleColor.DarkGreen => Color.FromArgb(0, 100, 0),
            ConsoleColor.DarkCyan => Color.FromArgb(0, 139, 139),
            ConsoleColor.DarkRed => Color.FromArgb(139, 0, 0),
            ConsoleColor.DarkMagenta => Color.FromArgb(139, 0, 139),
            ConsoleColor.DarkYellow => Color.FromArgb(139, 139, 0),
            ConsoleColor.Gray => Color.FromArgb(192, 192, 192),
            ConsoleColor.DarkGray => Color.FromArgb(169, 169, 169),
            ConsoleColor.Blue => Color.FromArgb(0, 0, 255),
            ConsoleColor.Green => Color.FromArgb(0, 255, 0),
            ConsoleColor.Cyan => Color.FromArgb(0, 255, 255),
            ConsoleColor.Red => Color.FromArgb(255, 0, 0),
            ConsoleColor.Magenta => Color.FromArgb(255, 0, 255),
            ConsoleColor.Yellow => Color.FromArgb(255, 255, 0),
            ConsoleColor.White => Color.FromArgb(255, 255, 255),
            _ => Color.White,
        };
    }

    // Helper method to calculate the distance between two colors
    private double ColorDistance(Color color1, Color color2)
    {
        int rDiff = color1.R - color2.R;
        int gDiff = color1.G - color2.G;
        int bDiff = color1.B - color2.B;
        return Math.Sqrt(rDiff * rDiff + gDiff * gDiff + bDiff * bDiff);
    }

    public Color LerpColor(Color minColor, Color maxColor, int min, int max, int input)
    {
        // Clamp input to be within the range [min, max]
        input = Math.Clamp(input, min, max);

        // Calculate the normalized ratio of input within the range [min, max]
        float t = (float)(input - min) / (max - min);

        // Interpolate each color channel
        int r = (int)(minColor.R + (maxColor.R - minColor.R) * t);
        int g = (int)(minColor.G + (maxColor.G - minColor.G) * t);
        int b = (int)(minColor.B + (maxColor.B - minColor.B) * t);

        // Return the interpolated color
        return Color.FromArgb(r, g, b);
    }
}
