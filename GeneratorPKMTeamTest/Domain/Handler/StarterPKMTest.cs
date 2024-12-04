using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.CustomException.Starter;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class StarterPKMTest
    {
        private List<PKM> _tousPkms;
        private PKMs _pkms;
        private IPKMPersistence _PKMPersistence;

        public StarterPKMTest()
        {
            _pkms = new PKMs();
            var premierStarter = new PKM() { Nom = "Carapuce", PKMTypes = new List<string>() { "Eau" } };
            var secondStarter = new PKM() { Nom = "Pikachu", PKMTypes = new List<string>() { "Electrik" } };
            var troisiemeStarter = new PKM() { Nom = "Salamèche", PKMTypes = new List<string>() { "Feu" } };
            var quatriemeStarter = new PKM() { Nom = "Bulbizarre", PKMTypes = new List<string>() { "Plante", "Poison" } };
            _tousPkms = new List<PKM>() { premierStarter, secondStarter, troisiemeStarter, quatriemeStarter };
            _pkms.TousPKMs = _tousPkms;
            _PKMPersistence = Substitute.For<IPKMPersistence>();
            _PKMPersistence.GetPKMs().Returns(_pkms);

        }

        [Fact]
        public void ChoisirStarterOK()
        {
            var starterPKM = new StarterPKM(_PKMPersistence);

            var pkm = starterPKM.ChoisirStarter("Carapuce");

            Assert.NotNull(pkm);
            Assert.Equal("Carapuce", pkm.Nom);
            Assert.Equal("Eau", pkm.PKMTypes[0]);
        }

        [Fact]
        public void StarterIntrouvable()
        {
            var starterPKM = new StarterPKM(_PKMPersistence);

            var result = Assert.Throws<StarterIntrouvableException>(() => starterPKM.ChoisirStarter("Karapuce"));

            Assert.Equal("Karapuce n'est pas le nom d'un PKM existant.", result.CustomMessage);
            Assert.Equal(TypeErreur.StarterIntrouvable, result.TypeErreur);
        }

        [Fact]
        public void StarterDejaExistant()
        {
            var starterPKM = new StarterPKM(_PKMPersistence);

            var pkm = starterPKM.ChoisirStarter("Carapuce");
            var result = Assert.Throws<StarterDejaExistantException>(() => starterPKM.ChoisirStarter("Carapuce"));

            Assert.Equal("Il y a déjà un starter sélectionné.", result.CustomMessage);
            Assert.Equal(TypeErreur.StarterDejaExistant, result.TypeErreur);
        }

        [Fact]
        public void RecupererStarterDejaChoisi()
        {
            var starterPKM = new StarterPKM(_PKMPersistence);

            starterPKM.ChoisirStarter("Bulbizarre");
            var pkm = starterPKM.RecupererStarter();

            Assert.NotNull(pkm);
            Assert.Equal("Bulbizarre", pkm.Nom);
            Assert.Equal("Plante", pkm.PKMTypes[0]);
            Assert.Equal("Poison", pkm.PKMTypes[1]);
        }

        [Fact]
        public void StarterAbsent()
        {
            var starterPKM = new StarterPKM(_PKMPersistence);

            var result = Assert.Throws<StarterAbsentException>(() => starterPKM.RecupererStarter());

            Assert.Equal("Aucun starter n'a été choisi pour le moment.", result.CustomMessage);
            Assert.Equal(TypeErreur.StarterAbsent, result.TypeErreur);
        }
    }
}