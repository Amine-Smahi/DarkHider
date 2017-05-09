using System.Drawing;

namespace DarkHider
{
    internal class Stegnography
    {
        public enum State
        {
            Hiding,
            Filling_With_Zeros
        }

        public static Bitmap embedText(string text, Bitmap bmp)
        {
            // initially, we'll be hiding characters in the image
            var state = State.Hiding;

            // holds the index of the character that is being hidden
            var charIndex = 0;

            // holds the value of the character converted to integer
            var charValue = 0;

            // holds the index of the color element (R or G or B) that is currently being processed
            long pixelElementIndex = 0;

            // holds the number of trailing zeros that have been added when finishing the process
            var zeros = 0;

            // hold pixel elements
            int R = 0, G = 0, B = 0;

            int i;


            // pass through the rows
            for (i = 0; i < bmp.Height; i++)
                if (i != null)
                    for (var j = 0; j < bmp.Width; j++)
                    {
                        // holds the pixel that is currently being processed
                        var pixel = bmp.GetPixel(j, i);

                        // now, clear the least significant bit (LSB) from each pixel element
                        R = pixel.R - pixel.R % 2;
                        G = pixel.G - pixel.G % 2;
                        B = pixel.B - pixel.B % 2;

                        // for each pixel, pass through its elements (RGB)
                        for (var n = 0; n < 3; n++)
                        {
                            // check if new 8 bits has been processed
                            if (pixelElementIndex % 8 == 0)
                            {
                                // check if the whole process has finished
                                // we can say that it's finished when 8 zeros are added
                                if (state == State.Filling_With_Zeros && zeros == 8)
                                {
                                    // apply the last pixel on the image
                                    // even if only a part of its elements have been affected
                                    if ((pixelElementIndex - 1) % 3 < 2)
                                        bmp.SetPixel(j, i, Color.FromArgb(R, G, B));

                                    // return the bitmap with the text hidden in
                                    return bmp;
                                }

                                // check if all characters has been hidden
                                if (charIndex >= text.Length)
                                    state = State.Filling_With_Zeros;
                                else
                                    charValue = text[charIndex++];
                            }

                            // check which pixel element has the turn to hide a bit in its LSB
                            switch (pixelElementIndex % 3)
                            {
                                case 0:
                                {
                                    if (state == State.Hiding)
                                    {
                                        // the rightmost bit in the character will be (charValue % 2)
                                        // to put this value instead of the LSB of the pixel element
                                        // just add it to it
                                        // recall that the LSB of the pixel element had been cleared
                                        // before this operation
                                        R += charValue % 2;

                                        // removes the added rightmost bit of the character
                                        // such that next time we can reach the next one
                                        charValue /= 2;
                                    }
                                }
                                    break;
                                case 1:
                                {
                                    if (state == State.Hiding)
                                    {
                                        G += charValue % 2;

                                        charValue /= 2;
                                    }
                                }
                                    break;
                                case 2:
                                {
                                    if (state == State.Hiding)
                                    {
                                        B += charValue % 2;

                                        charValue /= 2;
                                    }

                                    bmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                                }
                                    break;
                            }

                            pixelElementIndex++;

                            if (state == State.Filling_With_Zeros)
                                zeros++;
                        }
                    }

            return bmp;
        }

        public static string extractText(Bitmap bmp)
        {
            var colorUnitIndex = 0;
            var charValue = 0;

            // holds the text that will be extracted from the image
            var extractedText = string.Empty;

            // pass through the rows
            for (var i = 0; i < bmp.Height; i++)
            for (var j = 0; j < bmp.Width; j++)
            {
                var pixel = bmp.GetPixel(j, i);

                // for each pixel, pass through its elements (RGB)
                for (var n = 0; n < 3; n++)
                {
                    switch (colorUnitIndex % 3)
                    {
                        case 0:
                        {
                            // get the LSB from the pixel element (will be pixel.R % 2)
                            // then add one bit to the right of the current character
                            // this can be done by (charValue = charValue * 2)
                            // replace the added bit (which value is by default 0) with
                            // the LSB of the pixel element, simply by addition
                            charValue = charValue * 2 + pixel.R % 2;
                        }
                            break;
                        case 1:
                        {
                            charValue = charValue * 2 + pixel.G % 2;
                        }
                            break;
                        case 2:
                        {
                            charValue = charValue * 2 + pixel.B % 2;
                        }
                            break;
                    }

                    colorUnitIndex++;

                    // if 8 bits has been added, then add the current character to the result text
                    if (colorUnitIndex % 8 == 0)
                    {
                        // reverse? of course, since each time the process happens on the right (for simplicity)
                        charValue = reverseBits(charValue);

                        // can only be 0 if it is the stop character (the 8 zeros)
                        if (charValue == 0)
                            return extractedText;

                        // convert the character value from int to char
                        var c = (char) charValue;

                        // add the current character to the result text
                        extractedText += c.ToString();
                    }
                }
            }

            return extractedText;
        }

        public static int reverseBits(int n)
        {
            var result = 0;

            for (var i = 0; i < 8; i++)
            {
                result = result * 2 + n % 2;

                n /= 2;
            }

            return result;
        }
    }
}