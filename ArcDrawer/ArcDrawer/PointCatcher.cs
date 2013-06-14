using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ArcDrawer
{
  class PointCatcher
  {
    public PointCatcher()
    {
      m_i_counter = 0;
    }
    /// <summary>
    /// Special contructor
    /// </summary>
    /// <param name="a_d_tol"></param>
    public PointCatcher(
      double a_d_tol
    )
    {
      m_d_tolerance = FilterTolerance(a_d_tol);
      m_i_counter = 0;
    }

    /// <summary>
    /// Returns true if next point lays farther then previous
    /// </summary>
    /// <param name="a_point"></param>
    /// <returns></returns>
    public bool CatchSecond(ref Point a_point)
    {
      try
      {
        return Distance(ref m_points[m_i_counter - 1], ref a_point) >= 
          m_d_tolerance;
      }
      catch (System.IndexOutOfRangeException exception)
      {
        System.Windows.Forms.MessageBox.Show(exception.Message);
        return false;
      }
    }

    /// <summary>
    /// Accept point
    /// </summary>
    /// <param name="a_point"></param>
    /// <param name="a_graphics"></param>
    public void AssignPoint(
      ref System.Drawing.Point a_point,
      ref GraphicsHolder a_graphics
    )
    {
      this[m_i_counter] = a_point;

      if (m_i_counter == 3)
      {
        a_graphics.DrawThreePointConnection(ref m_points);

        NextThree();
      }
    }

    /// <summary>
    /// Calculate distance between two points
    /// </summary>
    /// <param name="a_p1"></param>
    /// <param name="a_p2"></param>
    /// <returns></returns>
    private double Distance(
      ref Point a_p1,
      ref Point a_p2
    )
    {
      return Math.Sqrt(
        (a_p1.X - a_p2.X) * (a_p1.X - a_p2.X) + 
        (a_p1.Y - a_p2.Y) * (a_p1.Y - a_p2.Y)
      );
    }

    /// <summary>
    /// Check tolerance's validity
    /// </summary>
    /// <param name="a_d_income"></param>
    /// <returns></returns>
    private double FilterTolerance(double a_d_income)
    {
      return a_d_income > 1.0 ? a_d_income : 1.0;
    }


    private void NextThree()
    {
      m_i_counter = 1;

      m_points[0] = m_points[2];
    }

    /// <summary>
    /// Access to members
    /// </summary>
    public double Tolerance
    {
      get
      {
        return m_d_tolerance;
      }
      set
      {
        m_d_tolerance = value;
      }
    }

    public Point SetStartPoint
    {
      set
      {
        m_points[0] = value;
        m_i_counter = 1;
      }
    }

    public Point this[int a_index]
    {
      get
      {
        return m_points[a_index];
      }
      set
      {
        m_points[a_index] = value;
        m_i_counter++;
      }
    }

    /// <summary>
    /// Members
    /// </summary>
    private double m_d_tolerance;

    private Point[] m_points = new Point[3];
    private int m_i_counter;

  }
}
