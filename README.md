# FrigidRogue
This is a C# library for making Roguelike games using MonoGame.  The library is still in early development as I develop my game [Mars Undiscovered](https://github.com/DavidFidge/MarsUndiscovered) on top of it.  I have made this repo public but it is not intended for public use yet (and more than likely never will be).  The code is undocumented, has few comments and still has some unneccessary files in it from a project I copied it from (will later remove those which I do not need once my game becomes more mature).

This library makes use of other libraries - GoRogue 3, SadPrimitives, GeonBit.UI, MediatR and Castle Windsor.

FrigidRogue is currently designed to use GeonBit.UI for the user interface and to render tiles onto a render target which is then rendered onto a 3D quad.  It is not designed for console user interfaces - look to SadConsole if you're interested in that.

An example of how the map rendering can look with a rotation applied:

![Rendering](/Images/readme1.png)
