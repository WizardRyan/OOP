using BicycleRaceSoftware.Shared;

namespace BicycleRaceSoftware.Client.Shared
{
	public class Screen
	{
		public string Label { get; set; }
		public List<Racer> Subjects { get; set; } = new List<Racer>();
	}

	public class BigScreen : Screen
	{

	}

	public class CheaterScreen : Screen
	{
	}
}
