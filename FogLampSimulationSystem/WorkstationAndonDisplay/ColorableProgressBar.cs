

using System.Drawing;
using System.Windows.Forms;
// I was struggling with making a bar graph achieve the exact same thing that this does
// I could not for the life of me get it to align properly.
// Microsoft should really support allowing us to programmatically change progress bar colors!
namespace RunnerStationStatusViewer
{

    /*
     * CITATION: Colorable Progress Bar
     * AVAILABLE: https://stackoverflow.com/questions/778678/how-to-change-the-color-of-progressbar-in-c-sharp-net-3-5
     * AUTHOR: 'Crispy'
     */

    /// <summary>
    /// CLASS: ColorableProgressBar
    /// DESCRIPTION: A progress bar that allows you to set the colour of the fill.
    /// </summary>
    internal class ColorableProgressBar : ProgressBar
    {
        /// <summary>
        /// Defualt constructor for the custom progress bar class
        /// </summary>
        public ColorableProgressBar()
        {
            // Set the style to allow us to paint on the control, the double buffer to fix flickering
            // and Opaque, because we don't want transparency
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | 
                            ControlStyles.UserPaint |
                            ControlStyles.Opaque, true);
        }


        /// <summary>
        /// OnPaint is called every time the ProgressBar is drawn on the screen.
        /// This code basically runs on every new part count change
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Get the size of the progress bar
            Rectangle rect = e.ClipRectangle;
            // Set the width to be equal to the width of the progress bar * Value / Maximum (how full the bar is)
            rect.Width = (int)(rect.Width * ((double)Value / Maximum));
            // This value is checked to ensure that we set the correct settings in the 'SetStyle' in the constructor
            // If we do not add the ControlStyles.UserPaint, we won't be able to draw to the progress bar.
            if (ProgressBarRenderer.IsSupported)
            {
                // Draw the ProgressBar to the screen.
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics,e.ClipRectangle);
            }

            rect.Height = rect.Height - 4; // Subtract 4 from the height so we don't paint over the border
            Brush brush = new SolidBrush(ForeColor); // Create a brush with our selected colour
            e.Graphics.FillRectangle(brush, 2, 2 ,rect.Width, rect.Height); // Paint the rectangle using our colour
        }
    }
}
