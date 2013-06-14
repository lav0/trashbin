using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArcDrawer
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();

      this.SizeChanged += new EventHandler(FixSize);
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      this.MinimumSize = this.Size;
    }

    public void FixSize(Object sender, EventArgs e)
    {
      this.MinimumSize = this.Size;
      pictureBox.Size = this.ClientSize;
      pictureBox.InitGraphics();
    }
  }
}
