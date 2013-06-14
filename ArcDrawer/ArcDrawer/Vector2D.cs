using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcDrawer
{
  class Vector2D
  {
    public Vector2D(double a_x, double a_y)
    {
      m_x = a_x;
      m_y = a_y;
    }

    public double scal_prod(Vector2D vc)
    {
      return this.m_x * vc.m_x + this.m_y * vc.m_y;
    }

    public double vector_prod_z_ort(Vector2D vc)
    {
      return this.m_x * vc.m_y - this.m_y * vc.m_x;
    }

    public double norm()
    {
      return scal_prod(this);
    }

    public double angle(Vector2D vc)
    {
      return Math.Atan2(vector_prod_z_ort(vc), scal_prod(vc));
    }

    public double X
    {
      get
      {
        return m_x;
      }
      set
      {
        m_x = value;
      }
    }
    public double Y
    {
      get
      {
        return m_y;
      }
      set
      {
        m_y = value;
      }
    }
    private double m_x, m_y;
  }
}
