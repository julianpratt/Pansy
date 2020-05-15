Pansy
=====

Pansy is a vintage console application that attempts to be a flight simulator. The original was given to me by a friend at University in about 1976.

If you answer yes to the first question, then some usage instructions will come up. These are not always that accurate, as you may find out.

The advice is to get the bird off the ground, bank sharp left (-100 on the ailerons) until the deviation comes down. Get the bearing and deviation sitting nicely at about 0. Don't go too fast (above 100km/hr) or too high (above 100m), because there is only just enough distance to land it again. Land with a speed between 55km/hr and 70 km/hr. 

I make no apologies for the quality of this code. It is dreadful, but it works and remains faithful to the original.

There are no dependencies.



Usage
--------

This application requires the [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download) to be downloaded and installed (preferably on the server where it is to run).

Having installed the SDK: download the application from GitHub (either using git or by downloading and unzipping the zip file), and then build and publish it.

It may be necessary to change the RuntimeIdentifier in the csproj (which is currently set to "osx-x64"). Search "dotnet core rid catalog" for the right setting for your machine (e.g. Windows users will need "win-x64").

```
git clone https://github.com/julianpratt/Pansy.git
dotnet build 
dotnet publish
```

Copy the executable from the publish folder (which dotnet will tell you) to a folder on your path (I have a folder called Tools for useful console apps and scripts). 

Pansy is run by issuing the command:

```
pansy
```
