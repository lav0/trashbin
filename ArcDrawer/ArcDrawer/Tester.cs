using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ArcDrawer
{
  class Tester
  {
    public static bool Test()
    {
      int[][] i_params = new int[2][];
      i_params[0] = new int[7] { -1, -1, 1, 3, 2, -1, 1 };
      i_params[1] = new int[7] { 0, 0, 3, 2, 2, -3, 0 };
      bool b_test_passed = true;
      Random random = new Random();
      for (int i = 0; i <= 1; i++)
      {
        int i_test_counter = 0;
        while (i_test_counter < 100)
        {
          i_test_counter++;
          double d_x_value = Convert.ToDouble(random.Next(-10000, 10000)) / 10.0;
          b_test_passed &= Tester.LineTest(
            i_params[i], d_x_value
          );
        }
      }

      b_test_passed &= LineIntersectionTest();
      b_test_passed &= ArcTest();
      b_test_passed &= ArcAnglesTest();
      if (!b_test_passed)
      {
        System.Windows.Forms.MessageBox.Show("Wow wow wow");
      }

      return true;
    }

    private static bool LineTest(
      int[] a_params,
      double a_x)
    {
      Point point1 = new Point(a_params[0], a_params[1]);
      Point point2 = new Point(a_params[2], a_params[3]);

      Line line_coefs = new Line(a_params[4], a_params[5], a_params[6]);
      Line line_points = new Line(
        point1,
        point2
      );

      double d_output_y1;
      double d_output_y2;
      bool b_any_y1 = line_coefs.GetYbyX(a_x, out d_output_y1);
      bool b_any_y2 = line_points.GetYbyX(a_x, out d_output_y2);

      return d_output_y1 == d_output_y2 || b_any_y1 || b_any_y1;
    }

    private static bool LineIntersectionTest()
    {
      Line line1 = new Line(2, -1, 1);
      Line line2 = new Line(new Point(-2, 4), new Point(-2, 6));

      Point point_intersection;

      bool b_found = line1.FindIntersection(ref line2, out point_intersection);

      return b_found && point_intersection.X == -2 && point_intersection.Y == -3;
    }

    private static bool ArcTest()
    {
      Point[] p = new Point[3];
      p[0] = new Point(0, 3);
      p[1] = new Point(3, 6);
      p[2] = new Point(3, 0);

      Arc arc1 = new Arc(p);

      p[0].X = Convert.ToInt32(50 * Math.Sqrt(2));
      p[0].Y = Convert.ToInt32(50 * Math.Sqrt(2));
      p[1].X = -Convert.ToInt32(50 * Math.Sqrt(2));
      p[1].Y = Convert.ToInt32(50 * Math.Sqrt(2));
      p[2].X = 0;
      p[2].Y = -100;

      Arc arc2 = new Arc(p);

      return arc1.IsDefined && arc1.Radius == 3.0 && arc1.Center == new Point(3,3) &&
        arc2.IsDefined && (arc2.Radius >= 99 && arc2.Radius <= 101);
    }

    private static bool ArcAnglesTest()
    {
      Point[] p = new Point[3];
      p[0] = new Point(0, 30);
      p[1] = new Point(30, 60);
      p[2] = new Point(30, 0);

      Arc arc1 = new Arc(p);

      return true;
    }

  }
}
