﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("XMLState")]
public class XMLState : IState
{
	// prevent calling Save() from all the property setters
	[XmlIgnore]
	private static bool preventSave;

	#region CONFIG
	[XmlElement("VersionMajor")]
	private int _versionMajor;
	public int VersionMajor
	{
		get { return _versionMajor; }
		set { _versionMajor = value; }
	}
	[XmlElement("VersionMiddle")]
	private int _versionMiddle;
	public int VersionMiddle
	{
		get { return _versionMiddle; }
		set { _versionMiddle = value; }
	}
	[XmlElement("VersionMinor")]
	private int _versionMinor;
	public int VersionMinor
	{
		get { return _versionMinor; }
		set { _versionMinor = value; }
	}
	#endregion

	#region RULES
	[XmlElement("TotalDays")]
	private int _totalDays;
	public int TotalDays
	{
		get { return _totalDays; }
		set { _totalDays = value; Save(); }
	}

	[XmlElement("MaxAP")]
	private int _maxAP;
	public int MaxAP
	{
		get { return _maxAP; }
		set { _maxAP = value; Save(); }
	}

	[XmlElement("EnterSectorAPCost")]
	private int _enterSectorAPCost;
	public int EnterSectorAPCost
	{
		get { return _enterSectorAPCost; }
		set { _enterSectorAPCost = value; Save(); }
	}

	[XmlElement("LootCharges")]
	private int _lootCharges;
	public int LootCharges
	{
		get { return _lootCharges; }
		set { _lootCharges = value; Save(); }
	}

	[XmlElement("SectorsParameters")]
	private List<SectorParameters> _sectorsParameters;
	public List<SectorParameters> SectorsParameters
	{
		get { return _sectorsParameters; }
		set { _sectorsParameters = value; Save(); }
	}

	[XmlElement("Artifacts")]
	private List<Artifact> _artifacts;
	public List<Artifact> Artifacts
	{
		get { return _artifacts; }
		set { _artifacts = value; Save(); }
	}
	[XmlElement("AnalyzeArtifactAPCost")]
	private int _analyzeArtifactAPCost;
	public int AnalyzeArtifactAPCost
	{
		get { return _analyzeArtifactAPCost; }
		set { _analyzeArtifactAPCost = value; Save(); }
	}

	[XmlElement("FixEngineAPCost")]
	private int _fixEngineAPCost;
	public int FixEngineAPCost
	{
		get { return _fixEngineAPCost; }
		set { _fixEngineAPCost = value; Save(); }
	}
	[XmlElement("FixEngineRequirements")]
	private SerializableDictionary<BreakageType, int[]> _fixEngineRequirements;
	public SerializableDictionary<BreakageType, int[]> FixEngineRequirements
	{
		get { return _fixEngineRequirements; }
		set { _fixEngineRequirements = value as SerializableDictionary<BreakageType, int[]>; Save(); }
	}

	[XmlElement("FuelSynthAPCost")]
	private int _synthFuelAPCost;
	public int FuelSynthAPCost
	{
		get { return _synthFuelAPCost; }
		set { _synthFuelAPCost = value; Save(); }
	}
	[XmlElement("FuelSynthGrace")]
	private int _synthFuelGrace;
	public int FuelSynthGrace
	{
		get { return _synthFuelGrace; }
		set { _synthFuelGrace = value; Save(); }
	}
	#endregion

	#region STATE
	[XmlElement("GameProgress")]
	private int _gameProgress;
	public GameStatus GameStatus
	{
		get { return (GameStatus)_gameProgress; }
		set { _gameProgress = (int)value; Save(); }
	}

	[XmlElement("BreakageType")]
	private int _breakageType;
	public BreakageType BreakageType
	{
		get { return (BreakageType)_breakageType; }
		set { _breakageType = (int)value; Save(); }
	}
	[XmlElement("EngineFixed")]
	private bool _engineFixed;
	public bool EngineFixed
	{
		get { return _engineFixed; }
		set { _engineFixed = value; Save(); }
	}

