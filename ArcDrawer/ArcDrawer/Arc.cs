using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ArcDrawer
{
  class Arc
  {
    public Arc(Point[] a_points)
    {
      m_b_defined = CreateArc(ref a_points);
    }

    private bool CreateArc(ref Point[] a_points)
    {
      try
      {
        Line triangle_edge1;
        Line triangle_edge2;

        if (DefineTriangleEdges(
          ref a_points[0], ref a_points[1], ref a_points[2],
          out triangle_edge1, out triangle_edge2))
        {
          Point middle_of_edge1 = new Point(
            Convert.ToInt32((a_points[0].X + a_points[1].X) * 0.5),
            Convert.ToInt32((a_points[0].Y + a_points[1].Y) * 0.5)
          );
          Point middle_of_edge2 = new Point(
            Convert.ToInt32((a_points[2].X + a_points[1].X) * 0.5),
            Convert.ToInt32((a_points[2].Y + a_points[1].Y) * 0.5)
          );

          if (CalculateCenter(ref triangle_edge1, ref middle_of_edge1,
            ref triangle_edge2, ref middle_of_edge2))
          {
            if (CalculateRadius(a_points[0]))
            {
              DefineAngles(
                ref a_points[0],
                ref a_points[1],
                ref a_points[2]
              );
              return true;
            }
          }
        }
        return false;
      }
      catch (System.IndexOutOfRangeException exception)
      {
        System.Windows.Forms.MessageBox.Show(exception.Message);
        return false;
      }

    }

    private bool CalculateCenter(
      ref Line a_line1, ref Point a_middle1,
      ref Line a_line2, ref Point a_middle2
    )
    {
      Line orth1 = new Line(a_line1.GetOrthogonalInPoint(ref a_middle1));
      Line orth2 = new Line(a_line2.GetOrthogonalInPoint(ref a_middle2));

      return orth1.FindIntersection(ref orth2, out m_center);
    }

    private bool CalculateRadius(Point a_arc_point)
    {
      m_d_radius = (m_center.X - a_arc_point.X) * (m_center.X - a_arc_point.X)
        + (m_center.Y - a_arc_point.Y) * (m_center.Y - a_arc_point.Y);

      m_d_radius = Math.Sqrt(m_d_radius);

      return Math.Abs(m_d_radius) < Double.Epsilon ? false : true;
    }

    private bool DefineTriangleEdges(
      ref Point a_point1,
      ref Point a_point2,
      ref Point a_point3,
      out Line a_edge1,
      out Line a_edge2
    )
    {
      a_edge1 = new Line(a_point1, a_point2);
      a_edge2 = new Line(a_point3, a_point2);

      return !a_edge1.IsCollinear(ref a_edge2);
    }

    public void DefineAngles(
      ref Point a_start,
      ref Point a_middle,
      ref Point a_end
    )
    {
      Vector2D vc_start = new Vector2D(
        a_start.X - m_center.X, a_start.Y - m_center.Y
      );
      Vector2D vc_middle = new Vector2D(
        a_middle.X - m_center.X, a_middle.Y - m_center.Y
      );
      Vector2D vc_end = new Vector2D(
        a_end.X - m_center.X, a_end.Y - m_center.Y
      );
      double d_middle_start = vc_middle.angle(vc_start);
      double d_end_middle = vc_end.angle(vc_middle);
      double d_end_start = vc_end.angle(vc_start);

      bool b_clockwise = d_middle_start < 0 && d_end_middle < 0;

      if (b_clockwise)
      {
        m_d_start_angle = GetAngleByPoint(ref a_start);
        m_d_sweep_angle = GetAngleByPoint(ref a_end) - m_d_start_angle;
      }
      else
      {
        m_d_start_angle = GetAngleByPoint(ref a_end);
        m_d_sweep_angle = GetAngleByPoint(ref a_start) - m_d_start_angle;
      }

      m_d_sweep_angle += m_d_sweep_angle < 0 ?
        2 * Math.PI : 0;
      
    }

    private double GetAngleByPoint(ref Point a_point)
    {
      return Math.Atan2(
        a_point.Y - m_center.Y,
        a_point.X - m_center.X
      );
    }

    /// <summary>
    /// Access to members
    /// </summary>
    public Point Center
    {
      get
      {
        return m_center;
      }
    }
    public double Radius
    {
      get
      {
        return m_d_radius;
      }
    }
    public double StartAngle
    {
      get
      {
        return m_d_start_angle;
      }
    }
    public double EndAngle
    {
      get
      {
        return m_d_sweep_angle;
      }
    }
    public bool IsDefined
    {
      get
      {
        return m_b_defined;
      }
    }
    ///
    /// members
    /// 
    private bool m_b_defined;
    private Point m_center;
    private double m_d_radius;
    private double m_d_start_angle;
    private double m_d_sweep_angle;
  }
}
