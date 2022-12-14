using System.Collections.Generic;
using EA.BaseProblem;
using EA.Core;
using EA.Upper;

namespace EA.CommonModules;

public sealed class ProblemSolverPopulationGenerator<TBaseType, TBaseProblem> : IPopulationGenerator
	where TBaseProblem : IGaProblem<TBaseType>
{
	private readonly TBaseProblem _baseProblem;
	private readonly IProblemSolver<TBaseType, TBaseProblem> _problemSolver;

	public ProblemSolverPopulationGenerator(TBaseProblem baseProblem, IProblemSolver<TBaseType, TBaseProblem> problemSolver)
	{
		_baseProblem = baseProblem;
		_problemSolver = problemSolver;
	}

	public IEnumerable<Genotype> Generate(int count)
	{
		for (var i = 0; i < count; i++)
		{
			yield return _baseProblem.Coder.Encode(_problemSolver.FindSolution(_baseProblem));
		}
	}
}