	[XmlElement("FuelSynthFormula")]
	private int[] _synthFuelFormula;
	public int[] FuelSynthFormula
	{
		get { return _synthFuelFormula; }
		set { _synthFuelFormula = value; Save(); }
	}
	[XmlElement("FuelSynthProbes")]
	private List<int[]> _fuelSynthProbes;
	public List<int[]> FuelSynthProbes
	{
		get { return _fuelSynthProbes; }
		set { _fuelSynthProbes = value; Save(); }
	}
	[XmlElement("FuelSynthed")]
	private bool _fuelSynthed;
	public bool FuelSynthed
	{
		get { return _fuelSynthed; }
		set { _fuelSynthed = value; Save(); }
	}

	[XmlElement("CurrentDay")]
	private int _currentDay;
	public int CurrentDay
	{
		get { return _currentDay; }
		set { _currentDay = value; Save(); }
	}

	[XmlElement("CurrentAP")]
	private int _currentAP;
	public int CurrentAP
	{
		get { return _currentAP; }
		set { _currentAP = value; Save(); }
	}

	[XmlElement("MineralA")]
	private int _mineralA;
	public int MineralA
	{
		get { return _mineralA; }
		set { _mineralA = value; Save(); }
	}
	[XmlElement("MineralB")]
	private int _mineralB;
	public int MineralB
	{
		get { return _mineralB; }
		set { _mineralB = value; Save(); }
	}
	[XmlElement("MineralC")]
	private int _mineralC;
	public int MineralC
	{
		get { return _mineralC; }
		set { _mineralC = value; Save(); }
	}

	[XmlElement("Wiring")]
	private int _wiring;
	public int Wiring
	{
		get { return _wiring; }
		set { _wiring = value; Save(); }
	}
	[XmlElement("Alloy")]
	private int _alloy;
	public int Alloy
	{
		get { return _alloy; }
		set { _alloy = value; Save(); }
	}
	[XmlElement("Chips")]
	private int _chips;
	public int Chips
	{
		get { return _chips; }
		set { _chips = value; Save(); }
	}
	#endregion

	public static XMLState Load ()
	{
		//if (PlayerPrefs.HasKey("XMLState"))
		//{
		//	preventSave = true;
		//	XmlSerializer serializer = new XmlSerializer(typeof(XMLState));
		//	StringReader stringReader = new StringReader(PlayerPrefs.GetString("XMLState"));
		//	XmlTextReader xmlReader = new XmlTextReader(stringReader);
		//	XMLState state = (XMLState)serializer.Deserialize(xmlReader);
		//	xmlReader.Close();
		//	stringReader.Close();
		//	preventSave = false;
		//	return state;
		//}
		//else return new XMLState();

		if (File.Exists(Path.Combine(Environment.CurrentDirectory, "state.xml")))
		{
			preventSave = true;
			var serializer = new XmlSerializer(typeof(XMLState));
			using (var stream = new FileStream(Path.Combine(Environment.CurrentDirectory, "state.xml"), FileMode.Open))
			{
				var state = serializer.Deserialize(stream) as XMLState;
				preventSave = false;
				return state;
			}
		}
		else
		{
			var state = new XMLState();
			state.Reset(true);
			ServiceLocator.Logger.Log("The state.xml file cannot be found and was created.");
			return state;
		}
	}

	private void Save ()
	{
		if (preventSave) return;

		VersionMajor = GlobalConfig.VERSION_MAJOR;
		VersionMiddle = GlobalConfig.VERSION_MIDDLE;
		VersionMinor = GlobalConfig.VERSION_MINOR;

		//XmlSerializer serializer = new XmlSerializer(typeof(XMLState));
		//StringWriter stringWriter = new StringWriter();
		//XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);
		//serializer.Serialize(xmlWriter, this);
		//string xml = stringWriter.ToString();
		//xmlWriter.Close();
		//stringWriter.Close();
		//PlayerPrefs.SetString("XMLState", xml);
		//PlayerPrefs.Save();

		var serializer = new XmlSerializer(typeof(XMLState));
		using (var stream = new FileStream(Path.Combine(Environment.CurrentDirectory, "state.xml"), FileMode.Create)) 
			serializer.Serialize(new StreamWriter(stream, System.Text.Encoding.UTF8), this);
	}

