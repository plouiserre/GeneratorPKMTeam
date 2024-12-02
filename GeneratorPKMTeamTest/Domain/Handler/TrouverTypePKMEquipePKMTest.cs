using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeamTest.Utils.Helper;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class TrouverTypePKMEquipePKMTest
    {
        [Fact]
        public void RecuperationPKMEquipeOK()
        {
            var listPKM = new List<PKM>() { new PKM() { Nom = "Bulbizarre" }, new PKM() { Nom = "Salamèche" }, new PKM() { Nom = "Carapuce" } };
            var choisirMeilleuresCombinaisonsTypes = Substitute.For<IChoisirMeilleuresCombinaisonsTypes>();
            choisirMeilleuresCombinaisonsTypes.GenererTirageParfait().Returns(new TiragePKMTypes());
            var assemblerEquipePKM = Substitute.For<IAssemblerEquipePKM>();
            assemblerEquipePKM.Assembler(Arg.Any<TiragePKMTypes>()).Returns(listPKM);

            var trouver = new TrouverTypePKMEquipePKM(choisirMeilleuresCombinaisonsTypes, assemblerEquipePKM);
            var resultat = trouver.GenererEquipePKM();

            Assert.Equal(3, resultat.Count);
            Assert.Equal("Bulbizarre", resultat[0].Nom);
            Assert.Equal("Salamèche", resultat[1].Nom);
            Assert.Equal("Carapuce", resultat[2].Nom);
        }

        [Fact]
        public void RecuperationPKMEquipeEchouePremiereFoisEtReussieApres()
        {
            var listPKM = new List<PKM>() { new PKM() { Nom = "Bulbizarre" }, new PKM() { Nom = "Salamèche" }, new PKM() { Nom = "Carapuce" } };
            var choisirMeilleuresCombinaisonsTypes = Substitute.For<IChoisirMeilleuresCombinaisonsTypes>();
            choisirMeilleuresCombinaisonsTypes.GenererTirageParfait().Returns(new TiragePKMTypes());
            var assemblerEquipePKM = Substitute.For<IAssemblerEquipePKM>();
            assemblerEquipePKM.Assembler(Arg.Any<TiragePKMTypes>()).Returns(x => null, x => listPKM);

            var trouver = new TrouverTypePKMEquipePKM(choisirMeilleuresCombinaisonsTypes, assemblerEquipePKM);
            var resultat = trouver.GenererEquipePKM();

            Assert.Equal(3, resultat.Count);
            Assert.Equal("Bulbizarre", resultat[0].Nom);
            Assert.Equal("Salamèche", resultat[1].Nom);
            Assert.Equal("Carapuce", resultat[2].Nom);
        }
    }
}