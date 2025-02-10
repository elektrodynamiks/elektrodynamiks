using CartesianMapClass;

namespace NavigationClass
{
    public class Trek
    {
        public char direction = '^';
        public int abscissa;
        public int ordinate;
        public int step = 1;
        public char obstacle = ' ';
        public bool motion = false;
    }

    public class Navigation
    {
        public int[] startPoint;
        public char track = 'X';

        public Navigation(string fileName)
        {
            Console.WriteLine("Navigation Class initialized!");
            // Trek startPosition = new Trek();
            // var navPlan = cartesianMap.Plan;
            // var myRoute = cartesianMap.CreateRoute();
            // startPosition = FindStartingPoint(navPlan);
            // myRoute[startPosition.abscissa][startPosition.ordinate] = track;
            // cartesianMap.PrintMapPlan(myRoute);
            // cartesianMap.TrekRouteLength(myRoute);
        }

        public Trek FindStartingPoint(char[][] navPlan, char startChar = '^')
        {
            // outer for loop
            Trek startPoint = new Trek();
            startPoint.direction = startChar;
            // int[] startPoint = [0, 0];
            for (int i = 0; i < navPlan.Length; i++)
            {
                // inner for loop
                for (int j = 0; j < navPlan[i].Length; j++)
                {
                    if (navPlan[i][j] == startChar)
                    {
                        startPoint.abscissa = j;
                        startPoint.ordinate = i;
                        Console.WriteLine(
                            "Starting Point dir:{0}, Postion X,Y: {1},{2}",
                            startPoint.direction,
                            startPoint.ordinate,
                            startPoint.abscissa
                        );
                        return startPoint;
                    }
                }
            }

            return startPoint;
        }

        public void Patrolling(char[][] navPlan, Trek position)
        {
            // find next location
            // check for obstacle
            // return the next location.
            // iterate until leaving the plan
        }

        public int[] nextLocation(char[][] navPlan, char direction)
        {
            switch (direction)
            {
                case '>':
                    //
                    return [1, 0];
                case 'v':
                    //
                    return [1, 0];
                case '<':
                    //
                    return [0, -1];
                case '^':
                    //
                    return [-1, 0];
                default:
                    return [0, 0];
            }
        }
    }
}

/* terms
location
track
waypoint
goal
obstacle
initialize
*/
