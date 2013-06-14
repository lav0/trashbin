using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ArcDrawer
{
  /// <summary>
  /// Two-dementioned line
  /// </summary>
  class Line
  {
    public Line(Line a_copy)
    {
      m_a = a_copy.m_a;
      m_b = a_copy.m_b;
      m_c = a_copy.m_c;
    }
    public Line(double a_coef1, double a_coef2, double a_coef3)
    {
      m_a = a_coef1;
      m_b = a_coef2;
      m_c = a_coef3;
    }

    public Line(Point a_one_point, Point a_other_point)
    {
      BuildLineWithTwoPoints(ref a_one_point, ref a_other_point);
    }

    public Line(Point a_on_line, double a_vc_along_x, double a_vc_along_y)
    {
      Point a_second = new Point(
        a_on_line.X + Convert.ToInt32(a_vc_along_x),
        a_on_line.Y + Convert.ToInt32(a_vc_along_y)
      );
      BuildLineWithTwoPoints(ref a_on_line, ref a_second);
    }

    public Line GetOrthogonalInPoint(ref Point a_point)
    {
      return new Line(a_point, this.m_a, this.m_b);
    }

    /// <summary>
    /// Use Krammer's rule
    /// </summary>
    /// <param name="a_line"></param>
    /// <param name="a_intersiction"></param>
    /// <returns></returns>
    public bool FindIntersection(ref Line a_line, out Point a_intersection)
    {
      double d_delta = this.m_a * a_line.m_b - this.m_b * a_line.m_a;
      if (Math.Abs(d_delta) < Double.Epsilon)
      {
        a_intersection = new Point();
        return false;
      }
      double d_delta_x = this.m_b * a_line.m_c - this.m_c * a_line.m_b;
      double d_delta_y = this.m_c * a_line.m_a - this.m_a * a_line.m_c;

      a_intersection = new Point(
        Convert.ToInt32(d_delta_x / d_delta), 
        Convert.ToInt32(d_delta_y / d_delta)
      );

      return true;
    }

    private bool BuildLineWithTwoPoints(ref Point a_one, ref Point a_two)
    {
      if (Math.Abs(a_one.X - a_two.X) < Double.Epsilon &&
        Math.Abs(a_one.Y - a_two.Y) < Double.Epsilon)
      {
        return false;
      }
      ///
      /// (B-A)-vector = {d_l, d_m}
      /// 

      double d_l = a_two.X - a_one.X;
      double d_m = a_two.Y - a_one.Y;

      m_a = d_m;
      m_b = -d_l;
      m_c = d_l * a_one.Y - d_m * a_one.X;

      return true;
    }

    /// <summary>
    /// Returns true if any Y
    /// </summary>
    /// <param name="a_d_x"></param>
    /// <param name="a_d_y"></param>
    /// <returns></returns>
    public bool GetYbyX(double a_d_x, out double a_d_y)
    {
      try
      {
        a_d_y = (m_a * a_d_x + m_c) / m_b;
        return false;
      }
      catch (System.DivideByZeroException exception)
      {
        System.Windows.Forms.MessageBox.Show(exception.Message);
        a_d_y = 0.0;
        return true;
      }
    }

    public bool IsCollinear(ref Line a_line)
    {
      if (Math.Abs(a_line.m_a) < Double.Epsilon)
      {
        if (Math.Abs(a_line.m_b) < Double.Epsilon)
        {
          return false;
        }
        if (Math.Abs(this.m_a) < Double.Epsilon)
        {
          return true;
        }
        return false;
      }

      return Math.Abs(this.m_a / a_line.m_a - this.m_b / a_line.m_b) < 
        Double.Epsilon;
    }
    ///
    /// Access to members
    /// 
    public double getA
    {
      get
      {
        return m_a;
      }
    }
    public double getB
    {
      get
      {
        return m_b;
      }
    }
    public double getC
    {
      get
      {
        return m_c;
      }
    }
    ///
    /// members  A*x + B*y + C = 0
    ///
    private double m_a;
    private double m_b;
    private double m_c;

  }
}
