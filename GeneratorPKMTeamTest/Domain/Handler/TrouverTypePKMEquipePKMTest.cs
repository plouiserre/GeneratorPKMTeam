using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeamTest.Utils.Helper;
using GeneratorPKMTeamTest.Utils.Personas;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    //TODO factoriser les tests
    public class TrouverTypePKMEquipePKMTest
    {
        [Fact]
        public void RecuperationPKMEquipeOK()
        {
            var listPKM = new List<PKM>() { new PKM() { Nom = "Bulbizarre" }, new PKM() { Nom = "Salamèche" }, new PKM() { Nom = "Carapuce" } };
            var noterEquipePKM = Substitute.For<INoterEquipePKM>();
            noterEquipePKM.AccepterCetteEquipe(listPKM).Returns(true);
            var assemblerEquipePKM = Substitute.For<IAssemblerEquipePKM>();
            assemblerEquipePKM.Assembler().Returns(listPKM);

            var trouver = new TrouverTypePKMEquipePKM(noterEquipePKM, assemblerEquipePKM);
            var resultat = trouver.GenererEquipePKM();

            Assert.Equal(3, resultat.Count);
            Assert.Equal("Bulbizarre", resultat[0].Nom);
            Assert.Equal("Salamèche", resultat[1].Nom);
            Assert.Equal("Carapuce", resultat[2].Nom);
        }

        [Fact]
        public void RecuperationPKMEquipeEchouePremiereFoisCarPasAccepteEtReussieApres()
        {
            var listPKM = new List<PKM>() { new PKM() { Nom = "Bulbizarre" }, new PKM() { Nom = "Salamèche" }, new PKM() { Nom = "Carapuce" } };
            var noterEquipePKM = Substitute.For<INoterEquipePKM>();
            noterEquipePKM.AccepterCetteEquipe(listPKM).Returns(x => false, x => true);
            var assemblerEquipePKM = Substitute.For<IAssemblerEquipePKM>();
            assemblerEquipePKM.Assembler().Returns(listPKM);

            var trouver = new TrouverTypePKMEquipePKM(noterEquipePKM, assemblerEquipePKM);
            var resultat = trouver.GenererEquipePKM();

            Assert.Equal(3, resultat.Count);
            Assert.Equal("Bulbizarre", resultat[0].Nom);
            Assert.Equal("Salamèche", resultat[1].Nom);
            Assert.Equal("Carapuce", resultat[2].Nom);
        }

        [Fact]
        public void RecuperationPKMEquipeEchouePremiereFoisCarPasTrouveEtReussieApres()
        {
            var listPKM = new List<PKM>() { new PKM() { Nom = "Bulbizarre" }, new PKM() { Nom = "Salamèche" }, new PKM() { Nom = "Carapuce" } };
            var noterEquipePKM = Substitute.For<INoterEquipePKM>();
            noterEquipePKM.AccepterCetteEquipe(listPKM).Returns(true);
            var assemblerEquipePKM = Substitute.For<IAssemblerEquipePKM>();
            assemblerEquipePKM.Assembler().Returns(x => null, x => listPKM);

            var trouver = new TrouverTypePKMEquipePKM(noterEquipePKM, assemblerEquipePKM);
            var resultat = trouver.GenererEquipePKM();

            Assert.Equal(3, resultat.Count);
            Assert.Equal("Bulbizarre", resultat[0].Nom);
            Assert.Equal("Salamèche", resultat[1].Nom);
            Assert.Equal("Carapuce", resultat[2].Nom);
        }

        [Fact]
        public void RecuperationPKMEquipeEchouePremiereFoisEnsuiteOKSansMockPourNote()
        {
            var datasFake = PKMDonneesPersonas.GetPersonas();
            var listPKM = CreerListePKM();
            var assemblerEquipePKM = Substitute.For<IAssemblerEquipePKM>();
            assemblerEquipePKM.Assembler().Returns(x => null, x => listPKM);
            var resultatCombatPKMType = Substitute.For<IResultatCombatPKMTypes>();
            resultatCombatPKMType.NoterResultatTirage(Arg.Any<List<PKMType>>(), Arg.Any<List<PKMType>>(),
            Arg.Any<List<PKMType>>()).Returns(new ResultatTirage() { NoteResultatTirage = 100, ResultatStatus = ResultatTirageStatus.Parfait });
            var pkmTypePersistance = Substitute.For<IPKMTypePersistence>();
            pkmTypePersistance.GetPKMDonnees().Returns<PKMDonnees>(datasFake);
            var noterEquipePKM = new NoterEquipePKM(resultatCombatPKMType, pkmTypePersistance);

            var trouver = new TrouverTypePKMEquipePKM(noterEquipePKM, assemblerEquipePKM);
            var resultat = trouver.GenererEquipePKM();

            Assert.Equal(3, resultat.Count);
            Assert.Equal("Bulbizarre", resultat[0].Nom);
            Assert.Equal("Carapuce", resultat[1].Nom);
            Assert.Equal("Salamèche", resultat[2].Nom);
        }

        private List<PKM> CreerListePKM()
        {
            var pkms = new List<PKM>();
            var bulbizarre = new PKM() { Nom = "Bulbizarre", PKMTypes = new List<string>() { "Plante", "Poison" } };
            var carapuce = new PKM() { Nom = "Carapuce", PKMTypes = new List<string>() { "Eau" } };
            var salameche = new PKM() { Nom = "Salamèche", PKMTypes = new List<string>() { "Feu" } };
            pkms.Add(bulbizarre);
            pkms.Add(carapuce);
            pkms.Add(salameche);
            return pkms;
        }
    }
}