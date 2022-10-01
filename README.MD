## Setup Project for Unity 2021.3.10f1
* Install [Unity Hub](https://unity3d.com/get-unity/download)
* Open Unity Hub, go to "Installs" and click on "Install Editor" in the upper right corner
* Install version **2021.3.10f1**
* In the Unity Hub, go to "Projects", and click on the dropdown arrow besides the "open" button in the upper right corner
* Click on "Add project from disk" and navigate to the folder into which you have cloned this repository
* Click on the added project
* Wait for Unity to install all packages and start the Editor

## Project Settings
The project is configured for the Universal Render Pipeline (URP) and has a default setting for medium quality set.  

Entering Play-Mode will not reload scripts. This makes entering play-mode a lot faster.
On the downside, scripts will not be reset when entering play-mode. To force a so called "domain reload" and reload and reset script states, either make changes to an existing script or re-import any script from the project.

