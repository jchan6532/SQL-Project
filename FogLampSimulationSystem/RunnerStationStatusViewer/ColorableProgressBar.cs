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
    internal class ColorableProgressBar : ProgressBar
    {
        public ColorableProgressBar()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | 
                            ControlStyles.UserPaint |
                            ControlStyles.UserPaint |
                            ControlStyles.Opaque, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = e.ClipRectangle;
            rect.Width = (int)(rect.Width * ((double)Value / Maximum)) - 4;

            if (ProgressBarRenderer.IsSupported)
            {
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics,e.ClipRectangle);
            }

            rect.Height = rect.Height - 4;
            Brush brush = new SolidBrush(ForeColor);
            e.Graphics.FillRectangle(brush, 2, 2 ,rect.Width, rect.Height);
        }
    }
}
