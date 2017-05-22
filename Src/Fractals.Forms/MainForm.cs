using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fractals.Forms
{
    public partial class MainForm : Form
    {
        const double rMin = -2.5, rMax = 1.0, iMin = -0.5, iMax = 0.5;

        Color[] pallette;

        public MainForm()
        {
            InitializeComponent();

            pallette = new Color[255];

            for (int i = 0; i < 255; i++)
            {
                pallette[i] = Color.FromArgb(255, i, i, 255);
            }
        }

        private void goToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(o => CalculateBrot()));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void CalculateBrot()
        {
            using (var g = canvas.CreateGraphics())
            {
                var rScale = (Math.Abs(rMin) + Math.Abs(rMax)) / canvas.Width;
                var iScale = (Math.Abs(iMin) + Math.Abs(iMax)) / canvas.Height;

                for (int x = 0; x < canvas.Width; x++)
                {
                    for (int y = 0; y < canvas.Height; y++)
                    {
                        Complex c = new Complex(x * rScale + rMin, y * iScale + iMin);
                        Complex z = c;

                        for (int i = 0; i < pallette.Length; i++)
                        {
                            if (z.Magnitude >= 2.0)
                            {
                                g.FillRectangle(new SolidBrush(pallette[i]), x, y, 1, 1);
                                break;
                            }
                            else
                            {
                                z = c + Complex.Pow(z, 2);
                            }
                        }
                    }
                }
            }
        }
    }
}
