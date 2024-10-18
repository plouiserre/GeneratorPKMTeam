using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Infrastructure.Connector.JsonModels;

namespace GeneratorPKMTeam.Infrastructure.Mapper
{
    public class PKMDatasMapper
    {
        public static PKMDatas ToDomain(PKMDatasInf json)
        {
            return new PKMDatas() { PKMTypes = json.PKMTypes.Select(o => ToDomain(o)).ToList() };
        }

        private static PKMType ToDomain(PKMTypeInf type)
        {
            return new PKMType()
            {
                Nom = type.Nom,
                RelPKMTypes = type.RelPKMTypes
                    .Select(o => ToDomain(o)).ToList()
            };
        }

        private static RelPKMType ToDomain(RelPKMTypeInf rel)
        {
            return new RelPKMType() { ModeImpact = rel.ModeImpact, TypePKM = rel.TypePKM };
        }
    }
}