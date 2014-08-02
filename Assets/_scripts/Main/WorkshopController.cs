﻿using System;

public class WorkshopController : BaseController
{
	public bool AnalyzeArtifact (Artifact artifact)
	{
		if (State.CurrentAP < State.AnalyzeArtifactAPCost) return false;

		State.CurrentAP -= State.AnalyzeArtifactAPCost;
		artifact.Status = ArtifactStatus.Analyzing;
		return true;
	}

	public bool FixEngine ()
	{
		if (State.CurrentAP < State.FixEngineAPCost || !CanFixEngine()) 
			return false;

		State.EngineFixed = true;

		State.CurrentAP -= State.FixEngineAPCost;
		return true;
	}

	public bool CanFixEngine ()
	{
		var requirements = State.FixEngineRequirements[State.BreakageType];
		return !State.EngineFixed && 
			State.Wiring >= requirements[0] && State.Alloy >= requirements[1] && State.Chips >= requirements[2] && 
			State.Artifacts.Find(x => x.Identity.GetValueOrDefault() == State.BreakageType).Status == ArtifactStatus.Analyzed;
	}

	public bool SynthFuel (int[] probe)
	{
		if (State.FuelSynthed || State.CurrentAP < State.FuelSynthAPCost || 
			State.MineralA < probe[0] || State.MineralB < probe[1] || State.MineralC < probe[2] ||
			probe[0] == 0 || probe[1] == 0 || probe[2] == 0 ||
			probe[0] + probe[1] + probe[2] != 9) return false;

		State.MineralA -= probe[0];
		State.MineralB -= probe[1];
		State.MineralC -= probe[2];

		State.FuelSynthProbes.Add(probe);

		if (MeasureProbe(probe) == ProbeType.True)
			State.FuelSynthed = true;

		State.CurrentAP -= State.FuelSynthAPCost;
		return true;
	}

	public ProbeType MeasureProbe (int[] probe)
	{
		int distance = Math.Abs(probe[0] - State.FuelSynthFormula[0]) +
			Math.Abs(probe[1] - State.FuelSynthFormula[1]) +
			Math.Abs(probe[2] - State.FuelSynthFormula[2]);

		return distance <= State.FuelSynthGrace * 3 ? ProbeType.True :
			distance <= 6 ? ProbeType.Close : ProbeType.Wrong;
	}
}