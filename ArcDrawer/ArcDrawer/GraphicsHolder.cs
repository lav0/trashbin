using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ArcDrawer
{
  
  class GraphicsHolder
  {
    private System.Drawing.Graphics m_graphics;
    private System.Drawing.Pen m_pen;

    static double RAD_TO_DEG = 57.2957795130823208768;

    public GraphicsHolder(GraphicsComponent a_component)
    {
      m_graphics = a_component.CreateGraphics();
      m_pen = new System.Drawing.Pen(System.Drawing.Color.Black, 3);
    }

    public void DrawThreePointConnection(ref Point[] a_points)
    {
      try
      {
        Arc arc = new Arc(a_points);
        if (arc.IsDefined)
        {
          DrawArc(ref arc);
        }
        else
        {
          DrawTwoLines(ref a_points);
        }
      }
      catch (System.IndexOutOfRangeException exception)
      {
        System.Windows.Forms.MessageBox.Show(exception.Message);
      }
    }

    private void DrawArc(ref Arc a_arc)
    {
      if (!a_arc.IsDefined)
        return;

      int parsed_start_angle = Convert.ToInt32(a_arc.StartAngle * RAD_TO_DEG);
      int parsed_end_angle = Convert.ToInt32(a_arc.EndAngle * RAD_TO_DEG);

      m_graphics.DrawArc(
        m_pen,
        a_arc.Center.X - Convert.ToInt32(a_arc.Radius),
        a_arc.Center.Y - Convert.ToInt32(a_arc.Radius),
        Convert.ToInt32(2*a_arc.Radius),
        Convert.ToInt32(2*a_arc.Radius),
        parsed_start_angle,
        parsed_end_angle
      );
    }

    public void DrawPoint(int a_x, int a_y)
    {
      m_graphics.DrawEllipse(
        m_pen,
        a_x, a_y,
        1, 1
      );
    }

    private void DrawTwoLines(ref Point[] a_points)
    {
      try
      {
        m_graphics.DrawLine(
          m_pen,
          a_points[0],
          a_points[1]
        );

        m_graphics.DrawLine(
          m_pen,
          a_points[1],
          a_points[2]
        );
      }
      catch (System.IndexOutOfRangeException exception)
      {
        System.Windows.Forms.MessageBox.Show(exception.Message);
      }
    }

    private void DrawRightDown(int a_x, int a_y)
    {
      m_graphics.DrawLine(
        m_pen, a_x, a_y,
        a_x + 10, a_y + 10
      );
    }

    private void DrawRightUp(int a_x, int a_y)
    {
      m_graphics.DrawLine(
        m_pen, a_x, a_y,
        a_x + 10, a_y - 10
      );
    }
  }
}
