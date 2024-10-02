Integrated Human Ventricular Cell Model with Excitation-Contraction-Energetics Coupling

This is an Integrated Human Ventricular Cell Model with Excitation-Contraction-Energetics Coupling, created by Ayako Takeuchi and Satoshi Matsuoka and published in Journal of Physiology 2024. 
The program was created using Visual C# (Microsoft Visual Studio 2022) and the ordinary differential equations were integrated using the 4th order Runge-Kutta method with an adaptive time step. 

A. Directory structure

IHVCM

--HumanVentricularCell

----Bin

----InitFiles

----Obj

----Properties


The "InitiFiles" directory contains an initial file (HumanVentricularCell.asc), which will be read upon executing the program. 
The model source codes and GUI source codes are contained in "HumanVentricularCell" directory. 

B. System requirements

Since this program uses a lot of memory, X64 platform is recommended. 
The code was written with Microsoft Visual Studio 2022. the program performance in older versions of Microsoft Visual Studio was not tested.

C. How to use

Set the platform to "X64".

Upon execute, a GUI form will appear.

Select "File"->"Load InitFile" in menu bar, then an initial file will be loaded.

Select "Start" in menu bar, then the program will start with default settings.

Some parameters can be modified in a left panel (Command 1 and Command 2).

Some parameters of the model cell can be modified in a right panel (Parameter list). Parameters which can be modified are marked “P”, and variables, which cannot be modified, are marked “V”. 
To change parameters, check “Alt” column, then change the value in “Value” column. 

