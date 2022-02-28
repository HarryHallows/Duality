# Duality Global Game Project

## How to open

**== INSTRUCTIONS ==**

**INSTALLATION**
1) Clone repo
2) Install Unity Hub and install editor version 2020.3.24f1 (https://unity3d.com/get-unity/download/archive)
3) Open through Unity Hub portal by clicking "Open" > *local file directory* > 'Duality' folder.
4) Once project is open navigate to the Scripts folder to access all of the source code for the project.

**TESTING CURRENT VERSION OF PROJECT**   
1) Go to the Scenes folder
2) Open the 'main' Scene
3) Press the play button on top of the Editor and interact with mouse with the scenes

*Example Visual:*

![Alt-text](https://media.giphy.com/media/Caxvp3ekp19lgl6WyH/giphy.gif)


## Background information about the project

Currently the project is in a unfinished state, I am currently working on the direction for the narrative to be able to correlate the correct puzzles to go along with these panoramas and start tying together the core loop. 

What's operational at this moment are the navigations between two example spaces and a few example highlightable objects (which would represent interactions within each space).

## Stretch goals for this project

As it is, I think it'll operate fine on most PC devices. However, to ensure that it could optimally work on also mobile devices I intend to rebuild using the Entity Component System (ECS) with the Data-Orientated Tech Stack. This will allow for the project to run the system's processes at a high sucessrates even on lower end devices allowing me to scale up the project as needed. This is achieved through the usage of multi-threaded processes in combination with the Job system.

