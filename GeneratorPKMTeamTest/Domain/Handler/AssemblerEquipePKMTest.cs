using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Handler.OrdrePKMType;
using GeneratorPKMTeam.Domain.Handler.SelectionPKM;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeamTest.Utils.Personas;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class AssemblerEquipePKMTest
    {
        private PKMDonnees _fakeDatas;
        private ChargerPKMTypes _chargerPKMTypes;
        private ChoisirPKMTypes _choisirPKMTypes;

        public AssemblerEquipePKMTest()
        {
            _fakeDatas = PKMDonneesPersonas.GetPersonas();
            InitChargerPKMTypes();
            InitChoisirPKMTypes();
        }

        [Fact]
        public void AssemblerEquipeOK()
        {
            var pkmsGuessed = new List<PKM>() { new PKM() { Nom = "Carapuce" },
                new PKM() { Nom = "Bulbizarre" } };

            var definirOrdre = Substitute.For<IDefinirOrdrePKMType>();
            definirOrdre.Generer(Arg.Any<List<PKMType>>());
            var recuperationPKMs = Substitute.For<IRecuperationPKMs>();
            recuperationPKMs.Recuperer(Arg.Any<Dictionary<int, List<PKMType>>>()).Returns(pkmsGuessed);

            var assemblage = new AssemblerEquipePKM(_chargerPKMTypes, _choisirPKMTypes, definirOrdre, recuperationPKMs);

            var pkmsFinales = assemblage.Assembler();

            Assert.Equal("Carapuce", pkmsFinales[0].Nom);
            Assert.Equal("Bulbizarre", pkmsFinales[1].Nom);
        }

        [Fact]
        public void AssemblerEquipeEchoue()
        {
            var definirOrdre = Substitute.For<IDefinirOrdrePKMType>();
            definirOrdre.Generer(Arg.Any<List<PKMType>>());
            var recuperationPKMs = Substitute.For<IRecuperationPKMs>();
            recuperationPKMs.Recuperer(Arg.Any<Dictionary<int, List<PKMType>>>()).Throws(new PKMAvecTypeInexistantException("exception!!!!"));

            var assemblage = new AssemblerEquipePKM(_chargerPKMTypes, _choisirPKMTypes, definirOrdre, recuperationPKMs);

            var pkmsFinales = assemblage.Assembler();

            Assert.Null(pkmsFinales);
        }

        public void InitChargerPKMTypes()
        {
            var PMKPersistence = Substitute.For<IPKMTypePersistence>();
            PMKPersistence.GetPKMDonnees().Returns(_fakeDatas);
            _chargerPKMTypes = new ChargerPKMTypes(PMKPersistence);
        }

        public void InitChoisirPKMTypes()
        {
            var starterPKM = Substitute.For<IGererStarterPKM>();
            starterPKM.RecupererStarter().Returns(new PKM() { Nom = "Carapuce", PKMTypes = new List<string>() { "Eau" } });
            _choisirPKMTypes = new ChoisirPKMTypes(starterPKM);
        }
    }
}