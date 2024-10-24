using System;
namespace GeneratorPKMTeam
{
	public class PKMDonnees : ICloneable
	{
		public List<PKMType> PKMTypes { get; set; }

		public PKMDonnees()
		{
		}

		public object Clone()
		{
			var PKMDatasCopie = new PKMDonnees();
			PKMDatasCopie.PKMTypes = new List<PKMType>();
			foreach (var PKMType in PKMTypes)
			{
				PKMDatasCopie.PKMTypes.Add(PKMType);
			}
			return PKMDatasCopie;
		}
	}
}

