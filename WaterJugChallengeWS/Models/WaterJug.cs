using System.ComponentModel.DataAnnotations;

namespace WaterJugChallengeWS.Models
{
    public class WaterJug
    {
        [Range(1, int.MaxValue, ErrorMessage = "Capacity of the jug X must be positive.")]
        public int XCapacity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Capacity of the jug Y must be positive.")]
        public int YCapacity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The amount wanted must be positive.")]
        public int ZAmountWanted { get; set; }
    }
}
