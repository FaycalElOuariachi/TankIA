Le projet Unity est contenu dans le dossier "Tank IA" (à charger dans Unity 3D)

Le dossier "Interfaces" contient un projet C# (Visual Studio/MonoDevelop). Il sert à faire le lien entre les APIs et le jeu

Le dossier "Log" contient une implémentation d'un Logger.

Les dossiers "IATankOne" et "IATankTwo" contiennent deux implémentations différentes de bot.

Le dossier "IAMany" contient l'implémentation de notre robot apprenti.

Le dossier "metaGenBayes/testPAND" permet de faire l'apprentissage. Attention, bien reporter ses variables utilisées dans son modèle probabiliste
dans le fichier main_createIA.py. Se référer à la donc pyAgrum.

Les exécutables sont dans "Windows/TankIA/" et "Linux/TankIA" selon la version.