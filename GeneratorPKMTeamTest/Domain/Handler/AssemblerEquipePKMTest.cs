using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class AssemblerEquipePKMTest
    {
        [Fact]
        public void AssemblerEquipeOK()
        {
            var pkmsGuessed = new List<PKM>() { new PKM() { Nom = "Carapuce" },
                new PKM() { Nom = "Bulbizarre" } };

            var tirages = new List<TiragePKMTypes>() {
                    new TiragePKMTypes() { NoteTirage = 67, ResultatTirageStatus = ResultatTirageStatus.Acceptable, PKMTypes = new List<PKMType>() },
                    new TiragePKMTypes() { NoteTirage = 88, ResultatTirageStatus = ResultatTirageStatus.Excellent, PKMTypes = new List<PKMType>() }
                };

            var definirOrdre = Substitute.For<IDefinirOrdrePKMType>();
            definirOrdre.Generer(Arg.Any<List<PKMType>>());
            var recuperationPKMs = Substitute.For<IRecuperationPKMs>();
            recuperationPKMs.Recuperer(Arg.Any<Dictionary<int, List<PKMType>>>()).Returns(pkmsGuessed);

            var assemblage = new AssemblerEquipePKM(definirOrdre, recuperationPKMs);

            var pkmsFinales = assemblage.Assembler(tirages);

            Assert.Equal(2, pkmsFinales.Count);
            Assert.Equal("Carapuce", pkmsFinales[0][0].Nom);
            Assert.Equal("Bulbizarre", pkmsFinales[0][1].Nom);
            Assert.Equal("Carapuce", pkmsFinales[1][0].Nom);
            Assert.Equal("Bulbizarre", pkmsFinales[1][1].Nom);
        }
    }
}