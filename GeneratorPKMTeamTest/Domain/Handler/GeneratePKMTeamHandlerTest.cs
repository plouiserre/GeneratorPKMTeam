using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeamTest.Utils.Personas;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class GeneratePKMTeamHandlerTest
    {
        private PKMDatas _fakeDatas;

        public GeneratePKMTeamHandlerTest()
        {
            _fakeDatas = PKMDatasPersonas.GetPersonas();
        }

        [Fact]
        public void GeneratePKMTeamIsOK()
        {
            var PMKPersistence = Substitute.For<IPKMTypePersistence>();
            PMKPersistence.GetPKMDatas().Returns(_fakeDatas);

            var loadPKMTypes = new LoadPKMTypes(PMKPersistence);
            var selectPKMTypes = new SelectPKMTypes();
            var fightPKMTypes = new FightPKMTypes();
            var resultFightPKMTypes = new ResultFightPKMTypes();
            var handler = new GeneratePKMTeamHandler(loadPKMTypes, selectPKMTypes, fightPKMTypes, resultFightPKMTypes);

            handler.Generate();
        }

        //TODO voir plus tard si on fait un test pour générer une exception
    }
}