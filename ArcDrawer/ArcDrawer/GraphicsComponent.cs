using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcDrawer
{
  class GraphicsComponent : System.Windows.Forms.PictureBox
  {
    public GraphicsComponent(System.Windows.Forms.Control a_external_controller)
    {
      this.BackColor = System.Drawing.Color.Azure;
      this.Size = a_external_controller.Size;
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MyMouseDown);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MyMouseUp);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MyMouseMove);
      m_controller = a_external_controller;

      m_left_button_pressed = false;

      m_catcher = new PointCatcher(25);

      InitGraphics();
    }

    public void InitGraphics()
    {
      m_graphics_holder = new GraphicsHolder(this);
    }
    
    private void MyMouseMove(Object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (m_left_button_pressed)
      {
        System.Drawing.Point pnt = new System.Drawing.Point(e.X, e.Y);
        if (m_catcher.CatchSecond(ref pnt))
        {
          m_graphics_holder.DrawPoint(e.X, e.Y);
          m_catcher.AssignPoint(ref pnt, ref m_graphics_holder);
        }
      }
    }

    private void MyMouseDown(Object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (e.Button == System.Windows.Forms.MouseButtons.Left)
      {
        m_left_button_pressed = true;

        m_catcher.SetStartPoint = new System.Drawing.Point(e.X, e.Y);
        
        m_graphics_holder.DrawPoint(e.X, e.Y);
      }
    }

    private void MyMouseUp(Object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (e.Button == System.Windows.Forms.MouseButtons.Left)
      {
        m_left_button_pressed = false;

        System.Drawing.Point pnt = new System.Drawing.Point(e.X, e.Y);
        m_graphics_holder.DrawPoint(e.X, e.Y);
        m_catcher.AssignPoint(ref pnt, ref m_graphics_holder);
      }
    }

    
    /// <summary>
    /// Members
    /// </summary>
    private System.Windows.Forms.Control m_controller;
    private GraphicsHolder m_graphics_holder;
    private PointCatcher m_catcher;

    private bool m_left_button_pressed;
    
  }
}
