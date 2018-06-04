# Tower-of-the-Arch-Mage-2

Documentation

Made for HKU - Kernmodule 3 - Game Development by Ronald van Egdom

My AI Flowchart can be found at the root file called "Flowchart.png"

I do NOT own rights to PandaBT and the Pixel Perfect Camera found in the Imported Assets folder and all the rights are owned to their respective creators.

# Goal
Create smart systems for generating content and agent behaviour.

# Idea
Create a dungeon generator similar to the ones used in Pokémon Mystery dungeon and work with A* Pathfinding and Behaviour Trees.

# Blog
Week 1 : Made the AI Analyse of the Holow Knight Enemies

Week 2 : Researched more AI Types and implementations.

Week 3 : Started Working on a basic AI prototype and dungeon generator.

Week 4 : Basic Dungeon Generator.
Flood Fill Algorithm to check double corridors.

Week 5 : Optimized Dungeon Generator, added features such as doors & player + Enemy spawns.
A Star Pathfinding inside dungeon.
Experimenting with behaviour tree AI and pathfinding.

Week 6 : Further improved dungeon generator, made some final tweaks.

Further blog & images on my Twitter : https://twitter.com/Cheezegami

Post-Mortem Week 7 :
Added Blog, Added Second Behaviour Tree, Fixed minor bugs.

# Design Choices
I choose for the behaviour tree because of it's flexibility and the way it's hierarchy is set up. It's really easy to add in new behaviour as well as tweak older one, it gives structure and an overview to what sometimes can be seen as quite messy.
I was heavily inspired by the Pokémon Mystery Dungeon games for the algorithm used by the mystery dungeons in that game. You had a seemingly endless variety of rooms while always stumbling across new things.

# Programming Structure
Since I'm trying to have more SOLID programming skills I have tried to use abstract classes and interfaces. The entirity of the Entity Class is very polymorphismic in nature. It inherits from a base, and each further sequence adds more features to a base class. I also used some Interfaces and a few abstract methods where I could.

# A* and Flood Fill
Both are algorithms I've used to create pathfinding on a 2d Plane, orginally I tried having enemies do pathfinding with a floodfill method, but this proved to be a bit of a hassle because they did not save the original position and the shortest path was not calculated from the get go. The A* proved to be very useful and very effective in finding a path towards the player, even if the enemy didn't have to move their position.

# Behaviour Tree
PandaBT is a free Unity Plugin I used to create Behaviour trees, you can assign [Task] Parameters towards methods you want to excecute in a BT, and the construction of the BT itself was easily done using plain text format.

to a seed that will make Unity uses that seed with it's random generation and thus output results based on the seed.

# Learning Goals
I want to learn about the following aspects of game development.
- Procedural Generation
- AI Agent, Pathfinding and Behaviour Tree
- SOLID Programming Structure
- Generating Game Content

