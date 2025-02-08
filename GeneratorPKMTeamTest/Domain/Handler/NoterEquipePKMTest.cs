using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeamTest.Utils.Helper;
using GeneratorPKMTeamTest.Utils.Personas;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class NoterEquipePKMTest
    {
        private PKMDonnees _fakeDatas;

        public NoterEquipePKMTest()
        {
            _fakeDatas = PKMDonneesPersonas.GetPersonas();
        }

        [Theory]
        [InlineData("Parfait", 100, true)]
        [InlineData("Excellent", 85, true)]
        [InlineData("Bonnes", 65, false)]
        [InlineData("Acceptables", 45, false)]
        [InlineData("Passables", 25, false)]
        [InlineData("Faibles", 15, false)]
        public void NoteEquipePKM(string status, int note, bool resultat)
        {
            var ResultatTirageStatus = DeterminerResultatTirageStatusApartirLabel(status);
            var starterPKM = Substitute.For<IGererStarterPKM>();
            starterPKM.RecupererStarter().Returns(new PKM() { Nom = "Carapuce", PKMTypes = new List<string>() { "Eau" } });
            var pKMTypePersistence = Substitute.For<IPKMTypePersistence>();
            pKMTypePersistence.GetPKMDonnees().Returns(_fakeDatas);
            var pkms = RetournerPKMs();
            var resultatCombatPKMTypes = Substitute.For<IResultatCombatPKMTypes>();
            resultatCombatPKMTypes.NoterResultatTirage(Arg.Any<List<PKMType>>(), Arg.Any<List<PKMType>>(), Arg.Any<List<PKMType>>())
                    .Returns(new ResultatTirage() { ResultatStatus = ResultatTirageStatus, NoteResultatTirage = note });

            var noterTirage = new NoterEquipePKM(resultatCombatPKMTypes, pKMTypePersistence);

            var tirageResultat = noterTirage.AccepterCetteEquipe(pkms);

            Assert.Equal(resultat, tirageResultat);
        }

        private List<PKM> RetournerPKMs()
        {
            var pkms = new List<PKM>();
            var allPkms = DatasHelperTest.RetournersTousPKM().Take(100).ToList();
            for (int i = 0; i < allPkms.Count; i++)
            {
                int[] idsOK = { 0, 3, 6, 9, 15, 26 };
                if (idsOK.Contains(i))
                    pkms.Add(allPkms[i]);
            }
            return pkms;
        }

        private ResultatTirageStatus DeterminerResultatTirageStatusApartirLabel(string label)
        {
            switch (label)
            {
                case "Parfait":
                    return ResultatTirageStatus.Parfait;
                case "Excellent":
                    return ResultatTirageStatus.Parfait;
                case "Bonnes":
                    return ResultatTirageStatus.Bonnes;
                case "Acceptables":
                    return ResultatTirageStatus.Acceptable;
                case "Passables":
                    return ResultatTirageStatus.Passables;
                case "Faibles":
                default:
                    return ResultatTirageStatus.Faible;
            }

        }
    }
}