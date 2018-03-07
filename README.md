# Nxfxr procedural sound generator 

Nsfxr is a procedural sound generator ported to C# from Pzfxr. 
It builds with .NET 3.5, which makes it easy to use with Unity.

The project consists of two parts.

1. A library DLL containing all the generation logic and callable by a simple public API. 
This can be used directly in Unity (not yet fully tested).

2. A console executable that generates sounds and writes out WAV files. 
By default the file name is derived from the seed, or it can be specified on the command line.

Run it for help about what it does.

## Credits

Pzfxr is by Nathan Whitehead. 
See https://github.com/nwhitehead/pzfxr for prior history. 
Directly based on
the [PuzzleScript](https://www.puzzlescript.net/) code in
file `js/sfxr.js` by increpare. The PuzzleScript
sound generation functions are based on [BFXR](https://www.bfxr.net/)
by increpare, which is based on the original
[SFXR](http://www.drpetter.se/project_sfxr.html) by Thomas Petersson.

## Licence

The licence is a moot point. 
The command line program is entirely my work, and that is under the Polyomino Free Software Licence.
However, most of the work is not mine, and any licence wording in the original source code has been lost along the way.
If you really care you might have some work to do to trace the lineage.


