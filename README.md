# The Dumb Robot

## I KNOW `Thread.Sleep` is bad, but its for visuals :rofl:

## Building

To build this project run: `dotnet build` or `dotnet run -c Release`
you need .NET 5 installed, otherwise feel free to change the target runtime, although you might face some issues cuz i've used C#9 syntax

## DEBUG vs RELEASE

if the project is build in **DEBUG** it will show a nice step by step animation for every step the robot does.
otherwise the `.Traverse()` will just return the number of traversed cells
you still can access the latest grid start using the `.Grid` property
if you wish to disable the animation alltogether pass `false` to the constructor in the `debugMode` parameter
