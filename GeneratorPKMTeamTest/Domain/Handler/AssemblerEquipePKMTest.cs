using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Handler.OrdrePKMType;
using GeneratorPKMTeam.Domain.Handler.SelectionPKM;
using GeneratorPKMTeam.Domain.Models;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class AssemblerEquipePKMTest
    {
        [Fact]
        public void AssemblerEquipeOK()
        {
            var pkmsGuessed = new List<PKM>() { new PKM() { Nom = "Carapuce" },
                new PKM() { Nom = "Bulbizarre" } };

            var tirage = new TiragePKMTypes() { NoteTirage = 88, ResultatTirageStatus = ResultatTirageStatus.Excellent, PKMTypes = new List<PKMType>() };

            var definirOrdre = Substitute.For<IDefinirOrdrePKMType>();
            definirOrdre.Generer(Arg.Any<List<PKMType>>());
            var recuperationPKMs = Substitute.For<IRecuperationPKMs>();
            recuperationPKMs.Recuperer(Arg.Any<Dictionary<int, List<PKMType>>>()).Returns(pkmsGuessed);

            var assemblage = new AssemblerEquipePKM(definirOrdre, recuperationPKMs);

            var pkmsFinales = assemblage.Assembler(tirage);

            Assert.Equal("Carapuce", pkmsFinales[0].Nom);
            Assert.Equal("Bulbizarre", pkmsFinales[1].Nom);
        }

        [Fact]
        public void AssemblerEquipeEchoue()
        {
            var tirage = new TiragePKMTypes() { NoteTirage = 88, ResultatTirageStatus = ResultatTirageStatus.Excellent, PKMTypes = new List<PKMType>() };

            var definirOrdre = Substitute.For<IDefinirOrdrePKMType>();
            definirOrdre.Generer(Arg.Any<List<PKMType>>());
            var recuperationPKMs = Substitute.For<IRecuperationPKMs>();
            recuperationPKMs.Recuperer(Arg.Any<Dictionary<int, List<PKMType>>>()).Throws(new PKMAvecTypeInexistantException("exception!!!!"));

            var assemblage = new AssemblerEquipePKM(definirOrdre, recuperationPKMs);

            var pkmsFinales = assemblage.Assembler(tirage);

            Assert.Null(pkmsFinales);
        }
    }
}