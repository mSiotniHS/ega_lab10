using System.Collections.Generic;
using System.Linq;
using Common;
using EA.BaseProblem;

namespace Travelling_Salesman_Problem.Solvers;

public sealed class RandomizedClosestNeighbourMethod : IProblemSolver<Route, Tsp>
{
	private readonly IRng _rng;

	public RandomizedClosestNeighbourMethod(IRng rng)
	{
		_rng = rng;
	}

	public Route FindSolution(Tsp problem)
	{
		var distances = problem.Distances;
		var firstCity = _rng.GetInt(distances.CityCount);

		var route = new Route {firstCity};

		var unvisitedCities = Enumerable.Range(0, distances.CityCount).ToList();
		unvisitedCities.Remove(firstCity);

		while (unvisitedCities.Count > 0)
		{
			var lastCity = route.Last();
			var nextCity = NextCity(distances, lastCity, unvisitedCities);

			route.Add(nextCity);

			unvisitedCities.Remove(nextCity);
		}

		return route;
	}

	private int NextCity(DistanceMatrix distances, int currentCity, IList<int> unvisitedCities)
	{
		var weights = new double[unvisitedCities.Count];
		for (var i = 0; i < unvisitedCities.Count; i++)
		{
			var unvisitedCity = unvisitedCities[i];
			weights[i] = (1 / (double) distances[currentCity, unvisitedCity]);
		}

		return Roulette.Spin(_rng, unvisitedCities, weights);
	}
}
