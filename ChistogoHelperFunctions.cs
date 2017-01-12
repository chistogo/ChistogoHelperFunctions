using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChistogoHelperFunctions
{
    static class HelperFunctions
    {

        
        /// <summary>
        /// Give it a Control you want to take a screenshot of. This can be the whole window or just
        /// an individual control.  You can also prompt to save the image if you give a 'true' in the second
        /// parameter
        /// </summary>
        /// <param name="form">Control you want to take a screenshot of</param>
        /// <param name="promptSave">Prompt to save the image</param>
        /// <returns>Return a bitmap of the selected control element</returns>
        public static Bitmap takeScreenshot(Control form , Boolean promptSave = false)
        {
            {
                int xLocation;
                int yLocation;

                //This will get the absolute position. If you are taking a picture of whole window you need
                //to just get the regular coordinates
                if(form.Parent == null)
                {
                    xLocation = form.Location.X;
                    yLocation = form.Location.Y;
                }else
                {
                    //Get the postion relative to screen
                    Point locationOnForm = form.PointToScreen(Point.Empty);
                    xLocation = locationOnForm.X;
                    yLocation = locationOnForm.Y;
                }

                
                //Take Screenshot at the location of the form
                int ix, iy, iw, ih;
                ix = xLocation;
                iy = yLocation;
                iw = form.Width + xLocation;
                ih = form.Height + yLocation;
                Bitmap image = new Bitmap(iw, ih,
                       System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(image);
                g.CopyFromScreen(ix, iy, ix, iy,
                         new System.Drawing.Size(iw, ih),
                         CopyPixelOperation.SourceCopy);

                // Crop the Picture
                Bitmap nb = new Bitmap(form.Width, form.Height);
                g = Graphics.FromImage(nb);
                g.DrawImage(image, -xLocation, -yLocation);
                image =  nb;



                if (promptSave) // Checking to see if user wants to save
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.DefaultExt = "png";
                    dlg.Filter = "Png Files|*.png";
                    DialogResult res = dlg.ShowDialog();
                    if (res == System.Windows.Forms.DialogResult.OK)
                    {
                        image.Save(dlg.FileName, ImageFormat.Png);
                    }
                    return image;
                }else 
                {
                    return image;
                }


            }
        }
    }
}