	public void HoldAutoSave (bool hold)
	{
		preventSave = hold;
		if (!hold) Save();
	}

	public void Reset (bool resetRules = false)
	{
		bool autoSaveWasTurnedOff = preventSave;
		preventSave = true;

		#region RULES
		if (resetRules)
		{
			GameStatus = GameStatus.FirstLaunch;

			TotalDays = 8;
			MaxAP = 10;

			EnterSectorAPCost = 3;
			LootCharges = 3;
			SectorsParameters = new List<SectorParameters> { 
				new SectorParameters(1, 4, 0, 3, 3), 
				new SectorParameters(2, 5, 3, 0, 3), 
				new SectorParameters(3, 3, 3, 3, 0), 
				new SectorParameters(4, 6, 2, 2, 2) 
			};

			Artifacts = new List<Artifact> { 
				new Artifact("Artifact1", "Infotrace for Artifact1",   BreakageType.BRK1, 30, 20, 15), 
				new Artifact("Artifact2", "Infotrace for Artifact2",   null,              05, 00, 15), 
				new Artifact("Artifact3", "Infotrace for Artifact3",   null,              15, 10, 00), 
				new Artifact("Artifact4", "Infotrace for Artifact4",   null,              00, 30, 00), 
				new Artifact("Artifact5", "Infotrace for Artifact5",   null,              00, 00, 35), 
				new Artifact("Artifact6", "Infotrace for Artifact6",   BreakageType.BRK2, 40, 10, 15), 
				new Artifact("Artifact7", "Infotrace for Artifact7",   null,              20, 05, 15), 
				new Artifact("Artifact8", "Infotrace for Artifact8",   null,              15, 00, 15), 
				new Artifact("Artifact9", "Infotrace for Artifact9",   null,              05, 00, 30), 
				new Artifact("Artifact10", "Infotrace for Artifact10", BreakageType.BRK3, 00, 50, 05), 
				new Artifact("Artifact11", "Infotrace for Artifact11", null,              15, 00, 15), 
				new Artifact("Artifact12", "Infotrace for Artifact12", null,              20, 00, 00),
				new Artifact("Artifact13", "Infotrace for Artifact13", null,              10, 10, 15),
				new Artifact("Artifact14", "Infotrace for Artifact14", null,              05, 15, 10),
				new Artifact("Artifact15", "Infotrace for Artifact15", BreakageType.BRK4, 30, 10, 20),
			};
			AnalyzeArtifactAPCost = 2;

			FixEngineAPCost = 5;
			FixEngineRequirements = new SerializableDictionary<BreakageType, int[]>
			{
				{BreakageType.BRK1, new int[3] {30, 35, 40}},
				{BreakageType.BRK2, new int[3] {35, 40, 30}},
				{BreakageType.BRK3, new int[3] {40, 30, 35}},
				{BreakageType.BRK4, new int[3] {30, 40, 35}},
			};

			FuelSynthAPCost = 2;
			FuelSynthGrace = 1;
		}
		#endregion

		#region STATE
		BreakageType[] possibleBRK = (BreakageType[])Enum.GetValues(typeof(BreakageType));
		BreakageType = possibleBRK[Rand.RND.Next(0, 4)];
		EngineFixed = false;

		FuelSynthFormula = new int[3];
		while ((FuelSynthFormula[0] + FuelSynthFormula[1] + FuelSynthFormula[2]) != 9)
		{
			FuelSynthFormula[0] = Rand.RND.Next(1, 10);
			FuelSynthFormula[1] = Rand.RND.Next(1, 10);
			FuelSynthFormula[2] = Rand.RND.Next(1, 10);
		}
		FuelSynthProbes = new List<int[]>();
		FuelSynthed = false;

		CurrentDay = 1;
		CurrentAP = MaxAP;

		MineralA = 0;
		MineralB = 0;
		MineralC = 0;

		Wiring = 0;
		Alloy = 0;
		Chips = 0;

		foreach (var artifact in Artifacts) 
			artifact.Status = ArtifactStatus.NotFound;
		#endregion

		preventSave = autoSaveWasTurnedOff;
		this.Save();
	}
}