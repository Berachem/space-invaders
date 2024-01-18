# Space Invaders 👾
### Retrouvez notre rapport du projet sur le lien suivant : [Rapport du projet](/Rapport_Space_Invaders.pdf) 🗒️

<center>
  <video width="480" height="480" controls>
    <source src="https://igadvisory.fr/opendata/spaceInvaders/space_invader_demo.mp4" type="video/mp4">
  </video>
</center>




Réalisé par Berachem MARKRIA & Joshua LEMOINE ❤

## Stack technique du projet
[![My Skills](https://skillicons.dev/icons?i=cs,visualstudio)](https://skillicons.dev)

## Problématique du projet 🚀
Ce projet en C# consiste à développer le célèbre jeu Space
Invaders, un classique de l'arcade où le joueur contrôle un vaisseau
spatial afin de défendre la Terre contre une invasion extraterrestre.

L'objectif principal du projet est de créer une application
interactive offrant une expérience de jeu fluide et divertissante. Le
défi réside dans la mise en œuvre des éléments essentiels du jeu, tels
que la gestion des mouvements du vaisseau spatial, le tir de
projectiles, la génération des ennemis et la gestion des collisions.

Le code doit être structuré de manière à garantir une bonne
facilité de maintenance et de lecture tout en suivant les principes de
programmation orientée objet. De plus, le rendu visuel du jeu doit
être soigné, avec une interface utilisateur attrayante et des
graphismes reflétant l'esthétique emblématique du jeu Space Invaders.
Ce projet offre une opportunité d'appliquer les concepts fondamentaux
de la programmation en C# tout en créant une expérience ludique pour
les utilisateurs.

## Addons réalisés 🌟

<table border="0">
    <tr>
        <td>
          <img src="https://igadvisory.fr/opendata/spaceInvaders/2v2_game.png" />
            _______________________________________________________________________
        </td>
        <td>
            <img src="https://igadvisory.fr/opendata/spaceInvaders/final_boss.png"/>
            _______________________________________________________________________
        </td>
        <td>
            <img src="https://igadvisory.fr/opendata/spaceInvaders/highscore.png" />
            _______________________________________________________________________
        </td>
    </tr>
</table>

- Multijoueur en local 🎮
  - Gestion des touches
- Menus 📋
  - Gestion des events personnalisés par bouton
- Décorations visuelles 🎨
  - Fond d'écran
  - Boutons
  - Points de vie
  - Vaisseaux
  - Tirs
  - Bonus
- Sons & Musiques 🎵
  - Arrière-plan dans les menus
  - Victoire/défaite
  - Tirs
  - Perte de PV
- Bonus 🎁
  - Gain de points
  - Gain de vie
  - Gain de bouclier
- Boss Final (système de tir personnalisé) 👾
  - Système de tir personnalisé
  - Déplacements en "zig-zag"
  - Bouclier régénératif
  - Affichage de la barre de vie
- Leaderboard de fin de partie (style Arcade) 🏆
  - Identification des parties
  - Stockage des scores dans un fichier

## Structure du projet 
```

-- SpaceInvaders
│   ├── GameObjects
│   │   ├── Bonus
│   │   │   ├── Bonus.cs
│   │   │   ├── HealingBonus.cs
│   │   │   ├── ScoreBonus.cs
│   │   │   ├── ShieldBonus.cs
│   │   ├── Decorations
│   │   │   ├── Background.cs
│   │   │   ├── Menu.cs
│   │   │   ├── MenuButton.cs
│   │   ├── Enemies
│   │   │   ├── Boss.cs
│   │   │   ├── EnemyBlock.cs
│   │   ├── Missiles
│   │   │   ├── Missile.cs
│   │   │   ├── MonoShooting.cs
│   │   │   ├── MultiShooting.cs
│   │   │   ├── ShootingSystem.cs
│   │   ├── Utils
│   │   │   ├── Controls.cs
│   │   │   ├── HighScoreManager.cs
│   │   │   ├── SerializerFileManager.cs
│   │   │   ├── SoundManager.cs
│   │   │   ├── Vecteur2D.cs
│   │   ├── Bunker.cs
│   │   ├── GameObject.cs
│   │   ├── GeneralObject.cs
│   │   ├── PlayerSpaceship.cs
│   │   ├── SimpleObject.cs
│   │   ├── SpaceShip.cs
│
├── LICENSE
├── README.md
├── SpaceInvaders.sln
├── app.config
├── ClassDiagram1.cd
├── Form1.cs
├── Form1.Designer.cs
├── Form1.resx
├── Game.cs
├── Program.cs
└── SpaceInvaders.csproj.
```
## 🕹️ Touches

<img src="https://igadvisory.fr/opendata/spaceInvaders/touches.png" />

