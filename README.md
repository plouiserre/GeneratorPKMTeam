# GeneratorPKMTeam

Un projet pour permettre de trouver à chaque fois la meilleure équipe PKM pour chaque génération.

## Pré-requis

Il suffit d'avoir Visual Studio ou VSCode de récupérer les sources et de pouvoir le lancer l'IDE

## Spécification technique

### Architecture technique

L'architecture hexagonale a été utilisée pour faire ce projet permettant de centralisant toute la logique business dans la couche domain et ainsi de faciliter les tests.

### Tests

Deux stratégies ont été mises en place pour les tests :

- toute classe présentant une logique ou algorithmie est testée avec des mocks
- la classe **GeneratePKMTeamHandlerTest** ne mocke que l'accès au fichier de configuration permettant ainsi de pouvoir valider toute la chaîne.

## Auteur

Pierre-Louis SERRE a crée ce projet pendant son temps libre pour développer sa maîtrise du TDD, de l'architecture hexagonale et de l'algorithmie

## Licence

Ce projet est sous licence MIT
