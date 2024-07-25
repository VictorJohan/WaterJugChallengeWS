using WaterJugChallengeWS.Models;

namespace WaterJugChallengeWS.Services
{
    public class WaterJugService
    {
        public WaterJugResponse Solve(WaterJug waterJug)
        {
            var response = new WaterJugResponse();

            // If the amount wanted is greater than the sum of the capacities of the jugs, it is impossible to solve
            if (waterJug.ZAmountWanted > waterJug.XCapacity + waterJug.YCapacity)
            {
                response.Solution.Add("No Solution.");
                return response;
            }

            
            var visited = new HashSet<(int, int)>();
            var queue = new Queue<(int x, int y, List<string> steps)>();
            queue.Enqueue((0, 0, new List<string>()));

            while (queue.Count > 0)
            {
                // Dequeue the current state of the jugs and the steps taken to reach this state
                var (x, y, solutionAux) = queue.Dequeue();

                if (x == waterJug.ZAmountWanted || y == waterJug.ZAmountWanted)
                {
                    response.Solution = solutionAux;
                    return response;
                }

                var solution = new List<(int, int, string)>
                {
                    (waterJug.XCapacity, y, $"Fill bucket X"),
                    (x, waterJug.YCapacity, $"Fill bucket Y"),
                    (0, y, $"Empty bucket X"),
                    (x, 0, $"Empty bucket Y"),
                    // Transfer X -> Y
                    (Math.Max(0, x - (waterJug.YCapacity - y)), Math.Min(waterJug.YCapacity, x + y), $"Transfer from bucket X to bucket Y"),
                    // Transfer Y -> X
                    (Math.Min(waterJug.XCapacity, x + y), Math.Max(0, y - (waterJug.XCapacity - x)), $"Transfer from bucket Y to bucket X")
                };

                foreach (var (newX, newY, action) in solution)
                {
                    if (!visited.Contains((newX, newY)))
                    {
                        visited.Add((newX, newY));
                        var newSteps = new List<string>(solutionAux) { action };
                        queue.Enqueue((newX, newY, newSteps));
                    }
                }
            }

            response.Solution.Add("No Solution");
            return response;
        }
    }
}
