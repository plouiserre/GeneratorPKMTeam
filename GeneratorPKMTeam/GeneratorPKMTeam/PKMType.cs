using System;
namespace GeneratorPKMTeam
{
	public class PKMType
	{
		public string Nom { get; set; }
		public List<RelPKMType> RelPKMTypes { get; set; }
		public PKMType()
		{
		}
	}
}

