using System;
namespace GeneratorPKMTeam
{
	public class PKMDatas : ICloneable
	{
		public List<PKMType> PKMTypes { get; set; }

		public PKMDatas()
		{
		}

		public object Clone()
		{
			var PKMDatasCopie = new PKMDatas();
			PKMDatasCopie.PKMTypes = new List<PKMType>();
			foreach (var PKMType in PKMTypes)
			{
				PKMDatasCopie.PKMTypes.Add(PKMType);
			}
			return PKMDatasCopie;
		}
	}
}

