# Générateur OTP - Projet de Sécurité

## Description

Ce projet implémente un système de génération et de validation d'OTP (One-Time Password) en utilisant une architecture client-serveur. L'application est développée en C# avec WPF (Windows Presentation Foundation) et suit les principes de sécurité pour l'authentification à deux facteurs.

## Architecture

Le projet est composé de deux applications principales :

### 🖥️ Serveur
- **Rôle** : Gestion des OTP, validation et stockage sécurisé
- **Technologie** : C# WPF
- **Fonctionnalités** :
  - Génération d'OTP sécurisés
  - Validation des codes reçus
  - Interface d'administration
  - Gestion des sessions

### 📱 Client
- **Rôle** : Demande et réception d'OTP
- **Technologie** : C# WPF
- **Fonctionnalités** :
  - Demande d'OTP
  - Affichage des codes reçus
  - Interface utilisateur intuitive

## Structure du Projet

```
generateur-otp/
├── Server/                 # Application serveur
│   ├── MainWindow.xaml     # Interface principale du serveur
│   ├── MainWindow.xaml.cs  # Logique métier du serveur
│   └── Server.csproj      # Configuration du projet serveur
├── Client/                 # Application client
│   ├── MainWindow.xaml     # Interface principale du client
│   ├── MainWindow.xaml.cs  # Logique métier du client
│   └── Client.csproj      # Configuration du projet client
└── Solution1.sln          # Solution Visual Studio
```

## Prérequis

- **Visual Studio** 2019 ou plus récent
- **.NET Framework** 4.7.2 ou plus récent
- **Windows** 10/11

## Installation et Configuration

### 1. Cloner le projet
```bash
git clone [URL_DU_REPO]
cd generateur-otp
```

### 2. Ouvrir la solution
- Ouvrir `Solution1.sln` dans Visual Studio
- Restaurer les packages NuGet si nécessaire

### 3. Compiler le projet
- Compiler la solution complète (Ctrl+Shift+B)
- Vérifier qu'il n'y a pas d'erreurs de compilation

## Utilisation

### Démarrage du Serveur
1. Lancer l'application `Server`
2. L'interface serveur s'ouvre et attend les connexions
3. Le serveur est prêt à générer et valider des OTP

### Utilisation du Client
1. Lancer l'application `Client`
2. Se connecter au serveur
3. Demander un OTP
4. Utiliser le code reçu pour l'authentification

## Fonctionnalités de Sécurité

- **Génération cryptographique** d'OTP
- **Expiration automatique** des codes
- **Validation sécurisée** côté serveur
- **Protection contre la réutilisation** des codes
- **Chiffrement** des communications

## Développement

### Ajout de nouvelles fonctionnalités
1. Modifier le code source dans le projet approprié
2. Tester les modifications
3. Compiler et vérifier l'absence d'erreurs
4. Documenter les changements

### Tests
- Tester la génération d'OTP
- Vérifier la validation des codes
- Tester la gestion des erreurs
- Valider la sécurité des communications

## Déploiement

### Serveur
- Compiler en mode Release
- Déployer sur un serveur sécurisé
- Configurer les paramètres de sécurité

### Client
- Compiler en mode Release
- Distribuer aux utilisateurs finaux
- Configurer la connexion au serveur

## Maintenance

- **Surveillance** des logs serveur
- **Mise à jour** des composants de sécurité
- **Sauvegarde** des configurations
- **Monitoring** des performances

## Support

Pour toute question ou problème :
- Consulter la documentation technique
- Vérifier les logs d'erreur
- Contacter l'équipe de développement

## Licence

Ce projet est développé dans le cadre d'un TP de sécurité informatique.

---

**Note** : Ce projet est destiné à des fins éducatives et de démonstration. Pour un usage en production, des audits de sécurité supplémentaires sont recommandés.
